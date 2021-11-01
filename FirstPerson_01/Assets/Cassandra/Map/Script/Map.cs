using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Cutscene cs_Obj;
    private Canvas MapCanvas;

    // Start is called before the first frame update
    void Start()
    {
        MapCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.instance.getUISTATE() == PAUSEUI.NONEPAUSE || PauseManager.instance.getUISTATE() == PAUSEUI.MAPUI)
        {
            if (MainMenu.instance.getMainMenuStatus() == false && cs_Obj.GetIsCutscene() == false)
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    if (PauseManager.instance.getIsPause() == true)
                    {
                        PauseManager.instance.setIsPause(false);
                        MapCanvas.enabled = false;

                        PauseManager.instance.ChangeUISTATE(PAUSEUI.NONEPAUSE);
                    }
                    else
                    {
                        PauseManager.instance.setIsPause(true);
                        MapCanvas.enabled = true;

                        PauseManager.instance.ChangeUISTATE(PAUSEUI.MAPUI);
                    }
                }
            }
        }

    }


            

}
            

