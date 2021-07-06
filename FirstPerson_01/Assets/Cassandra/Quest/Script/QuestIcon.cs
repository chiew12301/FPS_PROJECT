using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    public GameObject target;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        target = player.GetComponent<Objectives>().CurrentObjective.Target;
        //this.transform.position = target.transform.position;

        this.transform.position = new Vector3(target.transform.position.x, 168, target.transform.position.z);
    }
}
