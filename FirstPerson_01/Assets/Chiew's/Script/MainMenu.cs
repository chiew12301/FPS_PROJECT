using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject FirstStart_OBJ;
    [SerializeField] Image MM_bg;
    [SerializeField] TextMeshProUGUI MM_title;
    [SerializeField] GameObject MM_buttonParent;
    [SerializeField] Button LoadButton;
    [SerializeField] Animator MM_animator;
    [SerializeField] GameObject Creadit;
    [SerializeField] GameObject Setting;
    [SerializeField] GameObject instruction_OBJ;
    [SerializeField] LevelLoader LV_loader;

    const string MM_ANIMATION_FADEIN = "FadeIn";
    const string MM_ANIMATION_FADEOUT = "FadeOut";
    const string CREDIT_IN = "CreditIn";
    const string SETTING_IN = "SettingIn";

    bool isStartOn = false;
    bool isMMOn = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.StopAll();
        MainMenuStatus(false);
        isStartOn = false;
        CheckFirstStart();
        //MainMenuStatus(true);
    }

    // Update is called once per frame
    void Update()
    {
        CheckFirstStart();
        CheckSaveFile();
    }

    public void CheckFirstStart()
    {
        if(isStartOn == false)
        {
            FirstStart_OBJ.SetActive(true);
            Creadit.SetActive(false); instruction_OBJ.SetActive(false);
            MainMenuStatus(false);
            if (Input.anyKey)
            {
                //turn off the whole objects and set to false
                FirstStart_OBJ.SetActive(false);
                MainMenuStatus(true);
                isStartOn = true;
            }
        }
    }

    public void MainMenuStatus(bool stat)
    {
        if(stat == true)//turn on all the main menu assets, and play fade in
        {
            MM_bg.gameObject.SetActive(true); MM_buttonParent.SetActive(true); MM_title.gameObject.SetActive(true); Creadit.SetActive(false); instruction_OBJ.SetActive(false);
            if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(MM_ANIMATION_FADEIN) != true)
            {
                MM_animator.Play(MM_ANIMATION_FADEIN);
            }
            if(AudioManager.instance.FindIsPlaying("MainMenuBGM", "BGM") == false)
            {
                AudioManager.instance.StopAll();
                AudioManager.instance.Play("MainMenuBGM", "BGM");
            }
            isMMOn = true;
        }
        else 
        {
            if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(MM_ANIMATION_FADEOUT) != true)
            {
                MM_animator.Play(MM_ANIMATION_FADEOUT);
            }
            AudioManager.instance.StopAll();
            AudioManager.instance.Play("MainMenuBGM", "GameBGM");
            isMMOn = false;
        }
    }

    public void StartGameButton()
    {
        MainMenuStatus(false);
        LV_loader.LoadLevel(1);
        if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(SETTING_IN) != true)
        {
            MM_animator.Play(SETTING_IN);
        }
        AudioManager.instance.Play("GameBGM", "BGM");
    }

    public void LoadGameButton()
    {
        //MainMenuStatus(false);
    }

    public void SettingButton()
    {
        MainMenuStatus(false);
        Setting.SetActive(true);
        if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(SETTING_IN) != true)
        {
            MM_animator.Play(SETTING_IN);
        }
        AudioManager.instance.Play("GameBGM", "BGM");
    }

    public void CreditButton()
    {
        MainMenuStatus(false);
        Creadit.SetActive(true);
        if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(CREDIT_IN) != true)
        {
            MM_animator.Play(CREDIT_IN);
        }
        AudioManager.instance.Play("GameBGM", "BGM");
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

}
