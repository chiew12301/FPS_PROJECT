using Assets.Nicholas.Scripts.AI_Test_04;
using Assets.Nicholas.Scripts.AI_Test_04.AICode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ExecutionState
{
    NONE = 0,
    ACTIVE,
    COMPLETED,
    TERMINATED,
};

public enum FSMStateType
{
    IDLE = 0,
    PATROL,
    CHASE,
};

public abstract class AbstractFSMState : ScriptableObject
{
    protected NavMeshAgent navMeshAgent;
    protected AI ai;
    protected FiniteStateMachine fsm;
    protected float distance = 5.0f;

    public ExecutionState ExecutionState { get; protected set; }
    public FSMStateType StateType { get; protected set; }
    public bool EnteredState { get; protected set; }

    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
    }

    public virtual bool EnterState()
    {
        bool successNavMesh = true;
        bool successAI = true;
        ExecutionState = ExecutionState.ACTIVE;
        successNavMesh = (navMeshAgent != null);

        successAI = (ai != null);

        return successNavMesh & successAI;
    }

    public abstract void UpdateState(); 

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    public virtual void SetNavMeshAgent(NavMeshAgent _navMeshAgent)
    {
        if (_navMeshAgent != null)
        {
            navMeshAgent = _navMeshAgent;
        }
    }

    public virtual void SetExecutingFSM(FiniteStateMachine _fsm)
    {
        if (_fsm != null)
        {
            fsm = _fsm;
        }
    }

    public virtual void SetExecutingAI(AI _ai)
    {
        if (_ai != null)
        {
            ai = _ai;
        }
    }
}
