using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : StateMachineBehaviour
{
    // all variables that are currently being used for flocking
    [Header("Boid Variables")]
    public float minSpeed = 2;
    public float maxSpeed = 5;
    public float perceptionRadius = 2.5f;
    public float avoidanceRadius = 1;
    public float maxSteerForce = 3;

    public float alignWeight = 1;
    public float cohesionWeight = 1;
    public float seperateWeight = 1;

    [Header("Target Settings")]
    public float detectionRange = 10.0f;

    // for collision purposes
    [Header("Collisions")]
    public LayerMask obstacleMask;
    public float boundsRadius = .27f;
    public float avoidCollisionWeight = 10;
    public float collisionAvoidDst = 5;

    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;

    [HideInInspector]
    public Vector3 acceleration;
    [HideInInspector]
    public Vector3 avgFlockHeading;
    [HideInInspector]
    public Vector3 avgAvoidanceHeading;
    [HideInInspector]
    public Vector3 centreOfFlockmates;
    [HideInInspector]
    public int numPerceivedFlockmates;

    Material material;
    Transform cachedTransform;  // this boid
    Transform target;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        material = animator.transform.GetComponentInChildren<MeshRenderer>().material;
        cachedTransform = animator.transform;

        position = cachedTransform.position;
        forward = cachedTransform.forward;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        float startSpeed = (minSpeed + maxSpeed) / 2;
        velocity = animator.transform.forward * startSpeed;

        //animator.GetComponent<Animator>().SetBool("ToChase", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        acceleration = Vector3.zero;       

        FlockState();

        if (target != null && IsInRange())
        {
            animator.GetComponent<Animator>().SetBool("ToChase", true);
        }
    }

    void FlockState()
    {
        if (IsHeadingForCollision())
        {
            Vector3 collisionAvoidDir = ObstacleRays();
            Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        if (numPerceivedFlockmates != 0)
        {
            centreOfFlockmates /= numPerceivedFlockmates;

            Vector3 offsetToFlockmatesCentre = (centreOfFlockmates - position);

            var alignmentForce = SteerTowards(avgFlockHeading) * alignWeight;
            var cohesionForce = SteerTowards(offsetToFlockmatesCentre) * cohesionWeight;
            var seperationForce = SteerTowards(avgAvoidanceHeading) * seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }       

        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        velocity = dir * speed;

        if (cachedTransform != null)
        {
            cachedTransform.position = position;
            cachedTransform.position += velocity * Time.deltaTime;
            cachedTransform.forward = dir;
            position = cachedTransform.position;
            forward = dir;
        }
    }

    bool IsHeadingForCollision()
    {
        RaycastHit hit;
        if (Physics.SphereCast(position, boundsRadius, forward, out hit, collisionAvoidDst, obstacleMask))
        {
            return true;
        }
        else { }
        return false;
    }

    Vector3 ObstacleRays()
    {
        Vector3[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Vector3 dir = cachedTransform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(position, dir);
            if (!Physics.SphereCast(ray, boundsRadius, collisionAvoidDst, obstacleMask))
            {
                return dir;
            }
        }

        return forward;
    }

    Vector3 SteerTowards(Vector3 vector)
    {
        Vector3 v = vector.normalized * maxSpeed - velocity;
        return Vector3.ClampMagnitude(v, maxSteerForce);
    }

    public bool IsInRange()
    {
        if (Vector3.Distance(position, target.transform.position) <= detectionRange)
        {
            return true;
        }

        return false;
    }
}
