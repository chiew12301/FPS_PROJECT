using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] LevelLoader LV_loader;

    //public bool GameIsPaused = false;
    //public GameObject invUI;

    public GameObject pauseMenuUI;

    public Animator canvas_animator;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.instance.getUISTATE() == PAUSEUI.NONEPAUSE || PauseManager.instance.getUISTATE() == PAUSEUI.SETTINGUI)
        {
            if(PauseManager.instance.getIsPlayingAni() == false)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (PauseManager.instance.getIsPause() == true)
                    {
                        //Comment 25/09, pressing escape the cursor is not locked  and invisible, might need check in build have any problem.
                        Resume();
                    }
                    else
                    {
                        Pause();
                    }
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        PauseManager.instance.ChangeUISTATE(PAUSEUI.NONEPAUSE);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        PauseManager.instance.ChangeUISTATE(PAUSEUI.SETTINGUI);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu");
        //SceneManager.LoadScene("Main Menu UI");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseManager.instance.setIsPause(false);
        LV_loader.LoadLevel(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        PauseManager.instance.ChangeUISTATE(PAUSEUI.NONEPAUSE); //never test yet, but theory wise should be correct 25_09
        Application.Quit();
    }
}
