using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Image MM_bg;
    [SerializeField] TextMeshProUGUI MM_title;
    [SerializeField] GameObject MM_buttonParent;
    [SerializeField] Animator MM_animator;
    [SerializeField] GameObject Creadit;
    [SerializeField] GameObject Setting;

    const string MM_ANIMATION_FADEIN = "FadeIn";
    const string MM_ANIMATION_FADEOUT = "FadeOut";
    const string CREDIT_IN = "CreditIn";
    const string SETTING_IN = "SettingIn";

    // Start is called before the first frame update
    void Start()
    {
        MainMenuStatus(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuStatus(bool stat)
    {
        if(stat == true)//turn on all the main menu assets, and play fade in
        {
            MM_bg.gameObject.SetActive(true); MM_buttonParent.SetActive(true); MM_title.gameObject.SetActive(true); Creadit.SetActive(false);
            if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(MM_ANIMATION_FADEIN) != true)
            {
                MM_animator.Play(MM_ANIMATION_FADEIN);
            }
        }
        else 
        {
            if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(MM_ANIMATION_FADEOUT) != true)
            {
                MM_animator.Play(MM_ANIMATION_FADEOUT);
            }
        }
    }

    public void StartGameButton()
    {
        MainMenuStatus(false);
    }

    public void LoadGameButton()
    {
        MainMenuStatus(false);
    }

    public void SettingButton()
    {
        MainMenuStatus(false);
        Setting.SetActive(true);
        if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(SETTING_IN) != true)
        {
            MM_animator.Play(SETTING_IN);
        }
    }

    public void CreditButton()
    {
        MainMenuStatus(false);
        Creadit.SetActive(true);
        if (MM_animator.GetCurrentAnimatorStateInfo(0).IsName(CREDIT_IN) != true)
        {
            MM_animator.Play(CREDIT_IN);
        }
    }

    public void BackToMainMenu()
    {
        MainMenuStatus(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
