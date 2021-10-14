using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation2 : MonoBehaviour
{
    public Transform playerTransform;
    public float offset_y = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + Vector3.up * offset_y;
    }
}
