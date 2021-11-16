using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyCollider")
        {
            Debug.Log("IM BEING HIT");
            // do damage here
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle Hit");
    }
}
