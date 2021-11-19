using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    public new Light light;
    //public GameObject lightBulb;
    //Renderer lightBulbMat;
    public float minIntensity = 0.0f;
    public float maxIntensity = 1.0f;
    public int smoothing = 5;

    Queue<float> smoothQueue;
    float lastSum = 0;

    public void Reset()
    {
        smoothQueue.Clear();
        lastSum = 0;
    }
    void Start()
    {
        smoothQueue = new Queue<float>(smoothing);

        if(light == null)
        {
            light = GetComponent<Light>();
        }
        //lightBulbMat = lightBulb.GetComponent<Renderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(light == null)        
            return;
        
        while(smoothQueue.Count >= smoothing)
        {
            lastSum -= smoothQueue.Dequeue();
        }

        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        light.intensity = lastSum / (float)smoothQueue.Count;
        /*
        if(newVal <= maxIntensity/2)
        {
            lightBulbMat.material.SetColor("_EmissionColor", Color.black);
        }
        else
        {
            lightBulbMat.material.SetColor("_EmissionColor", Color.yellow);
        }
        */
    }
   
}
