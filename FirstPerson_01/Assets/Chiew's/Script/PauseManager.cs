using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
    }

    public bool getIsPause()
    {
        return isPause;
    }

    public void setIsPause(bool status)
    {
        isPause = status;
        if(isPause == false)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.000001f; //or 0.0f
        }
    }

}
