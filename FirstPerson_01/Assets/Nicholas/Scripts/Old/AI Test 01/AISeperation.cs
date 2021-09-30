using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISeperation : MonoBehaviour
{
    private GameObject[] AIObj;
    private float space = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        AIObj = GameObject.FindGameObjectsWithTag("AI");
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject go in AIObj)
        {
            if (go != gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, this.transform.position);
                if (distance <= space)
                {
                    Vector3 dir = transform.position - go.transform.position;
                    transform.Translate(dir * Time.deltaTime);
                }
            }
        }
    }
}
