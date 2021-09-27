using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : MonoBehaviour
{
    [Header("Boid Variables")]
    public float minSpeed = 2;
    public float maxSpeed = 5;
    public float perceptionRadius = 2.5f;
    public float avoidanceRadius = 1;
    public float maxSteerForce = 3;

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

    Material material;
    Transform cachedTransform;

    // Start is called before the first frame update
    void Awake()
    {
        material = transform.GetComponentInChildren<MeshRenderer>().material;
        cachedTransform = transform;
    }

    private void Start()
    {
        position = cachedTransform.position;
        forward = cachedTransform.forward;

        float startSpeed = maxSpeed;
        velocity = transform.forward * startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
