﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    BoidSettings settings;

    // State
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;
    bool isIdle;

    // To update:
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

    // Cached
    Material material;
    Transform cachedTransform;  // this boid 
    Transform target;

    void Awake()
    {
        material = transform.GetComponentInChildren<MeshRenderer>().material;
        cachedTransform = transform;
        isIdle = false;
    }

    public void Initialize (BoidSettings settings, Transform target) {
        this.target = target;
        this.settings = settings;

        position = cachedTransform.position;
        forward = cachedTransform.forward;

        float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;
        velocity = transform.forward * startSpeed;
    }

    public void SetColour (Color col) {
        if (material != null) {
            material.color = col;
        }
    }

    public void UpdateBoid () {
        acceleration = Vector3.zero;

        if (target != null && IsInRange())
        {
            SetColour(Color.yellow);
            Vector3 offsetToTarget = (target.position - position);
            //Debug.DrawRay(position, offsetToTarget, Color.black);
            acceleration += SteerTowards(offsetToTarget) * settings.targetWeight;

            if (IsInAttackRange())
            {
                SetColour(Color.red);
            }
        }
        else
        {
            SetColour(Color.blue);
        }       

        if (numPerceivedFlockmates != 0) {
            centreOfFlockmates /= numPerceivedFlockmates;

            Vector3 offsetToFlockmatesCentre = (centreOfFlockmates - position);

            var alignmentForce = SteerTowards (avgFlockHeading) * settings.alignWeight;
            var cohesionForce = SteerTowards (offsetToFlockmatesCentre) * settings.cohesionWeight;
            var seperationForce = SteerTowards (avgAvoidanceHeading) * settings.seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        if (IsHeadingForCollision ()) {
            Vector3 collisionAvoidDir = ObstacleRays ();
            Vector3 collisionAvoidForce = SteerTowards (collisionAvoidDir) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        //var offsetToCentre = Vector3.Distance(settings.boundTarget.transform.position, position);
        ////Debug.Log(offsetToCentre);
        ////Debug.DrawRay(settings.boundTarget.transform.position, position, Color.black);
        //if (offsetToCentre >= 10)
        //{
        //    Debug.Log("too far");
        //    Debug.DrawRay(position, settings.boundTarget.transform.position - position, Color.black);
        //    acceleration += move;
        //}
        if (position.y < settings.yMinMax.x || position.y > settings.yMinMax.y)
        {
            var newPos = position;
            newPos.y = Mathf.Clamp(newPos.y, settings.yMinMax.x, settings.yMinMax.y);
            position = newPos;           
        }

        //Debug.Log("Pos Y = " + position.y + ". Cached Y = " + cachedTransform.position.y);

        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp (speed, settings.minSpeed, settings.maxSpeed);
        velocity = dir * speed;

        //RandomIdle();

        if (cachedTransform != null && isIdle == false)
        {         
            cachedTransform.position = position;
            cachedTransform.position += velocity * Time.deltaTime;
            cachedTransform.forward = dir;                     
            position = cachedTransform.position;
            forward = dir;
        }
        else
        {
            velocity = Vector3.zero;
            position += velocity * Time.deltaTime;
            
        }       
    }

    bool IsHeadingForCollision () {
        RaycastHit hit;
        if (Physics.SphereCast (position, settings.boundsRadius, forward, out hit, settings.collisionAvoidDst, settings.obstacleMask)) {
            return true;
        } else { }
        return false;
    }

    bool IsInRange()
    {
        if (Vector3.Distance(position, target.position) <= settings.detectionRange)
        {
            return true;
        }

        return false;
    }

    Vector3 ObstacleRays () {
        Vector3[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++) {
            Vector3 dir = cachedTransform.TransformDirection (rayDirections[i]);
            Ray ray = new Ray (position, dir);
            if (!Physics.SphereCast (ray, settings.boundsRadius, settings.collisionAvoidDst, settings.obstacleMask)) {
                return dir;
            }
        }

        return forward;
    }

    Vector3 SteerTowards (Vector3 vector) {
        Vector3 v = vector.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude (v, settings.maxSteerForce);
    }   

    public bool RandomIdle()
    {
        int randomInt = (Random.Range(0, 50));

        if (randomInt == 2)
        {
            return isIdle = true;
        }
        else
        {
            return isIdle = false;
        }      
    }    

    public bool IsInAttackRange()
    {
        if (Vector3.Distance(target.position, position) <= settings.attackRange)
        {
            return true;
        }

        return false;
    }

    public bool IsIdle
    {
        get
        {
            return this.isIdle;
        }

        set
        {
            isIdle = value;
        }
    }
}