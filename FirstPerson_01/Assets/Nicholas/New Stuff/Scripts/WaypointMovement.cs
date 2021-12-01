using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;

    private ConnectedWaypoint[] connectedWaypoints;
    private ConnectedWaypoint prevWaypoint;
    private ConnectedWaypoint currWaypoint;
    private Rigidbody rigidbody;
    private int waypointIndex;
    private bool isMoving;

    private Vector3 desiredVelocity;
    private Vector3 acceleration;
    private Vector3 currentVelocity;
    private Vector3 position;
    private Vector3 target;
    [SerializeField] private GameObject targetGo;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        if (currWaypoint == null)
        {
            GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

            if (allWaypoints.Length > 0)
            {
                while (currWaypoint == null)
                {
                    // pick random waypoint
                    int randomNum = UnityEngine.Random.Range(0, allWaypoints.Length);
                    ConnectedWaypoint startingWaypoint = allWaypoints[randomNum].GetComponent<ConnectedWaypoint>();

                    if (startingWaypoint != null)
                    {
                        currWaypoint = startingWaypoint;
                    }
                }
            }
            else
            {
                Debug.LogError("Cannot find enough waypoints. Check gameobject tags or maybe distance too far apart.");
            }
        }

        //SetDestination();
        GetWaypoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float remainingDistance = Vector3.Distance(transform.position, currWaypoint.transform.position);

        //if (isMoving && remainingDistance <= 1.0f)
        //{
        //    isMoving = false;
        //    waypointIndex++;
        //}

        //SetDestination();      
    }

    private void Update()
    {
     
        position = transform.position;
        target = targetGo.transform.position;

        var remainingDistance = Vector3.Distance(position, currWaypoint.transform.position);

        if (isMoving && remainingDistance <= 1.0f)
        {
            isMoving = false;
            waypointIndex++;

            GetWaypoint();
        }       

        var distanceToTarget = currWaypoint.transform.position - position;
        var directionToTarget = distanceToTarget.normalized;

        desiredVelocity = directionToTarget * moveSpeed;
        var massToUse = Mathf.Max(0.001f, 1.0f);
        var steeringVector = desiredVelocity - currentVelocity;
        acceleration = Vector3.ClampMagnitude(steeringVector, 1.0f) / massToUse;

        currentVelocity += acceleration;
        currentVelocity = Vector3.ClampMagnitude(currentVelocity, moveSpeed);
        transform.Translate(currentVelocity * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.LookRotation(currentVelocity);

        Debug.Log(currWaypoint.gameObject);
    }

    private void GetWaypoint()
    {
        if (waypointIndex > 0)
        {
            ConnectedWaypoint nextWaypoint = currWaypoint.NextWaypoint(prevWaypoint);
            prevWaypoint = currWaypoint;
            currWaypoint = nextWaypoint;
        }

        isMoving = true;
    }
}
