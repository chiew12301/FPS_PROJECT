using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : NPCBaseFSM
{
    private ConnectedWaypoint _currentWaypoint;
    private ConnectedWaypoint _previousWaypoint;
    private int waypointIndex;
    private bool travelling;
    private float patrolSpeed = 5.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        waypointIndex = -1;       

        if (navMeshAgent == null)
        {
            Debug.LogError("No nav mesh agent");
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
            Debug.Log("going to first dest");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (travelling && navMeshAgent.remainingDistance <= 1.0f)
        {
            travelling = false;
            waypointIndex++;

            SetDestination();
            Debug.Log("going to another dest");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void SetDestination()
    {
        if (waypointIndex > 0)
        {
            ConnectedWaypoint nextWaypoint = _currentWaypoint.NextWaypoint(_previousWaypoint);
            _previousWaypoint = _currentWaypoint;
            _currentWaypoint = nextWaypoint;
        }

        Vector3 targetVector = _currentWaypoint.transform.position;
        navMeshAgent.SetDestination(targetVector);
        navMeshAgent.speed = patrolSpeed;
        travelling = true;
    }
}
