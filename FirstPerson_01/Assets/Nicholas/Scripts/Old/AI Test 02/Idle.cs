using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            Boid boid = GetComponent<Boid>();
            boid.IsIdle = true;
            Debug.Log(boid.IsIdle);
        }

        if (Input.GetKey(KeyCode.F))
        {
            Boid boid = GetComponent<Boid>();
            boid.IsIdle = false;
            Debug.Log(boid.IsIdle);
        }
    }
}
