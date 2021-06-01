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
    P_JUMP
}

public class PlayerMovement : MonoBehaviour
{
    public PLAYER_STATUS p_Direction;
    public Animator p_animator;

    private float p_Speed = 2.0f;
    private float p_JumpHeight = 5.0f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    public bool isMoving = false;
    public bool isJump = false;

    //Animation Name
    const string IDLE_ANIMATION = "idle";
    const string RUN_ANIMATION = "Run";
    const string RUN_LEFT_ANIMATION = "RunLeft";
    const string RUN_RIGHT_ANIMATION = "RunRight";
    const string RUN_BACK_ANIMATION = "Backward";

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
        if(Input.GetKey(KeyCode.W))
        {
            p_Direction = PLAYER_STATUS.P_FORWARD;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            p_Direction = PLAYER_STATUS.P_BACKWARD;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            p_Direction = PLAYER_STATUS.P_LEFT;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            p_Direction = PLAYER_STATUS.P_RIGHT;
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            p_Direction = PLAYER_STATUS.P_JUMP;
        }
        else
        {
            p_Direction = PLAYER_STATUS.P_IDLE;
        }
        CheckMovement(); //Then Check MOVEMENT
    }

    private void CheckMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded == true)
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
            transform.position -= transform.forward * Time.deltaTime * p_Speed;
            isMoving = true;
            if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(RUN_BACK_ANIMATION) != true)
            {
                p_animator.Play(RUN_BACK_ANIMATION);
            }
        }
        else if (p_Direction == PLAYER_STATUS.P_LEFT)
        {
            //Move Left
            transform.position -= transform.right * Time.deltaTime * p_Speed;
            isMoving = true;
            if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(RUN_LEFT_ANIMATION) != true)
            {
                p_animator.Play(RUN_LEFT_ANIMATION);
            }
        }
        else if (p_Direction == PLAYER_STATUS.P_RIGHT)
        {
            //Move Right
            transform.position += transform.right * Time.deltaTime * p_Speed;
            isMoving = true;
            if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(RUN_RIGHT_ANIMATION) != true)
            {
                p_animator.Play(RUN_RIGHT_ANIMATION);
            }
        }
        else if (p_Direction == PLAYER_STATUS.P_JUMP)
        {
            //Jump
            if(isJump == false)
            {
                transform.position += transform.up * Time.deltaTime * p_JumpHeight;
                isMoving = true;
                isJump = true;
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
            if (p_animator.GetCurrentAnimatorStateInfo(0).IsName(IDLE_ANIMATION) != true)
            {
                p_animator.Play(IDLE_ANIMATION);
            }
        }
    }

}
