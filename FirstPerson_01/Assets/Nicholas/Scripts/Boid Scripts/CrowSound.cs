using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowSound : MonoBehaviour
{
    Flocking boid;

    // Start is called before the first frame update
    void Start()
    {
        boid = GetComponent<Flocking>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boid.IsInRange())
        {
            Debug.Log("Player in range. Playing Crow sound");
            if (AudioManager.instance.FindIsPlaying("Crow", "SFX") == false)
            {
                AudioManager.instance.Play("Crow", "SFX");
            }
        }
    }
}
