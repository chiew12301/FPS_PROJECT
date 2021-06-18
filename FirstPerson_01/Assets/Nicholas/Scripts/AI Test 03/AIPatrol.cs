using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrol : MonoBehaviour
{
    [SerializeField] private bool patrolWaiting;
    [SerializeField] private float totalWaitTime = 2.0f;
    [SerializeField] private float switchProbability = 0.3f;
    [SerializeField] private List<Waypoint> patrolPoints;

    private NavMeshAgent _navMeshAgent;
    private int currentPatrolIndex;
    private bool travelling;
    private bool waiting;
    private bool patrolForward;
    private float waitTimer;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.LogError("No nav mesh agent on " + gameObject.name);
        }
        else
        {
            if (patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Not enough patrol points");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (travelling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            travelling = false;
            if (patrolWaiting)
            {
                waiting = true;
                waitTimer = 0.0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= totalWaitTime)
            {
                waiting = false;
                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (patrolPoints != null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            travelling = true;
        }
    }

    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0.0f, 1.0f) <= switchProbability)
        {
            patrolForward = !patrolForward;
        }

        if (patrolForward)
        {
            currentPatrolIndex++;
            if (currentPatrolIndex >= patrolPoints.Count)
            {
                currentPatrolIndex = 0;
            }

            //currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        else
        {
            currentPatrolIndex--;
            if (currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }

            //if (--currentPatrolIndex < 0)
            //{
            //    currentPatrolIndex = patrolPoints.Count - 1;
            //}
        }
    }
}
