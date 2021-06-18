using Assets.Nicholas.Scripts.AI_Test_04.AICode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Nicholas.Scripts.AI_Test_04
{
    public class FiniteStateMachine: MonoBehaviour
    {   
        private AbstractFSMState _currState;

        [SerializeField] private List<AbstractFSMState> _validStates;
        private Dictionary<FSMStateType, AbstractFSMState> _fsmStates;

        public void Awake()
        {
            _currState = null;
            _fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();
            NavMeshAgent navMeshAgent = this.GetComponent<NavMeshAgent>();
            AI ai = this.GetComponent<AI>();

            foreach (AbstractFSMState state in _validStates)
            {
                state.SetExecutingFSM(this);
                state.SetExecutingAI(ai);
                state.SetNavMeshAgent(navMeshAgent);
                _fsmStates.Add(state.StateType, state);
            }
        }

        public void Start()
        {
            EnterState(FSMStateType.IDLE);
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

            if (_currState != null)
            {
                _currState.ExitState();
            }

            _currState = nextState;
            _currState.EnterState();
        }

        public void EnterState(FSMStateType stateType)
        {
            if (_fsmStates.ContainsKey(stateType))
            {
                AbstractFSMState nextState = _fsmStates[stateType];

                EnterState(nextState);
            }
        }

        #endregion
    }
}
