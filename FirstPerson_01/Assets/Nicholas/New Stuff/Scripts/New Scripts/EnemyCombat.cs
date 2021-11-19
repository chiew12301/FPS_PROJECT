using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float attackSpeed = 1.0f;
    [SerializeField] private GameObject explosionPrefab;

    private float attackCooldown = 0.0f;
    private bool hasPlayed;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        hasPlayed = false;
    }

    private void Update()
    {
        //currHealth = EnemyController.instance.currHealth;
        attackCooldown -= Time.deltaTime;
    }

    public void Attack01()
    {
        if (animator != null)
        {
            ClawAttack();
        }
    }

    public void Attack02()
    {
        if (animator != null)
        {
            if (!hasPlayed)
            {
                StartCoroutine(SuicideAttack());
                hasPlayed = true;
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

        GameObject cloneExplosionPrefab = Instantiate(explosionPrefab, transform);
        cloneExplosionPrefab.transform.position = transform.position + transform.up + transform.forward;
        Destroy(cloneExplosionPrefab, 2);
    }
}
