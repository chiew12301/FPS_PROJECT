using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBaseFSM : StateMachineBehaviour
{
    private protected GameObject NPC;
    private protected GameObject player;

    private protected NavMeshAgent navMeshAgent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        player = NPC.GetComponent<NPC>().GetPlayer();
        navMeshAgent = NPC.GetComponent<NavMeshAgent>();
    }
}
