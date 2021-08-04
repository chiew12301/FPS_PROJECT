using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointDistance : MonoBehaviour
{
    public GameObject target;
    public GameObject Text;
    public RectTransform waypoint;
    private Transform player;
    private Text distanceText;

    private Vector3 offset = new Vector3(0, 1.25f, 0);

    // Start is called before the first frame update
    void Start()
    {
        distanceText = Text.GetComponent<Text>();

        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Gameobject in worldspace, UIobject in screenspace
        var screenPos = Camera.main.WorldToScreenPoint(target.transform.position + offset);

        if (player.GetComponent<Objectives>().CurrentObjective != null)
        {
            target = player.GetComponent<Objectives>().CurrentObjective.Target;

            //gameObject.SetActive(true);

            waypoint.position = screenPos;
            waypoint.gameObject.SetActive(screenPos.z > 0);

            distanceText.text = Vector3.Distance(player.position, target.transform.position).ToString("0") + "m";
        }
        else
        {
            waypoint.gameObject.SetActive(false);
        }


        //this.transform.position = screenPos;
        //this.gameObject.SetActive(screenPos.z > 0);

        
    }
}
