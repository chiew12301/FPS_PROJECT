using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Nicholas.Scripts.AI_Test_04.States
{
    [CreateAssetMenu(fileName = "IdleState", menuName = "FSM/States/Idle", order = 1 )]
    public class IdleState : AbstractFSMState
    {
        [SerializeField] private float idleDuration = 2f;
        private float totalDuration;

        //private Player target;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.IDLE;
        }

        public override bool EnterState()
        {
            base.EnterState();

            if (EnteredState)
            {
                Debug.Log("Entered Idle State");
                totalDuration = 0.0f;
            }

            EnteredState = true;
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                totalDuration += Time.deltaTime;
                Debug.Log("Updating Idle State");

                //if (Vector3.Distance(ai.transform.position, target.transform.position) <= distance)
                //{
                //    fsm.EnterState(FSMStateType.CHASE);
                //}
                //else
                //{
                    if (totalDuration >= idleDuration)
                    {
                        fsm.EnterState(FSMStateType.PATROL);
                    }
                //}               
            }
        }

        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("Exiting Idle State");

            return true;
        }
    }
}
