using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] LevelLoader LV_loader;

    public bool GameIsPaused = false;
    public GameObject invUI;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                GameIsPaused = false;
            }
            else
            {
                GameIsPaused = true;
            }
        }

        if (GameIsPaused || invUI.activeSelf)
        {
            if(GameIsPaused)
            {
                pauseMenuUI.SetActive(true);
            }
            Pause();
        }
        else
        {
            if (!GameIsPaused)
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
        Time.timeScale = 1f;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        GameIsPaused = false;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu");
        //SceneManager.LoadScene("Main Menu UI");
        Time.timeScale = 1.0f;
        LV_loader.LoadLevel(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();

    }
}
