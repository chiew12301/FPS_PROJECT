using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int numOfEnemy = 1;
    [SerializeField] private float spawnRadius = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 center = transform.position;

        for (int i = 0; i < numOfEnemy; i++)
        {
            Vector3 pos = RandomCircle(center, spawnRadius);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            Instantiate(enemyPrefab, pos, rot);
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
