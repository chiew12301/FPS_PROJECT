using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    public Camera camToUse;
    public Transform playerPosition; //meant to be player
    public Vector3 OffSet; //to set camera offset, use this to turn main menu

    private float CamSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        OffSet = new Vector3(0.0f, 10.0f, 0.0f); //above players head
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posToMove = playerPosition.position + OffSet;
        if(Input.GetKeyDown(KeyCode.A))
        {
            OffSet = new Vector3(0.0f, 0.0f, -100f);
        }

        Vector3 lerpPos = Vector3.Lerp(camToUse.transform.position,posToMove, CamSpeed * Time.deltaTime);
        camToUse.transform.position = lerpPos;

        camToUse.transform.LookAt(playerPosition);
    }
}
