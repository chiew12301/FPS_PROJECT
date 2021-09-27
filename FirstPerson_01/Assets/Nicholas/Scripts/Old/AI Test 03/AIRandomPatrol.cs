using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIRandomPatrol : MonoBehaviour
{
    [SerializeField] private bool patrolWaiting;
    [SerializeField] private float totalWaitTime = 2.0f;

    private NavMeshAgent _navMeshAgent;
    private ConnectedWaypoint _currentWaypoint;
    private ConnectedWaypoint _previousWaypoint;

    private bool travelling;
    private bool waiting;
    private float waitTimer;
    private int waypointsVisited;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.LogError("No nav mesh agent on " + gameObject.name);
        }
        else
        {
            if (_currentWaypoint == null)
            {
                GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                if (allWaypoints.Length > 0)
                {
                    while (_currentWaypoint == null)
                    {
                        int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                        ConnectedWaypoint startingWaypoint = allWaypoints[random].GetComponent<ConnectedWaypoint>();

                        if (startingWaypoint != null)
                        {
                            _currentWaypoint = startingWaypoint;
                        }
                    }
                }
                else
                {
                    Debug.LogError("Cannot find waypoints to use in scene");
                }
            }

            SetDestination();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (travelling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            travelling = false;
            waypointsVisited++;

            if (patrolWaiting)
            {
                waiting = true;
                waitTimer = 0.0f;
            }
            else
            {
                SetDestination();
            }
        }

        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= totalWaitTime)
            {
                waiting = false;

                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (waypointsVisited > 0)
        {
            ConnectedWaypoint nextWaypoint = _currentWaypoint.NextWaypoint(_previousWaypoint);
            _previousWaypoint = _currentWaypoint;
            _currentWaypoint = nextWaypoint;
        }

        Vector3 targetVector = _currentWaypoint.transform.position;
        _navMeshAgent.SetDestination(targetVector);
        travelling = true;
    }
}
