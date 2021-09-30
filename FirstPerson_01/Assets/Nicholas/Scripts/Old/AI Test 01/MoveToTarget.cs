using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform target;

    private float space = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) >= space)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir * Time.deltaTime);
        }
    }
}
