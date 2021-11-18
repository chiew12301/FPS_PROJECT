using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableLocation : MonoBehaviour
{
    public GameObject[] spawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < spawnLocations.Length; i++)
            {
                spawnLocations[i].SetActive(true);
            }
        }
    }
}
