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
    P_JUMP
    //P_CROUCH
}

public class PlayerMovementNew : MonoBehaviour
{
    public PLAYER_STATE p_Direction;
    public CharacterController controller;

    public float speed = 3f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public GameObject eqMenu;
    public Transform groundCheck;
    public float groundDistance = 200.0f;
    public LayerMask groundMask;
    public float runMultiply = 2.0f;
    /*public float crouchingHeight = 0.5f;
    public float crouchingMultiply = 0.001f;
    public float standingHeight = 1.5f;*/

    Vector3 velocity;
    bool isGrounded;
    bool isMoving;
    public bool isRunning;
    public bool isWalking;
    //public bool isCrouching;

    public Camera fpsCam;
    public float bobbingSpeed;
    public float bobbingAmount;
    float defaultPosY = 0;
    float timer = 0;
    
    void Start()
    {
        p_Direction = 0;
        defaultPosY = fpsCam.transform.localPosition.y;
        //controller.GetComponent<CharacterController>().detectCollisions = false;
    }

    // Update is called once per frame
    void Update()
    {
        InputState();
        ViewBobbing(isMoving);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (!isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey(KeyCode.LeftShift))// && isCrouching == false)
        {
            isRunning = true;
            isWalking = false;
            bobbingAmount = 0.04f;
        }
        else
        {
            isRunning = false;
            bobbingAmount = 0.02f;
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            eqMenu.SetActive(true);
        }
        else
        {
            eqMenu.SetActive(false);
        }

        /*if(!isCrouching)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                isCrouching = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                isCrouching = false;
            }
        }*/

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        if (isRunning && isGrounded)
        {
            move *= runMultiply;
        }

        /*if(isCrouching)
        {
            controller.height = crouchingHeight;
            move *= crouchingMultiply;
        }
        else
        {
            controller.height = standingHeight;
        }*/

        controller.Move(move * speed * Time.deltaTime);

        /*if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isGrounded = false;
        }*/

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void InputState()
    {
        speed = 3f;
        /*if (Input.GetKey(KeyCode.Space) || !isGrounded)
        {
            p_Direction = PLAYER_STATE.P_JUMP;
        }*/
        if(Input.GetKey(KeyCode.W))
        {
            if(isRunning)
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
                AudioManager.instance.Play("Run", "SFX");
            }
            else
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
                isWalking = true;
                AudioManager.instance.Play("Walk", "SFX");
            }
            isMoving = true;
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
            isWalking = true;
            isMoving = true;
            AudioManager.instance.Play("Walk", "SFX");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (isRunning)
            {
                p_Direction = PLAYER_STATE.P_LEFT;
                AudioManager.instance.Play("Run", "SFX");
            }
            else
            {
                p_Direction = PLAYER_STATE.P_WALKLEFT;
                isWalking = true;
                AudioManager.instance.Play("Walk", "SFX");
            }
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (isRunning)
            {
                p_Direction = PLAYER_STATE.P_RIGHT;
                AudioManager.instance.Play("Run", "SFX");
            }
            else
            {
                p_Direction = PLAYER_STATE.P_WALKRIGHT;
                isWalking = true;
                AudioManager.instance.Play("Walk", "SFX");
            }
            isMoving = true;
        }
        else
        {
            p_Direction = PLAYER_STATE.P_IDLE;
            isWalking = false;
            isMoving = false;
            AudioManager.instance.Stop("Walk", "SFX");
            AudioManager.instance.Stop("Run", "SFX");
        }
    }

    void ViewBobbing(bool isMoving)
    {
        if(isMoving)
        {
            timer += Time.deltaTime * bobbingSpeed;
            fpsCam.transform.localPosition = new Vector3(fpsCam.transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, fpsCam.transform.localPosition.z);
        }   
        else
        {
            timer = 0;
            fpsCam.transform.localPosition = new Vector3(fpsCam.transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * bobbingSpeed), fpsCam.transform.localPosition.z);
        }
    }
}
