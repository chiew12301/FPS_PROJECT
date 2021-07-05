using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] Animator obj_Animator;
    [SerializeField] PlayerMovementNew playerMovement_Script;

    Interactable focus;

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

    public void Update()
    {
        #region CASE_ANIMATION
        switch (playerMovement_Script.p_Direction)
        {
            case PLAYER_STATE.P_IDLE:
                ChangeAnimationState(IDLE_ANIMATION);
                break;
            case PLAYER_STATE.P_JUMP:
                ChangeAnimationState(JUMP_ANIMATION);
                break;
            case PLAYER_STATE.P_FORWARD:
                ChangeAnimationState(RUN_ANIMATION);
                break;
            case PLAYER_STATE.P_BACKWARD:
                ChangeAnimationState(RUN_BACK_ANIMATION);
                break;
            case PLAYER_STATE.P_LEFT:
                ChangeAnimationState(RUN_LEFT_ANIMATION);
                break;
            case PLAYER_STATE.P_RIGHT:
                ChangeAnimationState(RUN_RIGHT_ANIMATION);
                break;
            case PLAYER_STATE.P_LEFTFOWARD:
                ChangeAnimationState(RUN_LEFTFORWARD_ANIMATION);
                break;
            case PLAYER_STATE.P_LEFTBACKWARD:
                ChangeAnimationState(RUN_LEFTBACKWARD_ANIMATION);
                break;
            case PLAYER_STATE.P_RIGHTFORWARD:
                ChangeAnimationState(RUN_RIGHTFORWARD_ANIMATION);
                break;
            case PLAYER_STATE.P_RIGHTBACKWARD:
                ChangeAnimationState(RUN_RIGHTBACKWARD_ANIMATION);
                break;
            default:
                break;
        }
        #endregion CASE_ANIMATION

        #region IF_ANIMATION
        //if (playerMovement_Script.p_Direction == PLAYER_STATE.P_IDLE)
        //{
        //    ChangeAnimationState(IDLE_ANIMATION);
        //}
        //if (playerMovement_Script.p_Direction == PLAYER_STATE.P_JUMP)
        //{
        //    ChangeAnimationState(JUMP_ANIMATION);
        //}
        //if (playerMovement_Script.p_Direction == PLAYER_STATE.P_FORWARD)
        //{
        //    ChangeAnimationState(RUN_ANIMATION);
        //}
        //if (playerMovement_Script.p_Direction == PLAYER_STATE.P_BACKWARD)
        //{
        //    ChangeAnimationState(RUN_BACK_ANIMATION);
        //}
        //if (playerMovement_Script.p_Direction == PLAYER_STATE.P_LEFT)
        //{
        //    ChangeAnimationState(RUN_LEFT_ANIMATION);
        //}
        //if (playerMovement_Script.p_Direction == PLAYER_STATE.P_RIGHT)
        //{
        //    ChangeAnimationState(RUN_RIGHT_ANIMATION);
        //}

        //if (playerMovement_Script.p_Direction == PLAYER_STATE.P_LEFTFOWARD)
        //{
        //    ChangeAnimationState(RUN_LEFTFORWARD_ANIMATION);
        //}
        //else if (playerMovement_Script.p_Direction == PLAYER_STATE.P_LEFTBACKWARD)
        //{
        //    ChangeAnimationState(RUN_LEFTBACKWARD_ANIMATION);
        //}

        //if (playerMovement_Script.p_Direction == PLAYER_STATE.P_RIGHTFORWARD)
        //{
        //    ChangeAnimationState(RUN_RIGHTFORWARD_ANIMATION);
        //}
        //else if (playerMovement_Script.p_Direction == PLAYER_STATE.P_RIGHTBACKWARD)
        //{
        //    ChangeAnimationState(RUN_RIGHTBACKWARD_ANIMATION);
        //}
        #endregion IF_ANIMATION

        //create ray cast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isPressed = false;
        //if ray hit
        if (Physics.Raycast(ray, out hit, 100))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (Input.GetKeyUp(KeyCode.E))//can change any button/key
            {
                if (interactable != null)
                {
                    RemoveFocus();
                }
            }

            if (Input.GetKeyDown(KeyCode.E))//can change any button/key
            {
                isPressed = true;
            }

            if (interactable != null)
            {
                SetFocus(interactable, isPressed);
            }
        }

    }

    void SetFocus(Interactable newFocus, bool isPressed)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
        }

        focus = newFocus;
        newFocus.OnFocused(transform, isPressed);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
    }

    public void ChangeAnimationState(string AniState) //Remember to use Const String
    {
        if (obj_Animator.GetCurrentAnimatorStateInfo(0).IsName(AniState) != true)
        {
            obj_Animator.Play(AniState);
        }
    }

}
