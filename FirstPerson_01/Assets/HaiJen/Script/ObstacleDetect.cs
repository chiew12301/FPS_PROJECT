using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetect : MonoBehaviour
{
    public bool obstruction;
    public GameObject obj;
    private Collider colnow;

    void OnTriggerStay(Collider other)
    {
        if(!obstruction)
        {
            if (other.CompareTag("Wall"))
            {
                obstruction = true;
                obj = other.gameObject;
                colnow = other;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other == colnow)
        {
            obstruction = false;
        }
    }
}
