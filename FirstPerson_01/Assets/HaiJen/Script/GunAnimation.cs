using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    [SerializeField] Animator obj_Anim;
    [SerializeField] Gun gun_Script;

    public const string SHOOTINGONE_ANIMATION = "ShootingOne";
    public const string SHOOTINGTWO_ANIMATION = "ShootingTwo";
    public const string IDLE_ANIMATION = "Breathing";
    public const string RELOAD_ANIMATION = "Reload";

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        switch (gun_Script.g_State)
        {
            case GUN_STATE.G_IDLE:
                ChangeAnimationState(IDLE_ANIMATION);
                break;
            case GUN_STATE.G_SHOOTING:
                ChangeAnimationState(SHOOTINGONE_ANIMATION);
                break;
            case GUN_STATE.G_RELOADING:
                ChangeAnimationState(RELOAD_ANIMATION);
                break;
            default:
                break;
        }
    }

    public void ChangeAnimationState(string AniState) //Remember to use Const String
    {
        if (obj_Anim.GetCurrentAnimatorStateInfo(0).IsName(AniState) != true)
        {
            obj_Anim.Play(AniState);
        }
    }
}
