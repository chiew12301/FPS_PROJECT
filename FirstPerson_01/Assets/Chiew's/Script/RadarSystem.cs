using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadarSystem : MonoBehaviour
{
    //Inspector Variables
    [Header("Assign All Object")]
    public GameObject[] AllObjectives_Object;
    [Header("Radius to detect")]
    public float Distance_To_Detect = 5.0f;

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
        if(Input.GetKeyDown(KeyCode.A))
        {
            Scan();
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
                        debugText.text = "Detected" + GO.name +" Distance Count: " + dis.ToString();
                    }
                    isScanned = true;
                    isDetected = true;
                }
                else
                {
                    //Not Scan
                    if (isDebug == true)
                    {
                        debugText.text = "Undetected. Distance Last Count:" + dis.ToString();
                    }
                }
            }
        }
    }

    public void ResetScanner()
    {
        isScanned = false;
        isDetected = false;
    }

}
