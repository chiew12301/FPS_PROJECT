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
    float movespeed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
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

    [SerializeField]
    GameObject mainMenu;
    void Start()
    {
        p_Direction = 0;
        defaultPosY = fpsCam.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<Cutscene>().GetIsCutscene() && !PauseManager.instance.getIsPause() && !mainMenu.GetComponent<MainMenu>().getMainMenuStatus() 
            && gameObject.GetComponent<Cutscene>().GetCanMovePlayer())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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

            controller.Move(move * movespeed * Time.deltaTime);

            /*if(Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isGrounded = false;
            }*/

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            SFXCheck();
        }
        else
        {
            if(mainMenu.GetComponent<MainMenu>().getMainMenuStatus() || PauseManager.instance.getIsPause())
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            if (AudioManager.instance.FindIsPlaying("Run", "SFX"))
            {
                AudioManager.instance.Stop("Run", "SFX");
            }
            if (!AudioManager.instance.FindIsPlaying("Walk", "SFX"))
            {
                AudioManager.instance.Stop("Walk", "SFX");
            }
        }
    }

    private void InputState()
    {
        movespeed = speed;
        /*if (Input.GetKey(KeyCode.Space) || !isGrounded)
        {
            p_Direction = PLAYER_STATE.P_JUMP;
        }*/
        if (Input.GetKey(KeyCode.W))
        {
            if (isRunning)
            {
                p_Direction = PLAYER_STATE.P_FORWARD;
                if (Input.GetKey(KeyCode.A))
                {
                    p_Direction = PLAYER_STATE.P_LEFTFOWARD;
                    movespeed = speed / 1.5f;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    p_Direction = PLAYER_STATE.P_RIGHTFORWARD;
                    movespeed = speed / 1.5f;
                }
            }
            else
            {
                p_Direction = PLAYER_STATE.P_WALKFORWARD;
                if (Input.GetKey(KeyCode.A))
                {
                    p_Direction = PLAYER_STATE.P_WALKLEFTFOWARD;
                    movespeed = speed / 1.5f;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    p_Direction = PLAYER_STATE.P_WALKRIGHTFORWARD;
                    movespeed = speed / 1.5f;
                }
                isWalking = true;
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
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (isRunning)
            {
                p_Direction = PLAYER_STATE.P_LEFT;
            }
            else
            {
                p_Direction = PLAYER_STATE.P_WALKLEFT;
                isWalking = true;
            }
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (isRunning)
            {
                p_Direction = PLAYER_STATE.P_RIGHT;
            }
            else
            {
                p_Direction = PLAYER_STATE.P_WALKRIGHT;
                isWalking = true;
            }
            isMoving = true;
        }
        else
        {
            p_Direction = PLAYER_STATE.P_IDLE;
            isWalking = false;
            isMoving = false;
        }
    }

    void SFXCheck()
    {
        if(p_Direction == PLAYER_STATE.P_WALKFORWARD || p_Direction == PLAYER_STATE.P_WALKLEFT || p_Direction == PLAYER_STATE.P_WALKLEFTFOWARD 
            || p_Direction == PLAYER_STATE.P_WALKRIGHT || p_Direction == PLAYER_STATE.P_WALKRIGHTFORWARD || p_Direction == PLAYER_STATE.P_BACKWARD
            || p_Direction == PLAYER_STATE.P_LEFTBACKWARD || p_Direction == PLAYER_STATE.P_RIGHTBACKWARD)
        {
            if (AudioManager.instance.FindIsPlaying("Run", "SFX"))
            {
                AudioManager.instance.Stop("Run", "SFX");
            }
            if (!AudioManager.instance.FindIsPlaying("Walk", "SFX"))
            {
                AudioManager.instance.Play("Walk", "SFX");
            }
        }
        if (p_Direction == PLAYER_STATE.P_FORWARD || p_Direction == PLAYER_STATE.P_LEFTFOWARD || p_Direction == PLAYER_STATE.P_LEFT
            || p_Direction == PLAYER_STATE.P_RIGHT || p_Direction == PLAYER_STATE.P_RIGHTFORWARD)
        {
            if (!AudioManager.instance.FindIsPlaying("Walk", "SFX"))
            {
                AudioManager.instance.Stop("Walk", "SFX");
            }
            if (!AudioManager.instance.FindIsPlaying("Run", "SFX"))
            {
                AudioManager.instance.Play("Run", "SFX");
            }
        }
        if (p_Direction == PLAYER_STATE.P_IDLE)
        {
            AudioManager.instance.Stop("Walk", "SFX");
            AudioManager.instance.Stop("Run", "SFX");
        }
    }

    void ViewBobbing(bool isMoving)
    {
        if (isMoving)
        {
            timer += Time.deltaTime * bobbingSpeed;
            fpsCam.transform.localPosition = new Vector3(fpsCam.transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, fpsCam.transform.localPosition.z);
        }
        else
        {
            timer = 0;
            fpsCam.transform.localPosition = new Vector3(fpsCam.transform.localPosition.x, Mathf.Lerp(fpsCam.transform.localPosition.y, defaultPosY, Time.deltaTime * bobbingSpeed), fpsCam.transform.localPosition.z);
        }
    }
}
