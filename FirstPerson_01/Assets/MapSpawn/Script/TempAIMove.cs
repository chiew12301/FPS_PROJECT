using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAIMove : MonoBehaviour
{
    private int randNum = 0;
    private float p_Speed = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        randNum = Random.Range(0, 4);
        if(randNum == 0)
        {
            transform.position += transform.forward * Time.deltaTime * p_Speed;
        }
        else if(randNum == 1)
        {
            transform.position -= transform.forward * Time.deltaTime * p_Speed;
        }
        else if(randNum == 2)
        {
            transform.position -= transform.right * Time.deltaTime * p_Speed;
        }
        else
        {
            transform.position += transform.right * Time.deltaTime * p_Speed;
        }
    }
}
