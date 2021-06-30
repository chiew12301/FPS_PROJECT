using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{
    public Transform myTransform { get; set; }

    [SerializeField] private float FOVAngle;
    [SerializeField] private float smoothDamp;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private Vector3[] dirToCheckWhenAvoidingObstacles;

    private List<FlockUnit> cohesionNeighbours = new List<FlockUnit>();
    private List<FlockUnit> avoidanceNeighbours = new List<FlockUnit>();
    private List<FlockUnit> alignmentNeighbours = new List<FlockUnit>();
    private Flock assignedFlock;
    private Vector3 currentVelocity;
    private Vector3 currentObstacleAvoidanceVector;
    private float speed;

    private void Awake()
    {
        myTransform = transform;
    }

    public void AssignFlock(Flock flock)
    {
        assignedFlock = flock;
    }

    public void InitialiseSpeed (float speed)
    {
        this.speed = speed;
    }

    public void MoveUnit()
    {
        FindNeighbourUnits();
        CalculateSpeed();

        // cohesion vector - average position of all units in a certain radius
        var cohesionVec = CalculateCohesionVector() * assignedFlock.cohesionWeight;
        var avoidanceVec = CalculateAvoidanceVector() * assignedFlock.avoidanceWeight;
        var alignmentVec = CalculateAlignmentVector() * assignedFlock.alignmentWeight;
        var boundsVec = CalculateBoundsVector() * assignedFlock.boundsWeight;
        var obstacleVec = CalculateObstacleVector() * assignedFlock.obstacleWeight;
        var targetVec = CalculateTargetVector();

        var moveVec = cohesionVec + avoidanceVec + alignmentVec + boundsVec;

        if (IsHeadingForCollision())
        {
            Vector3 collisionAvoidDir = ObstacleRays();
            Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * 10;
            moveVec += collisionAvoidForce;
        }

        moveVec = Vector3.SmoothDamp(myTransform.forward, moveVec, ref currentVelocity, smoothDamp);
        moveVec = moveVec.normalized * speed;
        myTransform.forward = moveVec;
        myTransform.position += moveVec * Time.deltaTime;

    }

    private void FindNeighbourUnits()
    {
        cohesionNeighbours.Clear();
        avoidanceNeighbours.Clear();
        alignmentNeighbours.Clear();

        var allUnits = assignedFlock.allUnits;

        for (int i = 0; i < allUnits.Length; i++)
        {
            var currentUnit = allUnits[i];
            if (currentUnit != this)
            {
                float currentNeighbourDistanceSqr = Vector3.SqrMagnitude(currentUnit.transform.position - transform.position);
                if (currentNeighbourDistanceSqr <= assignedFlock.cohesionDistance * assignedFlock.cohesionDistance)
                {
                    cohesionNeighbours.Add(currentUnit);
                }
                if (currentNeighbourDistanceSqr <= assignedFlock.avoidanceDistance * assignedFlock.avoidanceDistance)
                {
                    avoidanceNeighbours.Add(currentUnit);
                }
                if (currentNeighbourDistanceSqr <= assignedFlock.alignmentDistance * assignedFlock.alignmentDistance)
                {
                    alignmentNeighbours.Add(currentUnit);
                }
            }
        }

    }

    private void CalculateSpeed()
    {
        if (cohesionNeighbours.Count == 0)
            return;
        speed = 0;
        for (int i = 0; i < cohesionNeighbours.Count; i++)
        {
            speed += cohesionNeighbours[i].speed;
        }
        speed /= cohesionNeighbours.Count;
        speed = Mathf.Clamp(speed, assignedFlock.minSpeed, assignedFlock.maxSpeed);
    }

    private Vector3 CalculateTargetVector()
    {
        var targetVector = assignedFlock.GetTarget().transform.position;
        if (assignedFlock.GetTarget() == null)
        {
            return Vector3.zero;
        }
        else
        {
            var dir = targetVector - myTransform.position;           

            if (dir.magnitude <= 20.0f)
            {
                Debug.DrawRay(myTransform.position, dir, Color.green);
                return dir;
            }
            else
            {
                var dir2 = assignedFlock.transform.position - myTransform.position;
                //Debug.DrawRay(myTransform.position, dir2, Color.red);
                return dir2;
            }
        }       
    }

    private Vector3 CalculateCohesionVector()
    {
        var cohesionVector = Vector3.zero;
        if (cohesionNeighbours.Count == 0)
            return cohesionVector; 

        int neighbourInFOV = 0;
        for (int i = 0; i < cohesionNeighbours.Count; i++)
        {
            if (IsInFOV(cohesionNeighbours[i].myTransform.position))
            {
                neighbourInFOV++;
                cohesionVector += cohesionNeighbours[i].transform.position;
            }
        }

        cohesionVector /= neighbourInFOV;
        cohesionVector -= myTransform.position;
        cohesionVector = cohesionVector.normalized;

        return cohesionVector;
    }

    private Vector3 CalculateAlignmentVector()
    {
        var alignmentVector = myTransform.forward;
        if (alignmentNeighbours.Count == 0)
            return alignmentVector;

        int neighbourInFOV = 0;
        for (int i = 0; i < alignmentNeighbours.Count; i++)
        {
            if (IsInFOV(alignmentNeighbours[i].myTransform.forward))
            {
                neighbourInFOV++;
                alignmentVector += alignmentNeighbours[i].myTransform.forward;
            }
        }

        if (neighbourInFOV == 0)
            return myTransform.forward;
        alignmentVector /= neighbourInFOV;
        alignmentVector = alignmentVector.normalized;
        return alignmentVector;
    }

    private Vector3 CalculateAvoidanceVector()
    {
        var avoidanceVector = Vector3.zero;
        if (avoidanceNeighbours.Count == 0)
            return Vector3.zero;

        int neighbourInFOV = 0;
        for (int i = 0; i < avoidanceNeighbours.Count; i++)
        {
            if (IsInFOV(avoidanceNeighbours[i].myTransform.position ))
            {
                neighbourInFOV++;
                avoidanceVector += (myTransform.position - avoidanceNeighbours[i].transform.position);
            }
        }

        avoidanceVector /= neighbourInFOV;
        avoidanceVector = avoidanceVector.normalized;
        return avoidanceVector;
    }

    private Vector3 CalculateBoundsVector()
    {
        var offsetToCentre = assignedFlock.transform.position - myTransform.position;
        bool isNearCentre = (offsetToCentre.magnitude >= assignedFlock.boundsDistance * 0.9);
        return isNearCentre ? offsetToCentre.normalized : Vector3.zero;
    }

    bool IsHeadingForCollision()
    {
        RaycastHit hit;
        if (Physics.SphereCast(myTransform.position, assignedFlock.boundsDistance, myTransform.forward, out hit, assignedFlock.obstacleDistance, obstacleMask))
        {
            return true;
        }
        else { }
        return false;
    }
    Vector3 ObstacleRays()
    {
        Vector3[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Vector3 dir = myTransform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(myTransform.position, dir);
            if (!Physics.SphereCast(ray, assignedFlock.boundsDistance, assignedFlock.obstacleDistance, obstacleMask))
            {
                return dir;
            }
        }

        return myTransform.forward;
    }

    private Vector3 CalculateObstacleVector()
    {
        var obstacleVector = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(myTransform.position, myTransform.forward, out hit, assignedFlock.obstacleDistance, obstacleMask))
        {
            obstacleVector = FindDirToAvoidObstacle();
        }
        else
        {
            currentObstacleAvoidanceVector = Vector3.zero;
        }
        return obstacleVector;
    }

    private Vector3 FindDirToAvoidObstacle()
    {
        if (currentObstacleAvoidanceVector != Vector3.zero)
        {
            RaycastHit hit;
            if (!Physics.Raycast(myTransform.position, myTransform.forward, out hit, assignedFlock.obstacleDistance, obstacleMask))
            {
                return currentObstacleAvoidanceVector;
            }
        }

        float maxDistance = int.MinValue;
        var selectedDir = Vector3.zero;
        for (int i = 0; i < dirToCheckWhenAvoidingObstacles.Length; i++)
        {
            RaycastHit hit;
            var currentDir = myTransform.TransformDirection(dirToCheckWhenAvoidingObstacles[i].normalized);
            if (Physics.Raycast(myTransform.position, currentDir, out hit, assignedFlock.obstacleDistance, obstacleMask))
            {
                float currentDist = (hit.point - myTransform.position).sqrMagnitude;
                if (currentDist > maxDistance)
                {
                    maxDistance = currentDist;
                    selectedDir = currentDir;
                }
            }
            else
            {
                selectedDir = currentDir;
                currentObstacleAvoidanceVector = currentDir.normalized;
                return selectedDir.normalized;
            }
        }
        return selectedDir.normalized;
    }

    private bool IsInFOV(Vector3 pos)
    {
        return Vector3.Angle(myTransform.forward, pos - myTransform.position) <= FOVAngle;
    }

    Vector3 SteerTowards(Vector3 vector)
    {
        Vector3 v = vector.normalized * assignedFlock.minSpeed - (myTransform.forward * assignedFlock.minSpeed);
        return Vector3.ClampMagnitude(v, 5);
    }
}
