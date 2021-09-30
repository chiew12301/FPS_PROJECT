using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crow : MonoBehaviour
{
    [Header("Crow Values")]
    [SerializeField] private float currHealth;
    //[SerializeField] private float maxHealth;
    private TargetScript targetScript;

    [Header("Target Values")]
    [SerializeField] private GameObject target;
    [SerializeField] private float targetRange;

    [Header("Behaviour Values")]
    [SerializeField] private float wanderRadius;
    [SerializeField] private float wanderTimer;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float attackInterval;
    [SerializeField] private float suicideTimer;

    NavMeshAgent navMeshAgent;
    Animator animator;
    float timer;
    float sTimer;
    bool isChasing = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        targetScript = GetComponentInChildren<TargetScript>();    

        if (navMeshAgent == null)
            Debug.LogError("No nav mesh agent on " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    childCrowObject.position = transform.forward;
        //}

        currHealth = targetScript.currentHealth;

        if (animator != null)
        {
            #region WANDER BEHAVIOUR    
            timer += Time.deltaTime;

            if (timer >= wanderTimer && isChasing == false)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                //transform.LookAt(newPos);
                navMeshAgent.SetDestination(newPos);
                timer = 0f;
            }
            #endregion

            #region CHASE AND ATTACK BEHAVIOUR
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance <= targetRange)
            {
                isChasing = true;
                transform.LookAt(target.transform);
                navMeshAgent.SetDestination(target.transform.position);

                if (distance <= stoppingDistance)
                {
                    Debug.Log("Crow Current Health : " + currHealth);
                    Debug.Log("Crow Max Health : " + targetScript.maxHealth);

                    navMeshAgent.isStopped = true;

                    // check to see which attack to use
                    // if > 50% hp, use claw attack
                    if (!IsCurrHealthLessThanHalf())
                    {
                        StartCoroutine("Attack");
                    }                        // if < 50% hp, suicide
                    else if (IsCurrHealthLessThanHalf())
                    {
                        animator.SetBool("IsInRange", false);
                        StartCoroutine("Suicide");
                    }
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
            #endregion
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    currHealth = 2;
        //}
    }
    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private bool IsCurrHealthLessThanHalf()
    {
        if (currHealth <= targetScript.maxHealth / 2)
        {
            return true;
        }
        return false;
    }

    IEnumerator Attack()
    {
        animator.SetBool("IsInRange", true);

        yield return new WaitForSecondsRealtime(attackInterval);
    }

    IEnumerator Suicide()
    {
        animator.SetBool("IsSuicide", true);
        targetRange = 100f;
        //sTimer += Time.deltaTime;

        //if (sTimer >= suicideTimer)
        //{
        //    // explode
        //    GameObject.Instantiate(explosionPrefab, transform.localPosition, transform.rotation);
        //    Destroy(explosionPrefab, 2);
        //    //Destroy(gameObject);
        //}

        yield return new WaitForSeconds(0);
    }

}
