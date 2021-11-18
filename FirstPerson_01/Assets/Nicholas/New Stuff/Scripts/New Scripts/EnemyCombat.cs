using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float attackSpeed = 1.0f;
    [SerializeField] private GameObject explosionPrefab;

    private float attackCooldown = 0.0f;
    private float currHealth;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        currHealth = EnemyController.instance.currHealth;
        attackCooldown -= Time.deltaTime;
    }

    public void Attack()
    {
        if (animator != null)
        {
            if (currHealth >= EnemyController.instance.maxHealth / 2.0f)
            {
                ClawAttack();
            }
            else
            {
                StartCoroutine("SuicideAttack"); 
            }
        }
    }

    public void Die()
    {
        if (animator != null)
        {
            animator.Play("DeathAnim");
        }
    }

    void ClawAttack()
    {
        if (attackCooldown <= 0)
        {
            // do damage
            Debug.Log("Hitting Player");
            animator.Play("ClawAttackAnim");
            //animator.SetTrigger("Attack");
            attackCooldown = 1f / attackSpeed;  // higher attack speed, lower attack cooldown
        }
    }

    IEnumerator SuicideAttack()
    {
        animator.Play("CrowSuicide");
        yield return new WaitForSeconds(1f);
        Die();

        GameObject cloneExplosionPrefab = Instantiate(explosionPrefab);
        cloneExplosionPrefab.transform.position = transform.position + transform.up + transform.forward;
        Destroy(cloneExplosionPrefab, 2);
    }
}
