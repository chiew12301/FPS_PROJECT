using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Nicholas.Scripts.AI_Test_04.States
{
    [CreateAssetMenu(fileName = "PatrolState", menuName = "FSM/States/Patrol", order = 2)]
    public class PatrolState : AbstractFSMState
    {
        private ConnectedWaypoint[] connectedWaypoints;
        private int waypointIndex;

        //private Player target;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.PATROL;
            waypointIndex = -1;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                // get patrol points
                connectedWaypoints = ai.ConnectedWaypoints;

                if (connectedWaypoints == null || connectedWaypoints.Length == 0)
                {
                    Debug.LogError("Patrol State : Cannot find waypoints from AI");
                }
                else
                {
                    if (waypointIndex < 0)
                    {
                        waypointIndex = UnityEngine.Random.Range(0, connectedWaypoints.Length);
                    }
                    else
                    {
                        waypointIndex = (waypointIndex + 1) % connectedWaypoints.Length;
                    }

                    SetDestination(connectedWaypoints[waypointIndex]);
                    EnteredState = true;
                    Debug.Log("Entered Patrol State");
                }
            }
     
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                //if (Vector3.Distance(ai.transform.position, target.transform.position) <= distance)
                //{
                //    fsm.EnterState(FSMStateType.CHASE);
                //}
                //else
                //{
                    if (navMeshAgent.remainingDistance <= 1.0f) // check to see if it reach the waypoint
                    {
                        fsm.EnterState(FSMStateType.IDLE);
                    }
                //}               
            }
        }

        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("Exiting Patrol State");

            return true;
        }

        private void SetDestination(ConnectedWaypoint destination)
        {
            if (navMeshAgent != null && destination != null)
            {
                navMeshAgent.SetDestination(destination.transform.position);
            }
        }
    }
}
