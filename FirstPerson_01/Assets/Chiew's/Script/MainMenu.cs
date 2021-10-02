using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject MM_Parent;
    [SerializeField] TextMeshProUGUI MM_title;
    [SerializeField] GameObject MM_buttonParent;
    [SerializeField] Button LoadButton;
    [SerializeField] Animator MM_animator;
    [SerializeField] GameObject Creadit;
    [SerializeField] GameObject setting;
    [SerializeField] GameObject instruction_OBJ;

    const string MM_ANIMATION_FADEIN = "MainMenuFadeIn";
    const string MM_ANIMATION_FADEOUT = "MainMenuFadeOut";
    const string CREDIT_IN = "CreditFadeIn";
    const string CREDIT_OUT = "CreditFadeOut";
    const string SETTING_IN = "SettingFadeIn";
    const string SETTING_OUT = "SettingFadeOut";
    const string INSTRUCTION_IN = "InstructionFadeIn";
    const string INSTRUCTION_OUT = "InstructionFadeOut";

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
            StartCoroutine(animationCountDown(MM_ANIMATION_FADEIN));
        }
        else 
        {
            StartCoroutine(animationCountDown(MM_ANIMATION_FADEOUT));
            //AudioManager.instance.StopAll();
            //AudioManager.instance.Play("MainMenuBGM", "GameBGM");
            MM_Parent.SetActive(false);
        }
    }

    public void StartGameButton()
    {
        StartCoroutine(animationCountDown(MM_ANIMATION_FADEOUT));
        MainMenuStatus(false);
        isOn = false;
    }

    public void LoadGameButton()
    {
        //MainMenuStatus(false);
    }

    public void SettingButton()
    {
        MainMenuStatus(false);
        setting.SetActive(true);
        StartCoroutine(animationCountDown(SETTING_IN));
    }

    public void CreditButton()
    {
        MainMenuStatus(false);
        Creadit.SetActive(true);
        StartCoroutine(animationCountDown(CREDIT_IN));
    }

    public void InstructionButtom()
    {
        MainMenuStatus(false);
        instruction_OBJ.SetActive(true);
        StartCoroutine(animationCountDown(INSTRUCTION_IN));
    }

    public void BackToMainMenu(int i)
    {
        if(i == 0)
        {
            StartCoroutine(animationCountDown(SETTING_OUT));
        }
        else if(i == 1)
        {
            StartCoroutine(animationCountDown(CREDIT_OUT));
        }
        else
        {
            StartCoroutine(animationCountDown(INSTRUCTION_OUT));
        }
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

    /// <summary>
    /// Function to wait animation play before next action
    /// </summary>
    /// <param name="AnimationName">Animation to play</param>
    IEnumerator animationCountDown(string AnimationName)
    {
        //start play the animation
        if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationName) != true)
        {
            MM_animator.Play(AnimationName);
        }
        yield return new WaitForSeconds(1.1f); //1 seconds limited to each animation, addition 0.1 seconds is for safety purposes
        //Completed Animation
    }

}
