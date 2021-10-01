using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] GameObject FirstStart_OBJ;
    //[SerializeField] Image MM_bg;
    [SerializeField] GameObject MM_Parent;
    [SerializeField] TextMeshProUGUI MM_title;
    [SerializeField] GameObject MM_buttonParent;
    [SerializeField] Button LoadButton;
    [SerializeField] Animator MM_animator;
    [SerializeField] GameObject Creadit;
    [SerializeField] GameObject setting;
    [SerializeField] GameObject instruction_OBJ;

    const string MM_ANIMATION_FADEIN = "FadeIn";
    const string MM_ANIMATION_FADEOUT = "FadeOut";
    const string CREDIT_IN = "CreditIn";
    const string SETTING_IN = "SettingIn";

    bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MainMenuStatus(true);
        isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSaveFile();
    }

    public void MainMenuStatus(bool stat)
    {
        if(stat == true)//turn on all the main menu assets, and play fade in
        {
            Creadit.SetActive(false); instruction_OBJ.SetActive(false); setting.SetActive(false); MM_Parent.SetActive(true);
            //if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(MM_ANIMATION_FADEIN) != true)
            //{
            //    MM_animator.Play(MM_ANIMATION_FADEIN);
            //}
            //if(AudioManager.instance.FindIsPlaying("MainMenuBGM", "BGM") == false)
            //{
            //    AudioManager.instance.StopAll();
            //    AudioManager.instance.Play("MainMenuBGM", "BGM");
            //}
        }
        else 
        {
            //if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(MM_ANIMATION_FADEOUT) != true)
            //{
            //    MM_animator.Play(MM_ANIMATION_FADEOUT);
            //}
            //AudioManager.instance.StopAll();
            //AudioManager.instance.Play("MainMenuBGM", "GameBGM");
            MM_Parent.SetActive(false);
        }
    }

    public void StartGameButton()
    {
        MainMenuStatus(false);
        isOn = false;
        //if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(SETTING_IN) != true)
        //{
        //    MM_animator.Play(SETTING_IN);
        //}
    }

    public void LoadGameButton()
    {
        //MainMenuStatus(false);
    }

    public void SettingButton()
    {
        MainMenuStatus(false);
        setting.SetActive(true);
        //if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(SETTING_IN) != true)
        //{
        //    MM_animator.Play(SETTING_IN);
        //}
    }

    public void CreditButton()
    {
        MainMenuStatus(false);
        Creadit.SetActive(true);
        //if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(CREDIT_IN) != true)
        //{
        //    MM_animator.Play(CREDIT_IN);
        //}
    }

    public void InstructionButtom()
    {
        MainMenuStatus(false);
        instruction_OBJ.SetActive(true);
    }

    public void BackToMainMenu()
    {
        MainMenuStatus(true);
    }

    public void QuitButton()
    {
        AudioManager.instance.StopAll();
        Application.Quit();
    }

    public void CheckSaveFile()
    {
        //a save methods is implemented then will recode it

        //as placeholder done by 21/6/2021 will be unable to interact it because there's no save file.
        LoadButton.interactable = false;
    }

    public bool getMainMenuStatus()
    {
        return isOn;
    }

}
