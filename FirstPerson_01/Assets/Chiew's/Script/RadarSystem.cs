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
    public Objective firstQuest;
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
        ResetScanner();
    }

    // Update is called once per frame
    void Update()
    {
        if(Objectives_On_Player.CurrentObjective != firstQuest)
        {
            if (Input.GetKeyDown(key))
            {
                dogTagObjectStatus(true);
                Scan();
            }
        }
        else
        {
            dogTagObjectStatus(false);
        }
    }

    public void Scan()
    {
        foreach(GameObject GO in AllObjectives_Object)
        {
            if(GO != null)
            {
                float dis = Vector3.Distance(this.gameObject.transform.position, GO.transform.position);
                if (dis <= Distance_To_Detect)
                {
                    //Scanned
                    if (isDebug == true)
                    {
                        if(debugText != null)
                        {
                            debugText.text = "Detected" + GO.name + " Distance Count: " + dis.ToString();
                        }
                        Debug.Log("Detected " + GO.name);
                    }
                    isScanned = true;
                    isDetected = true;
                }
                else
                {
                    //Not Scan
                    if (isDebug == true)
                    {
                        if (debugText != null)
                        {
                            debugText.text = "Undetected. Distance Last Count:" + dis.ToString();
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

}
