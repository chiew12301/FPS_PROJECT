using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotateIcon : MonoBehaviour
{
    public float lockPos;

    void Update()
    {
        // Locks the rotation.
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lockPos, lockPos);
    }

}
