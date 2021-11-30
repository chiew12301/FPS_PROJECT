using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public void EnableCollider()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = true;
    }

    public void DisableCollider()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    private void Update()
    {
        if (GetComponentInParent<EnemyController>().isDead)
        {
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
        }
    }
}
