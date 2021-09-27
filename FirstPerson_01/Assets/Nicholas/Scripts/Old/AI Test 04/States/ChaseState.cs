//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;

//namespace Assets.Nicholas.Scripts.AI_Test_04.States
//{
//    [CreateAssetMenu(fileName = "ChaseState", menuName = "FSM/States/Chase", order = 3)]
//    public class ChaseState : AbstractFSMState
//    {
//        [SerializeField] private float moveSpeed = 1.0f;

//        public override void OnEnable()
//        {
//            base.OnEnable();
//            StateType = FSMStateType.CHASE;
//        }

//        public override bool EnterState()
//        {
//            Debug.Log("test test test");
//            EnteredState = false;
//            if (base.EnterState())
//            {
//                if (target == null)
//                {
//                    Debug.LogError("No target found");
//                }
//                else
//                {
//                    var direction = target.transform.position - ai.transform.position;
//                    ai.transform.Translate(direction * Time.deltaTime * moveSpeed);
//                    EnteredState = true;
//                    Debug.Log("Entered Chase State");
//                }
//            }

//            return EnteredState;
//        }

//        public override void UpdateState()
//        {
//            //if (EnteredState)
//            //{
//            //    if (Vector3.Distance(ai.transform.position, target.transform.position) >= distance)
//            //    {
//            //        fsm.EnterState(FSMStateType.IDLE);
//            //    }
//            //}
//        }

//        public override bool ExitState()
//        {
//            base.ExitState();
//            Debug.Log("Exiting Chase State");

//            return true;
//        }
//    }
//}
