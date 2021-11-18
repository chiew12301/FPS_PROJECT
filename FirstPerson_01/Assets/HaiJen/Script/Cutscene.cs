using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    enum CUTSCENE_STATE
    {
        AUDIO = 0,
        CITY,
        SHAKE,
        JUMP,
        FIRST_FALL,
        FIRST_BLOCK,
        SECOND_FALL,
        SECOND_BLOCK,
        THIRD_FALL,
        PARACHUTE,
        LAST_FALL,
        END,
        TUTORIAL
    }

    [SerializeField]
    CUTSCENE_STATE c_State;
    [SerializeField]
    GameObject[] tutorialList;

    [SerializeField]
    Text jumpText;
    [SerializeField]
    Text shootText;
    [SerializeField]
    Text aimText;
    [SerializeField]
    Text parachuteText;
    [SerializeField]
    Camera playerCam;
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    Dialogues dialogueScript;

    //Determination
    float timer;
    bool isCutscene;
    bool isCredit;
    bool canMoveCamera;
    bool creditStart;
    bool creditDone;
    bool firstTrigger;
    bool dialoguePlayed;

    //GameObject to destroy to proceed
    [SerializeField]
    GameObject firstBlock;
    [SerializeField]
    GameObject secondBlock;

    //Every position to change cutscene state
    Vector3 startPosition = new Vector3(-524.8f, 250.0f, 404.8f);
    Vector3 cityFirstPos = new Vector3(-524.8f, 250.0f, -247.4f);
    Vector3 cityEndPosition = new Vector3(28.3f, 250.0f, -247.4f);
    Vector3 firstFallPosition = new Vector3(28.3f, 200.0f, -247.4f);
    Vector3 firstBlockPosition = new Vector3(28.3f, 190.0f, -247.4f);
    Vector3 secondFallPosition = new Vector3(28.3f, 150.0f, -247.4f);
    Vector3 secondBlockPosition = new Vector3(28.3f, 140.0f, -247.4f);
    Vector3 thirdFallPosition = new Vector3(28.3f, 100.0f, -247.4f);
    Vector3 lastFallPosition = new Vector3(28.3f, 1.1f, -247.4f);

    //To check if the lerp ran
    public bool firstCityScene;
    public bool endCityScene;
    public bool shakeScene;
    public bool firstFall;
    public bool firstBlockRotate;
    public bool secondFall;
    public bool secondBlockRotate;
    public bool thirdFall;
    public bool parachute;
    public bool lastFall;

    public Animator endCreditAnim;
    public Animator cutsceneCreditAnim;

    Gun playerGun;
    Objectives obj;

    bool firstTutorialEnd;
    bool secondTutorialEnd;
    bool thirdTutorialEnd;
    bool fourthTutorialEnd;

    // Start is called before the first frame update
    void Start()
    {
        playerGun = gameObject.GetComponent<Gun>();
        obj = gameObject.GetComponent<Objectives>();
        c_State = 0;
        isCredit = true;
        isCutscene = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(isCredit && !mainMenu.GetComponent<MainMenu>().getMainMenuStatus())
        {
            if(!creditStart)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Credit") != true)
                {
                    creditStart = true;
                    endCreditAnim.Play("Credit");
                    StartCoroutine(CreditTimer());
                }
            }
        }*/
        if (GetIsCutscene() && !mainMenu.GetComponent<MainMenu>().getMainMenuStatus())
        {
            if(c_State == CUTSCENE_STATE.AUDIO)
            {
                AudioManager.instance.Play("Helicopter", "SFX");
                if (AudioManager.instance.FindIsPlaying("Helicopter", "SFX"))
                {
                    c_State = CUTSCENE_STATE.CITY;
                }
            }
            else if (c_State == CUTSCENE_STATE.CITY)
            {
                if (cutsceneCreditAnim.GetCurrentAnimatorStateInfo(0).IsName("CutsceneCredit") != true)
                {
                    cutsceneCreditAnim.Play("CutsceneCredit");
                }
                canMoveCamera = false;
                playerCam.transform.localRotation = Quaternion.Euler(30, 0, 0);
                if (!firstCityScene)
                {
                    transform.position = startPosition;
                    transform.localRotation = Quaternion.Euler(0, 90f, 0);
                    StartCoroutine(LerpPosition(cityFirstPos, 15f, firstCityScene));
                    firstCityScene = true;
                }
                if (transform.position == cityFirstPos)
                {
                    Vector3 rotation = new Vector3(0, 0, 0);
                    dialogueScript.PlayDialogue_1();
                    StartCoroutine(LerpRotate(Quaternion.Euler(rotation), 3f, endCityScene));
                    StartCoroutine(LerpPosition(cityEndPosition, 15f, endCityScene));
                    endCityScene = true;
                }
                if (transform.position == cityEndPosition)
                {
                    c_State = CUTSCENE_STATE.SHAKE;
                }
            }
            else if (c_State == CUTSCENE_STATE.SHAKE)
            {
                if(!dialoguePlayed)
                {
                    dialogueScript.PlayDialogue_2();
                    dialogueScript.PlayDialogue_3();
                    dialoguePlayed = true;
                }
                StartCoroutine(CamShake(5f, 5f, shakeScene));
                shakeScene = true;
                timer += Time.deltaTime;
                if (timer >= 5f)
                {
                    c_State = CUTSCENE_STATE.JUMP;
                    timer = 0;
                }
            }
            else if (c_State == CUTSCENE_STATE.JUMP)
            {
                jumpText.gameObject.SetActive(true);
                if (Input.GetKey(KeyCode.W))
                {
                    c_State = CUTSCENE_STATE.FIRST_FALL;
                    jumpText.gameObject.SetActive(false);
                    StartCoroutine(ExplosionAudio());
                }
                firstBlock.SetActive(true);
                secondBlock.SetActive(true);
            }
            else if (c_State == CUTSCENE_STATE.FIRST_FALL)
            {
                firstBlock.transform.position = transform.position + new Vector3(0, 20, 0);
                secondBlock.transform.position = transform.position + new Vector3(0, 50, 0);
                StartCoroutine(CamRotate(Quaternion.Euler(0, 0, 0), 1f, firstFall));
                StartCoroutine(LerpRotate(Quaternion.Euler(90, 0, 0), 1f, firstFall));
                StartCoroutine(LerpPosition(firstFallPosition, 5f, firstFall));
                firstFall = true;
                transform.rotation = Quaternion.Euler(90, 90, 90);
                if (transform.position == firstFallPosition)
                {
                    c_State = CUTSCENE_STATE.FIRST_BLOCK;
                }
            }
            else if (c_State == CUTSCENE_STATE.FIRST_BLOCK)
            {
                firstBlock.transform.position = transform.position + new Vector3(0, 20, 0);
                secondBlock.transform.position = transform.position + new Vector3(0, 50, 0);
                StartCoroutine(LerpRotate(Quaternion.Euler(270, 90, 90), 1f, firstBlockRotate));
                StartCoroutine(LerpPosition(firstBlockPosition, 1f, firstBlockRotate));
                firstBlockRotate = true;
                shootText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    AudioManager.instance.Play("Shoot", "SFX");
                    firstBlock.SetActive(false);
                    c_State = CUTSCENE_STATE.SECOND_FALL;
                    shootText.gameObject.SetActive(false);
                }
            }
            else if (c_State == CUTSCENE_STATE.SECOND_FALL)
            {
                StartCoroutine(ObjectLerpPosition(secondBlock, transform.position + new Vector3(0, 35, 0), 4f, secondFall));
                StartCoroutine(LerpPosition(secondFallPosition, 4f, secondFall));
                secondFall = true;
                if (transform.position == secondFallPosition)
                {
                    c_State = CUTSCENE_STATE.SECOND_BLOCK;
                }
            }
            else if (c_State == CUTSCENE_STATE.SECOND_BLOCK)
            {
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    playerGun.Zoom();
                    aimText.gameObject.SetActive(false);
                    shootText.gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        AudioManager.instance.Play("Shoot", "SFX");
                        secondBlock.SetActive(false);
                    }
                }
                else
                {
                    playerGun.UnZoom();
                    aimText.gameObject.SetActive(true);
                    shootText.gameObject.SetActive(false);
                }
                if (!secondBlock.activeSelf)
                {
                    playerGun.UnZoom();
                    aimText.gameObject.SetActive(false);
                    shootText.gameObject.SetActive(false);
                    StartCoroutine(LerpRotate(Quaternion.Euler(90, 90, 90), 1f, secondBlockRotate));
                    StartCoroutine(LerpPosition(secondBlockPosition, 1f, secondBlockRotate));
                    secondBlockRotate = true;
                    transform.rotation = Quaternion.Euler(90, 0, 0);
                    if (transform.rotation == Quaternion.Euler(90, 0, 0))
                    {
                        c_State = CUTSCENE_STATE.THIRD_FALL;
                    }
                }
            }
            else if (c_State == CUTSCENE_STATE.THIRD_FALL)
            {
                StartCoroutine(LerpPosition(thirdFallPosition, 4f, thirdFall));
                thirdFall = true;
                if (transform.position == thirdFallPosition)
                {
                    c_State = CUTSCENE_STATE.PARACHUTE;
                }
            }
            else if (c_State == CUTSCENE_STATE.PARACHUTE)
            {
                parachuteText.gameObject.SetActive(true);
                if (Input.GetKey(KeyCode.Space))
                {
                    StartCoroutine(LerpRotate(Quaternion.Euler(0, 0, 0), 1f, parachute));
                    parachute = true;
                    parachuteText.gameObject.SetActive(false);
                    c_State = CUTSCENE_STATE.LAST_FALL;
                }
            }
            else if (c_State == CUTSCENE_STATE.LAST_FALL)
            {
                canMoveCamera = true;
                StartCoroutine(LerpPosition(lastFallPosition, 10f, lastFall));
                lastFall = true;
                if (transform.position == lastFallPosition)
                {
                    c_State = CUTSCENE_STATE.END;
                }
            }
            else if (c_State == CUTSCENE_STATE.END)
            {
                isCutscene = false;
                canMoveCamera = true;
                dialogueScript.PlayDialogue_4_5();
                StartCoroutine(TutorialStart());
            }
        }
        if (c_State == CUTSCENE_STATE.TUTORIAL)
        {
            if(!firstTrigger)
            {
                tutorialList[0].SetActive(true);
                tutorialList[4].SetActive(true);
                firstTrigger = true;
                StartCoroutine(firstTutorial());
            }
            else if (tutorialList[0].activeSelf && (Input.GetKeyDown(KeyCode.Space) || firstTutorialEnd))
            {
                StartCoroutine(secondTutorial());
                tutorialList[0].SetActive(false);
                tutorialList[1].SetActive(true);
            }
            else if (tutorialList[1].activeSelf && (Input.GetKeyDown(KeyCode.Space) || secondTutorialEnd))
            {
                StartCoroutine(thirdTutorial());
                tutorialList[1].SetActive(false);
                tutorialList[2].SetActive(true);
            }
            else if (tutorialList[2].activeSelf && (Input.GetKeyDown(KeyCode.Space) || thirdTutorialEnd))
            {
                StartCoroutine(fourthTutorial());
                tutorialList[2].SetActive(false);
                tutorialList[3].SetActive(true);
            }
            else if (tutorialList[3].activeSelf && (Input.GetKeyDown(KeyCode.Space) || fourthTutorialEnd))
            {
                tutorialList[3].SetActive(false);
                tutorialList[4].SetActive(false);
            }

            if(obj.secondObjectiveCompletedCount >= 5)
            {
                if (endCreditAnim.GetCurrentAnimatorStateInfo(0).IsName("Credit") != true)
                {
                    endCreditAnim.Play("Credit");
                    StartCoroutine(EndGameRestart());
                }
            }
        }
    }

    public bool GetIsCutscene()
    {
        return isCutscene;
    }

    public bool GetCanMoveCamera()
    {
        return canMoveCamera;
    }

    //Player Position
    IEnumerator LerpPosition(Vector3 targetPos, float duration, bool isRun)
    {
        if (!isRun)
        {
            float time = 0;
            Vector3 startPosition = transform.position;

            while (time < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPos, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;
        }
    }

    //Camera Shake
    IEnumerator CamShake(float magnitude, float duration, bool isRun)
    {
        if (!isRun)
        {
            Vector3 originalPos = playerCam.transform.localPosition;
            float time = 0.0f;

            while (time < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                playerCam.transform.localPosition = new Vector3(x, y, originalPos.z);
                time += Time.deltaTime;
                yield return null;
            }

            playerCam.transform.localPosition = originalPos;
        }
    }

    //Player Rotation
    IEnumerator LerpRotate(Quaternion targetRot, float duration, bool isRun)
    {
        if (!isRun)
        {
            float time = 0;
            Quaternion startRotation = transform.rotation;

            while (time < duration)
            {
                transform.localRotation = Quaternion.Lerp(startRotation, targetRot, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            transform.rotation = targetRot;
        }
    }

    //Camera Rotation
    IEnumerator CamRotate(Quaternion targetRot, float duration, bool isRun)
    {
        if (!isRun)
        {
            float time = 0;
            Quaternion startRotation = playerCam.transform.localRotation;

            while (time < duration)
            {
                playerCam.transform.localRotation = Quaternion.Lerp(startRotation, targetRot, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            playerCam.transform.localRotation = targetRot;
        }
    }

    IEnumerator ObjectLerpPosition(GameObject ob, Vector3 targetPos, float duration, bool isRun)
    {
        if (!isRun)
        {
            float time = 0;
            Vector3 startPosition = ob.transform.position;

            while (time < duration)
            {
                ob.transform.position = Vector3.Lerp(startPosition, targetPos, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            ob.transform.position = targetPos;
        }
    }

    IEnumerator ExplosionAudio()
    {
        yield return new WaitForSeconds(2.0f);
        AudioManager.instance.Stop("Helicopter", "SFX");
        AudioManager.instance.Play("HelicopterExplosion", "SFX");
    }

    IEnumerator CreditTimer()
    {
        yield return new WaitForSeconds(43.0f);
        creditDone = true;
    }

    IEnumerator TutorialStart()
    {
        yield return new WaitForSeconds(7.5f);
        c_State = CUTSCENE_STATE.TUTORIAL;
    }

    IEnumerator EndGameRestart()
    {
        yield return new WaitForSeconds(41.35f);
        SceneManager.LoadScene(0);
    }

    IEnumerator firstTutorial()
    {
        yield return new WaitForSeconds(5.0f);
        firstTutorialEnd = true;
    }

    IEnumerator secondTutorial()
    {
        yield return new WaitForSeconds(5.0f);
        secondTutorialEnd = true;
    }

    IEnumerator thirdTutorial()
    {
        yield return new WaitForSeconds(5.0f);
        thirdTutorialEnd = true;
    }

    IEnumerator fourthTutorial()
    {
        yield return new WaitForSeconds(5.0f);
        fourthTutorialEnd = true;
    }
}
