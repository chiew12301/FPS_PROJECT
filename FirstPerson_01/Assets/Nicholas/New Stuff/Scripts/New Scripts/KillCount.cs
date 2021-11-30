using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    public int totalKillCount;

    #region Singleton
    public static KillCount instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (totalKillCount == 5)
        {
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyCollider"))
        {
            Debug.Log("BEING HIT RN");
        }
    }
}
