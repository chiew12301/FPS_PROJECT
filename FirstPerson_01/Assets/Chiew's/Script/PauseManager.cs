using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PAUSEUI
{
    INVENTORYUI = 0,
    SETTINGUI,
    MAPUI,
    NONEPAUSE
}

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
    public Animator canvas_animator;
    private PAUSEUI UISTAT = PAUSEUI.NONEPAUSE; //change this

    private bool isPause = false;
    private bool INV_PAUSE = false;
    private bool SET_PAUSE = false;
    private bool MM_PAUSE = false;
    private bool isPlayingAni = false;

    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        isPlayingAni = false;
    }

    public bool getIsPlayingAni()
    {
        return isPlayingAni;
    }

    public bool getIsPause()
    {
        return isPause;
    }

    public void ChangeUISTATE(PAUSEUI uistate)
    {
        UISTAT = uistate;
        if(UISTAT == PAUSEUI.INVENTORYUI)
        {
            if(AudioManager.instance.FindIsPlaying("OpenUI", "SFX") == false)
            {
                AudioManager.instance.Play("OpenUI", "SFX");
            }

            StartCoroutine(playAnimation("InventoryIn"));
        }
        else if(UISTAT == PAUSEUI.MAPUI)
        {
            //play mapui animation;
        }
        else if(UISTAT == PAUSEUI.SETTINGUI)
        {
            if (AudioManager.instance.FindIsPlaying("OpenUI", "SFX") == false)
            {
                AudioManager.instance.Play("OpenUI", "SFX");
            }

            StartCoroutine(playAnimation("SettingIn"));
        }
        else //NONEPAUSE
        {
            ChangeAnimationState("Empty");
            setIsPause(false);
            isPlayingAni = false;
        }
    }

    public PAUSEUI getUISTATE()
    {
        return UISTAT;
    }

    public void setIsPause(bool status)
    {
        isPause = status;
        if(isPause == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Time.timeScale = 1.0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Time.timeScale = 0.000001f; //or 0.0f
        }
    }

    public void ChangeAnimationState(string AniState) //Remember to use Const String
    {
        isPlayingAni = true;
        if (canvas_animator.GetCurrentAnimatorStateInfo(0).IsName(AniState) != true)
        {
            canvas_animator.Play(AniState);
        }
    }
    IEnumerator playAnimation(string AniState)
    {
        ChangeAnimationState(AniState);
        yield return new WaitForSeconds(0.5f);
        setIsPause(true);
        isPlayingAni = false;
    }

}
