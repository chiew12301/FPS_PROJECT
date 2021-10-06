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

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        //if (currWaypoint == null)
        //{
        //    GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        //    if (allWaypoints.Length > 0)
        //    {
        //        while (currWaypoint == null)
        //        {
        //            // pick random waypoint
        //            int randomNum = UnityEngine.Random.Range(0, allWaypoints.Length);
        //            ConnectedWaypoint startingWaypoint = allWaypoints[randomNum].GetComponent<ConnectedWaypoint>();

        //            if (startingWaypoint != null)
        //            {
        //                currWaypoint = startingWaypoint;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogError("Cannot find enough waypoints. Check gameobject tags or maybe distance too far apart.");
        //    }
        //}

        //SetDestination();
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
        rigidbody.velocity = moveSpeed * transform.forward;
    }

    private void SetDestination()
    {
        if (waypointIndex > 0)
        {
            ConnectedWaypoint nextWaypoint = currWaypoint.NextWaypoint(prevWaypoint);
            prevWaypoint = currWaypoint;
            currWaypoint = nextWaypoint;
        }

        rigidbody.velocity += currWaypoint.transform.position;

        //transform.position = Vector3.MoveTowards(transform.position, currWaypoint.transform.position, moveSpeed * Time.deltaTime);
        isMoving = true;
    }
}
