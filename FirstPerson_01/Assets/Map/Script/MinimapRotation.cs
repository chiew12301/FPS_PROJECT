using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinimapRotation : MonoBehaviour
{
    public Transform playerTransform;
    public float offset_y = 10f;

    void Update()
    {
        transform.position = playerTransform.position + Vector3.up * offset_y;
    }
}
