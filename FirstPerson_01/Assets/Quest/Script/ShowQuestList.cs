using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowQuestList : MonoBehaviour
{
    private Canvas QuestCanvas;

    void Start()
    {
        QuestCanvas = GetComponent<Canvas>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(Input.GetKeyUp(KeyCode.Q))
        {
            QuestCanvas.enabled = !QuestCanvas.enabled;
        }
    }
}
