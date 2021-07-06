using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private Canvas MapCanvas;

    // Start is called before the first frame update
    void Start()
    {
        MapCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            //MapCanvas.enabled = !MapCanvas.enabled;

            if(PauseManager.instance.getIsPause() == true)
            {
                PauseManager.instance.setIsPause(false);
                MapCanvas.enabled = false;
            }
            else
            {
                PauseManager.instance.setIsPause(true);
                MapCanvas.enabled = true;
            }

        }

    }
}
