using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    //public float maxHealth = 50f;
    public float currentHealth;

    private EnemyCombat combat;

    private void Start()
    {
        combat = GetComponent<EnemyCombat>();
    }

    private void Update()
    {
        currentHealth = EnemyController.instance.currHealth;
    }

    public void TakeDamage(float damage)
    {
        EnemyController.instance.currHealth -= damage;

        if (currentHealth <= 0f)
        {
            combat.Die();
        }
    }
}
