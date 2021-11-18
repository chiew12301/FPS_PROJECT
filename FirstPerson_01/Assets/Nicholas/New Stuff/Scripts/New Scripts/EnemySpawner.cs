using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject meteor;
    [SerializeField] private int numOfEnemy = 1;
    [SerializeField] private float spawnRadius = 1.0f;
    [SerializeField] private List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        Vector3 center = transform.position;

        for (int i = 0; i < numOfEnemy; i++)
        {
            Vector3 pos = RandomCircle(center, spawnRadius);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            enemies.Add(Instantiate(enemyPrefab, pos, rot, transform));
        }
    }

    private void Update()
    {
        // check for enemies if theyre still alive
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<EnemyController>().currHealth <= 0)
            {
                enemies.RemoveAt(i);
            }
        }

        if (enemies.Count <= 0)
        {
            // enable meteor
            meteor.SetActive(true);
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
