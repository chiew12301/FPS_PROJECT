using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] Animator obj_Animator;

    //Animation Name (Get your animation here)
    //Example to call it, In your own script call the function, and pass the const string into the function
    //================Example=================
    //In movement script
    //public Animation animationSCript;
    //then when assigning call this below:
    //animationScript.ChangeAnimationState(animationScript.IDLE_ANIMATION);
    public const string IDLE_ANIMATION = "idle";
    public const string JUMP_ANIMATION = "Jump";
    public const string RUN_ANIMATION = "Run";
    public const string RUN_LEFT_ANIMATION = "RunLeft";
    public const string RUN_RIGHT_ANIMATION = "RunRight";
    public const string RUN_BACK_ANIMATION = "Backward";
    public const string RUN_LEFTFORWARD_ANIMATION = "LEFTFOWARD";
    public const string RUN_LEFTBACKWARD_ANIMATION = "LEFTBACKWARD";
    public const string RUN_RIGHTFORWARD_ANIMATION = "RIGHTFORWARD";
    public const string RUN_RIGHTBACKWARD_ANIMATION = "RIGHTBACKWARD";

    public void ChangeAnimationState(string AniState) //Remember to use Const String
    {
        if (obj_Animator.GetCurrentAnimatorStateInfo(0).IsName(AniState) != true)
        {
            obj_Animator.Play(AniState);
        }
    }

}
