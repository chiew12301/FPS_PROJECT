using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{
    public Transform myTransform { get; set; }

    [SerializeField] private float FOVAngle;
    [SerializeField] private float smoothDamp;

    private List<FlockUnit> cohesionNeighbours = new List<FlockUnit>();
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
        var cohesionVec = CalculateCohesionVector();
        var moveVec = Vector3.SmoothDamp(myTransform.forward, cohesionVec, ref currentVelocity, smoothDamp);
        moveVec = moveVec.normalized * speed;
        myTransform.forward = moveVec;
        myTransform.position += moveVec * Time.deltaTime;
    }

    private void FindNeighbourUnits()
    {
        cohesionNeighbours.Clear();
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

        if (neighbourInFOV == 0)
            return cohesionVector;

        cohesionVector /= neighbourInFOV;
        cohesionVector -= myTransform.position;
        cohesionVector = Vector3.Normalize(cohesionVector);

        return cohesionVector;
    }

    private bool IsInFOV(Vector3 pos)
    {
        return Vector3.Angle(myTransform.forward, pos - myTransform.position) <= FOVAngle;
    }
}
