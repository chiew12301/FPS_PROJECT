using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_STATE
{
    P_IDLE = 0,
    P_FORWARD,
    P_WALKFORWARD,
    P_BACKWARD,
    P_LEFT,
    P_WALKLEFT,
    P_RIGHT,
    P_WALKRIGHT,
    P_LEFTFOWARD,
    P_WALKLEFTFOWARD,
    P_RIGHTFORWARD,
    P_WALKRIGHTFORWARD,
    P_LEFTBACKWARD,
    P_RIGHTBACKWARD,
    P_JUMP,
    P_CROUCH
}

public class PlayerMovementNew : MonoBehaviour
{
    public PLAYER_STATE p_Direction;
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 200.0f;
    public LayerMask groundMask;
    public float walkMultiply = 0.001f;
    public float crouchingHeight = 1f;
    public float crouchingMultiply = 0.001f;
    public float standingHeight = 2f;

    Vector3 velocity;
    bool isForward;
    bool isBackward;
    bool isGrounded;
    public bool isWalking;
    public bool isCrouching;
    
    void Start()
    {
        p_Direction = 0;
    }

    // Update is called once per frame
    void Update()
    {
        InputState();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (!isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        if (isWalking && isGrounded)
        {
            move *= walkMultiply;
        }

        if(isCrouching)
        {
            controller.height = crouchingHeight;
            move *= crouchingMultiply;
        }
        else
        {
            controller.height = standingHeight;
        }

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isGrounded = false;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if(p_Direction != PLAYER_STATE.P_IDLE || p_Direction != PLAYER_STATE.P_JUMP)
        {
            AudioManager.instance.Play("Footstep", "SFX");
        }
    }

    private void InputState()
    {
        if (Input.GetKey(KeyCode.Space) || !isGrounded)
        {
            p_Direction = PLAYER_STATE.P_JUMP;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if(isWalking)
            {
                p_Direction = PLAYER_STATE.P_WALKFORWARD;
                if (Input.GetKey(KeyCode.A))
                {
                    p_Direction = PLAYER_STATE.P_WALKLEFTFOWARD;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    p_Direction = PLAYER_STATE.P_WALKRIGHTFORWARD;
                }
            }
            else
            {
                p_Direction = PLAYER_STATE.P_FORWARD;
                if (Input.GetKey(KeyCode.A))
                {
                    p_Direction = PLAYER_STATE.P_LEFTFOWARD;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    p_Direction = PLAYER_STATE.P_RIGHTFORWARD;
                }
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            p_Direction = PLAYER_STATE.P_BACKWARD;
            if (Input.GetKey(KeyCode.A))
            {
                p_Direction = PLAYER_STATE.P_LEFTBACKWARD;
            }
            if (Input.GetKey(KeyCode.D))
            {
                p_Direction = PLAYER_STATE.P_RIGHTBACKWARD;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (isWalking)
            {
                p_Direction = PLAYER_STATE.P_WALKLEFT;
            }
            else
            {
                p_Direction = PLAYER_STATE.P_LEFT;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (isWalking)
            {
                p_Direction = PLAYER_STATE.P_WALKRIGHT;
            }
            else
            {
                p_Direction = PLAYER_STATE.P_RIGHT;
            }
        }
        else
        {
            p_Direction = PLAYER_STATE.P_IDLE;
        }
    }
}
