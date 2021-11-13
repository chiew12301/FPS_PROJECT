using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadarSystem : MonoBehaviour
{
    //Inspector Variables
    [Header("Assign All Object")]
    public GameObject[] AllObjectives_Object;
    public Objectives Objectives_On_Player;
    public TextMeshProUGUI radarDetectedText;
    public GameObject playerCamPos;
    public Animator Canvas_Animator;
    [Header("Radius to detect")]
    public float Distance_To_Detect = 5.0f;

    [Header("INPUT")]
    public KeyCode key;

    [Header("ForDEBUGGIN")]
    public bool isDebug = true;
    public TextMeshProUGUI debugText;

    //All Private Variables Below
    private bool isDetected = false;
    private bool isScanned = false;

    // Start is called before the first frame update
    void Start()
    {
        radarDetectedText.gameObject.SetActive(false);
        ResetScanner();
    }

    // Update is called once per frame
    void Update()
    {
        if (Objectives_On_Player.isCompletedFirstObjective == true)
        {
            dogTagObjectStatus(true);
            if (Input.GetKeyDown(key))
            {
                Scan();
            }
        }
        else
        {
            dogTagObjectStatus(false);
        }
        checkIsRadarTextActive();
    }

    public void Scan()
    {
        foreach(GameObject GO in AllObjectives_Object)
        {
            if(GO != null)
            {
                float dis = Vector3.Distance(this.gameObject.transform.position, GO.transform.position);
                Debug.Log(GO.name + " distance is " + dis.ToString());
                if (dis <= Distance_To_Detect)
                {
                    //Scanned
                    if (isDebug == true)
                    {
                        if(debugText != null)
                        {
                            //debugText.text = "Detected" + GO.name + " Distance Count: " + dis.ToString();
                        }
                        Debug.Log("Detected " + GO.name);
                    }

                    if(radarDetectedText != null)
                    {
                        radarDetectedText.gameObject.SetActive(true);
                        radarDetectedText.text = GO.name + " detected nearby.";
                    }
                    
                    isScanned = true;
                    isDetected = true;
                    break;
                }
                else
                {
                    if (radarDetectedText != null)
                    {
                        radarDetectedText.gameObject.SetActive(true);
                        radarDetectedText.text = "No dog tag is detected....";
                    }

                    //Not Scan
                    if (isDebug == true)
                    {
                        if (debugText != null)
                        {
                            //debugText.text = "Undetected. Distance Last Count:" + dis.ToString();
                        }
                    }
                }
            }
        }
    }

    public void dogTagObjectStatus(bool status)
    {
        foreach (GameObject GO in AllObjectives_Object)
        {
            if(GO != null)
            {
                GO.SetActive(status);
            }
        }
    }

    public void ResetScanner()
    {
        isScanned = false;
        isDetected = false;
    }

    private void checkIsRadarTextActive()
    {
        if(radarDetectedText != null)
        {
            if(radarDetectedText.gameObject.activeSelf == true)
            {
                StartCoroutine(disableTheText());
            }
        }
    }

    IEnumerator disableTheText()
    {
        yield return new WaitForSeconds(0.2f);
        if (Canvas_Animator.GetCurrentAnimatorStateInfo(0).IsName("DogTagAnimationText") != true)
        {
            Canvas_Animator.Play("DogTagAnimationText");
        }
        yield return new WaitForSeconds(0.3f);
        radarDetectedText.gameObject.SetActive(false);
    }

}
