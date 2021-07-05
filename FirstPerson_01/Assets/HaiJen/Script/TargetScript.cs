using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }
    
    void Die()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
