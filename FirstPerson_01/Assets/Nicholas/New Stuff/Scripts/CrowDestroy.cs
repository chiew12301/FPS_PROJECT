using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowDestroy : StateMachineBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject parentTransform;

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);
        GameObject cloneExplosionPrefab = Instantiate(explosionPrefab, animator.transform.position, animator.transform.rotation);
        Destroy(cloneExplosionPrefab, 3);
    }
}
