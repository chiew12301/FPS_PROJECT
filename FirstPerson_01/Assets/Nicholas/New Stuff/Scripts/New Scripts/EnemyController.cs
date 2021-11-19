using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float lookRadius = 10.0f;

    Transform target;
    NavMeshAgent agent;
    EnemyCombat combat;
    float timer;
    bool isChasingPlayer = false;

    [Header("Enemy Values")]
    public float maxHealth = 50f;
    //[HideInInspector]
    public float currHealth;
    [SerializeField] private float wanderTimer = 1.0f;
    [SerializeField] private float wanderRadius = 1.0f;

    #region Singleton

    public static EnemyController instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<EnemyCombat>();

        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent != null)
        {
            // wander
            timer += Time.deltaTime;
            if (timer >= wanderTimer && isChasingPlayer == false)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0.0f;
            }

            // chase
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {
                isChasingPlayer = true;
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance)
                {
                    // attack the player
                    if (currHealth <= maxHealth / 2)
                    {
                        // suicide attack
                        combat.Attack02();
                    }
                    else
                    {
                        // claw attack
                        combat.Attack01();
                        LookAtPlayer();
                    }                  
                }
            }
            else
            {
                isChasingPlayer = false;
            }

            CheckIsDead();
        }            
    }

    public void CheckIsDead()
    {
        if (currHealth <= 0)
        {
            // die
            combat.Die();
            agent.isStopped = true;
            Destroy(agent);
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);

            GetComponent<AudioSource>().enabled = false;
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
