using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Nicholas.Scripts.AI_Test_04
{
    public class FiniteStateMachine: MonoBehaviour
    {
        [SerializeField] private AbstractFSMState _startState;        
        private AbstractFSMState _currState;

        public void Awake()
        {
            _currState = null;
        }

        public void Start()
        {
            if (_startState != null)
            {
                EnterState(_startState);
            }
        }

        public void Update()
        {
            if (_currState != null)
            {
                _currState.UpdateState();
            }
        }

        #region Manage State

        public void EnterState(AbstractFSMState nextState)
        {
            if (nextState == null)
            {
                return;
            }

            _currState = nextState;
            _currState.EnterState();
        }

        #endregion
    }
}
