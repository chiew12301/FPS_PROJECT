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

        var moveVec = cohesionVec + avoidanceVec + alignmentVec + boundsVec;
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

    private Vector3 CalculateObstacleVector()
    {
        var obstacleVector = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(myTransform.position, myTransform.forward, out hit, assignedFlock.obstacleDistance, obstacleMask))
        {
            obstacleVector = FindDirToAvoidObstacle();
        }
        return obstacleVector;
    }

    private Vector3 FindDirToAvoidObstacle()
    {
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
        }
        return selectedDir.normalized;
    }

    private bool IsInFOV(Vector3 pos)
    {
        return Vector3.Angle(myTransform.forward, pos - myTransform.position) <= FOVAngle;
    }
}
