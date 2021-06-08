using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Animator animator;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
