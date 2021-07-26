using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerScript : MonoBehaviour
{
    private Image iconImage;
    private Text distanceText;

    public Transform player;
    public Transform target;
    public Camera cam;

    public float closeEnoughDist; //when player is within a certain distance, waypoint will be destroyed
    
    private void Start()
    {
        iconImage = GetComponent<Image>();
        distanceText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (target != null)
        {
            GetDistance();
            CheckOnScreen();
        }
    }

    //showing the distance
    private void GetDistance()
    {
        float dist = Vector3.Distance(player.position, target.position);
        distanceText.text = dist.ToString("f1") + "m";

        if(dist < closeEnoughDist)
        {
            Destroy(gameObject);
        }
    }

    //positioning the icon on screen when player looking at it
    private void CheckOnScreen()
    {
        float thing = Vector3.Dot((target.position - cam.transform.position).normalized, cam.transform.forward);

        if(thing <= 0)
        {
            ToggleUI(false);
        }
        else
        {
            ToggleUI(true);
            transform.position = cam.WorldToScreenPoint(target.position);
        }
    }

    private void ToggleUI(bool _value)
    {
        iconImage.enabled = _value;
        distanceText.enabled = _value;
    }
}
