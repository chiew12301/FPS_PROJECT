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
        if(player.GetComponent<Objectives>().CurrentObjective != null)
        {
            target = player.GetComponent<Objectives>().CurrentObjective.Target;

            this.gameObject.SetActive(true);

            this.transform.position = new Vector3(target.transform.position.x, 250, target.transform.position.z);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
     
        
    }
}
