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
    bool isAniDone = false;
    bool FadeOutisPlayed = true;
    bool FadeInisPlayed = false;
    bool isFirstTime = true;
    bool changedLayer = false;
    bool isEntered = false;

    enum MMUISTATE
    {
        MAINMENU = 0,
        SETTING,
        INSTRUCTION,
        CREDIT = 3
    }
    
    [System.Serializable]
    public class MainMenuLayers
    {
        public string name; //Which Layer
        public GameObject MainParent; //Parents of Layer
        public Button[] LayersButton; //All the button(s) under that layer
    };

    public MainMenuLayers[] MM_LAYERS;
    MMUISTATE state = MMUISTATE.MAINMENU;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MainMenuStatus(true);
        isAniDone = true;
        isFirstTime = true;
        isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSaveFile();
        if(state == MMUISTATE.MAINMENU)
        {
            if(isFirstTime == true)
            {
                if (isAniDone == true && FadeInisPlayed == false)
                {
                    Creadit.SetActive(false); instruction_OBJ.SetActive(false); setting.SetActive(false); MM_Parent.SetActive(true);
                    StartCoroutine(animationCountDown(MM_ANIMATION_FADEIN));
                }
                isFirstTime = false;
            }
            else
            {
                if(changedLayer == true)
                {
                    if (isAniDone == true && FadeInisPlayed == false)
                    {
                        Creadit.SetActive(false); instruction_OBJ.SetActive(false); setting.SetActive(false); MM_Parent.SetActive(true);
                        StartCoroutine(animationCountDown(MM_ANIMATION_FADEIN));
                        changedLayer = false;
                    }
                }
            }
        }
        else if(state == MMUISTATE.SETTING)
        {
            if(changedLayer == true)
            {
                if (FadeOutisPlayed == false && isEntered == false)
                {
                    StartCoroutine(animationCountDown(MM_ANIMATION_FADEOUT));
                    isEntered = true;
                }

                if (isAniDone == true && FadeInisPlayed == false)
                {
                    MainMenuStatus(false);
                    setting.SetActive(true);
                    StartCoroutine(animationCountDown(SETTING_IN));
                    isEntered = false;
                    changedLayer = false;
                }
            }

        }
        else if (state == MMUISTATE.CREDIT)
        {
            if (changedLayer == true)
            {
                if (FadeOutisPlayed == false && isEntered == false)
                {
                    StartCoroutine(animationCountDown(MM_ANIMATION_FADEOUT));
                    isEntered = true;
                }

                if (isAniDone == true && FadeInisPlayed == false)
                {
                    MainMenuStatus(false);
                    Creadit.SetActive(true);
                    StartCoroutine(animationCountDown(CREDIT_IN));
                    isEntered = false;
                    changedLayer = false;
                }
            }
        }
        else if (state == MMUISTATE.INSTRUCTION)
        {
            if (changedLayer == true)
            {
                if (FadeOutisPlayed == false && isEntered == false)
                {
                    StartCoroutine(animationCountDown(MM_ANIMATION_FADEOUT));
                    isEntered = true;
                }

                if (isAniDone == true && FadeInisPlayed == false)
                {
                    MainMenuStatus(false);
                    instruction_OBJ.SetActive(true);
                    StartCoroutine(animationCountDown(INSTRUCTION_IN));
                    isEntered = false;
                    changedLayer = false;
                }
            }
        }
    }

    public void MainMenuStatus(bool stat)
    {
        if(stat == true)//turn on all the main menu assets, and play fade in
        {
            state = MMUISTATE.MAINMENU;
            changedLayer = true;
        }
        else 
        {
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

    public void LoadGameButton() //load game
    {
        //MainMenuStatus(false);
    }

    public void SettingButton()
    {
        state = MMUISTATE.SETTING;
        changedLayer = true;
    }

    public void CreditButton()
    {
        state = MMUISTATE.CREDIT;
        changedLayer = true;
    }

    public void InstructionButtom()
    {
        state = MMUISTATE.INSTRUCTION;
        changedLayer = true;
    }

    public void BackToMainMenu(int i)
    {
        if (i == 0)
        {
            if (FadeOutisPlayed == false)
            {
                StartCoroutine(animationCountDown(SETTING_OUT));
            }
        }
        else if(i == 1)
        {
            if (FadeOutisPlayed == false)
            {
                StartCoroutine(animationCountDown(CREDIT_OUT));
            }
        }
        else
        {
            if (FadeOutisPlayed == false)
            {
                StartCoroutine(animationCountDown(INSTRUCTION_OUT));
            }
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

    /// <summary>
    /// Function get is the main menu close
    /// </summary>
    public bool getMainMenuStatus()
    {
        return isOn;
    }

    private void ButtonsInteractable(bool State)
    {
        foreach (MainMenuLayers ml in MM_LAYERS)
        { 
            foreach(Button b in ml.LayersButton)
            {
                b.interactable = State;
            }
        }
    }

    /// <summary>
    /// Function to wait animation play before next action
    /// </summary>
    /// <param name="AnimationName">Animation to play</param>
    IEnumerator animationCountDown(string AnimationName)
    {
        isAniDone = false;
        ButtonsInteractable(false);
        //start play the animation
        if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationName) != true)
        {
            MM_animator.Play(AnimationName);
        }
        yield return new WaitForSeconds(1.3f); //1 seconds limited to each animation, addition 0.5 seconds is for safety purposes
        //Completed Animation
        isAniDone = true;
        ButtonsInteractable(true);
        FadeInisPlayed = !FadeInisPlayed;
        FadeOutisPlayed = !FadeOutisPlayed;
    }

}
