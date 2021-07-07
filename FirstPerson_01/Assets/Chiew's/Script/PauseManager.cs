using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PAUSEUI
{
    INVENTORYUI = 0,
    SETTINGUI,
    MAPUI,
    NONEPAUSE
}

public class PauseManager : MonoBehaviour
{
    #region SINGLETONS_PAUSE
    public static PauseManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance is creating, Inventory!");
            return;
        }
        instance = this;
    }

    #endregion SINGLETONS_PAUSE

    private PAUSEUI UISTAT = PAUSEUI.NONEPAUSE; //change this

    private bool isPause = false;
    private bool INV_PAUSE = false;
    private bool SET_PAUSE = false;
    private bool MM_PAUSE = false;


    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
    }

    public bool getIsPause()
    {
        return isPause;
    }

    public void ChangeUISTATE(PAUSEUI uistate)
    {
        UISTAT = uistate;
    }

    public PAUSEUI getUISTATE()
    {
        return UISTAT;
    }

    public void setIsPause(bool status)
    {
        isPause = status;
        if(isPause == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0.000001f; //or 0.0f
        }
    }

}
