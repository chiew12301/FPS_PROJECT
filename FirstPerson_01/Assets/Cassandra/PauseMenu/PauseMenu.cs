using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] LevelLoader LV_loader;

    //public bool GameIsPaused = false;
    public GameObject invUI;

    public GameObject pauseMenuUI;

    private void Start()
    {
       // GameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseManager.instance.getIsPause() == true)
            {
                PauseManager.instance.setIsPause(false);
            }
            else
            {
                PauseManager.instance.setIsPause(true);
            }
        }

        if (PauseManager.instance.getIsPause() == true)
        {
            if(PauseManager.instance.getIsPause() == true)
            {
                pauseMenuUI.SetActive(true);
            }
            Pause();
        }
        else
        {
            if (PauseManager.instance.getIsPause() == false)
            {
                pauseMenuUI.SetActive(false);
            }
            Resume();
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseManager.instance.setIsPause(false);
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseManager.instance.setIsPause(true);
    }

    public void ResumeButton()
    {
        PauseManager.instance.setIsPause(false);
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
        Application.Quit();

    }
}
