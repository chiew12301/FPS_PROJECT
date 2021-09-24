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
       // GameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.instance.getUISTATE() == PAUSEUI.NONEPAUSE || PauseManager.instance.getUISTATE() == PAUSEUI.SETTINGUI)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (PauseManager.instance.getIsPause() == true)
                {
                    pauseMenuUI.SetActive(false);
                    Resume();

                    PauseManager.instance.ChangeUISTATE(PAUSEUI.NONEPAUSE);
                }
                else
                {
                    pauseMenuUI.SetActive(true);
                    StartCoroutine(playAnimation());
                    Pause();
                }
            }
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
    IEnumerator playAnimation()
    {
        if (canvas_animator.GetCurrentAnimatorStateInfo(0).IsName("SettingIn") != true)
        {
            canvas_animator.Play("SettingIn");
        }
        yield return new WaitForSeconds(1.2f);
        PauseManager.instance.ChangeUISTATE(PAUSEUI.SETTINGUI);
        PauseManager.instance.setIsPause(true);
    }
}
