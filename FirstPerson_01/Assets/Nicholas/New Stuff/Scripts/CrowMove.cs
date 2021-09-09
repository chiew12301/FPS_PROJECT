using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float targetRange;
    [SerializeField] private float wanderRadius;
    [SerializeField] private float wanderTimer;
    [SerializeField] private float stoppingDistance;

    NavMeshAgent navMeshAgent;
    Animator animator;
    float timer;
    bool isChasing = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        timer = wanderTimer;

        if (navMeshAgent == null)
            Debug.LogError("No nav mesh agent on " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer && isChasing == false)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            //transform.LookAt(newPos);
            navMeshAgent.SetDestination(newPos);
            timer = 0f;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= targetRange)
        {
            isChasing = true;
            transform.LookAt(target);
            navMeshAgent.SetDestination(target.position);

            if (distance <= stoppingDistance)
            {
                navMeshAgent.isStopped = true;
                animator.SetBool("IsInRange", true);
            }                            
            else
            {
                navMeshAgent.isStopped = false;
                animator.SetBool("IsInRange", false);
            }
            
        }
        else
        {
            isChasing = false;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
