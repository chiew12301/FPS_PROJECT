using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_STATUS
{
    P_IDLE = 0,
    P_FORWARD,
    P_BACKWARD,
    P_LEFT,
    P_RIGHT,
    P_LEFTFOWARD,
    P_RIGHTFORWARD,
    P_LEFTBACKWARD,
    P_RIGHTBACKWARD,
    P_JUMP
}

public class PlayerMovement : MonoBehaviour
{
    public PLAYER_STATUS p_Direction;
    public Animator p_animator;

    private float p_Speed = 4.0f;
    private float p_JumpHeight = 5.0f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    private bool isForward = false;
    private bool isBackward = false;
    public bool isMoving = false;
    public bool isJump = false;

    //Animation Name
    const string IDLE_ANIMATION = "idle";
    const string RUN_ANIMATION = "Run";
    const string RUN_LEFT_ANIMATION = "RunLeft";
    const string RUN_RIGHT_ANIMATION = "RunRight";
    const string RUN_BACK_ANIMATION = "Backward";
    const string RUN_LEFTFORWARD_ANIMATION = "LEFTFOWARD";
    const string RUN_LEFTBACKWARD_ANIMATION = "LEFTBACKWARD";
    const string RUN_RIGHTFORWARD_ANIMATION = "RIGHTFORWARD";
    const string RUN_RIGHTBACKWARD_ANIMATION = "RIGHTBACKWARD";
    const string JUMP_ANIMATION = "JUMP";

    // Start is called before the first frame update
    void Start()
    {
        p_Direction = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput(); //Trigger Once
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            p_Direction = PLAYER_STATUS.P_FORWARD;
            isForward = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            p_Direction = PLAYER_STATUS.P_BACKWARD;
            isBackward = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            p_Direction = PLAYER_STATUS.P_LEFT;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            p_Direction = PLAYER_STATUS.P_RIGHT;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            p_Direction = PLAYER_STATUS.P_JUMP;
        }
        else
        {
            p_Direction = PLAYER_STATUS.P_IDLE;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isForward = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isBackward = false;
        }

        if (isForward == true || isBackward == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (isForward == true)
                {
                    p_Direction = PLAYER_STATUS.P_LEFTFOWARD;
                }
                else { p_Direction = PLAYER_STATUS.P_LEFTBACKWARD; }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (isForward == true)
                {
                    p_Direction = PLAYER_STATUS.P_RIGHTFORWARD;
                }
                else { p_Direction = PLAYER_STATUS.P_RIGHTBACKWARD; }
            }
        }

        CheckMovement(); //Then Check MOVEMENT
    }

    private void CheckMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded == true)
        {
            isJump = false;
        }
        else
        {
            isJump = true;
        }

        if (p_Direction == PLAYER_STATUS.P_FORWARD)
        {
            //move forward
            transform.position += transform.forward * Time.deltaTime * p_Speed;
            isMoving = true;
            if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(RUN_ANIMATION) != true)
            {
                p_animator.Play(RUN_ANIMATION);
            }
        }
        else if (p_Direction == PLAYER_STATUS.P_BACKWARD)
        {
            //Move Backward
            transform.position -= transform.forward * Time.deltaTime * (p_Speed - 2.0f);
            isMoving = true;
            if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(RUN_BACK_ANIMATION) != true)
            {
                p_animator.Play(RUN_BACK_ANIMATION);
            }
        }
        else if (p_Direction == PLAYER_STATUS.P_LEFT)
        {
            //Move Left
            transform.position -= transform.right * Time.deltaTime * (p_Speed - 2.0f);
            isMoving = true;
            isForward = false;
            isBackward = false;
            if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(RUN_LEFT_ANIMATION) != true)
            {
                p_animator.Play(RUN_LEFT_ANIMATION);
            }
        }
        else if (p_Direction == PLAYER_STATUS.P_RIGHT)
        {
            //Move Right
            transform.position += transform.right * Time.deltaTime * (p_Speed - 2.0f);
            isMoving = true;
            isForward = false;
            isBackward = false;
            if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(RUN_RIGHT_ANIMATION) != true)
            {
                p_animator.Play(RUN_RIGHT_ANIMATION);
            }
        }
        else if (p_Direction == PLAYER_STATUS.P_LEFTFOWARD)
        {
            transform.position -= transform.right * Time.deltaTime * p_Speed;
            transform.position += transform.forward * Time.deltaTime * p_Speed;
            isMoving = true;
            if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(RUN_LEFTFORWARD_ANIMATION) != true)
            {
                p_animator.Play(RUN_LEFTFORWARD_ANIMATION);
            }
        }
        else if (p_Direction == PLAYER_STATUS.P_LEFTBACKWARD)
        {
            transform.position -= transform.right * Time.deltaTime * p_Speed;
            transform.position -= transform.forward * Time.deltaTime * (p_Speed - 2.0f);
            isMoving = true;
            if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(RUN_LEFTBACKWARD_ANIMATION) != true)
            {
                p_animator.Play(RUN_LEFTBACKWARD_ANIMATION);
            }
        }
        else if (p_Direction == PLAYER_STATUS.P_RIGHTFORWARD)
        {
            transform.position += transform.right * Time.deltaTime * (p_Speed - 2.0f);
            transform.position += transform.forward * Time.deltaTime * p_Speed;
            isMoving = true;
            if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(RUN_RIGHTFORWARD_ANIMATION) != true)
            {
                p_animator.Play(RUN_RIGHTFORWARD_ANIMATION);
            }
        }
        else if (p_Direction == PLAYER_STATUS.P_RIGHTBACKWARD)
        {
            transform.position += transform.right * Time.deltaTime * (p_Speed - 2.0f);
            transform.position -= transform.forward * Time.deltaTime * (p_Speed - 2.0f);
            isMoving = true;
            if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(RUN_RIGHTBACKWARD_ANIMATION) != true)
            {
                p_animator.Play(RUN_RIGHTBACKWARD_ANIMATION);
            }
        }
        else if (p_Direction == PLAYER_STATUS.P_JUMP)
        {
            //Jump
            if (isJump == false)
            {
                transform.position += transform.up * Time.deltaTime * p_JumpHeight;
                isMoving = true;
                isJump = true;
                if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(JUMP_ANIMATION) != true)
                {
                    p_animator.Play(JUMP_ANIMATION);
                }
            }
            else
            {
                //check
                transform.position -= transform.up * Time.deltaTime * p_JumpHeight;
            }
        }
        else
        {
            //IDLE
            isMoving = false;
            isForward = false;
            isBackward = false;
            if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(IDLE_ANIMATION) != true)
            {
                p_animator.Play(IDLE_ANIMATION);
            }
        }
    }

}

