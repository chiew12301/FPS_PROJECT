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
        public override bool EnterState()
        {
            base.EnterState();
            Debug.Log("Entered Idle State");

            return true;
        }

        public override void UpdateState()
        {
            Debug.Log("Updating Idle State");
        }

        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("Exiting Idle State");

            return true;
        }
    }
}
