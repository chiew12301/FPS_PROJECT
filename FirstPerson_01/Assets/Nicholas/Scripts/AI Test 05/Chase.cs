using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : NPCBaseFSM
{
    [SerializeField] private float chaseSpeed = 10.0f;

    private Transform target;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        target = player.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //var direction = player.transform.position - NPC.transform.position;
        //NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime);
        //NPC.transform.Translate(direction * Time.deltaTime);
        ChasePlayer();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private void ChasePlayer()
    {
        if (target != null)
        {
            Vector3 targetVector = target.transform.position;
            navMeshAgent.SetDestination(targetVector);
            navMeshAgent.speed = chaseSpeed;
        }
    }
}
