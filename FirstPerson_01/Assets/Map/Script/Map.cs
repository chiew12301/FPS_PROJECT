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
            MapCanvas.enabled = !MapCanvas.enabled;

            if (Time.timeScale >= 1.0f)
            {
                Time.timeScale = 0.0001f;

                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1f;

                //Cursor.lockState = CursorLockMode.Locked;
                //Cursor.visible = false;
            }

        }

    }
}
