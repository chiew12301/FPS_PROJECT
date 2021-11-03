using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;

    public Transform playerObject;

    public float x_Rotation = 0.0f;

    public Vector3 gunRotation = Vector3.zero;

    private Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gun = playerObject.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject.GetComponent<Cutscene>().GetCanMoveCamera() && !PauseManager.instance.getIsPause())
        {
            if(gun.isZoom)
            {
                mouseSensitivity = 25.0f;
            }
            else
            {
                mouseSensitivity = 100.0f;
            }
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            x_Rotation -= mouseY;
            x_Rotation = Mathf.Clamp(x_Rotation, -90.0f, 90f);

            if (gunRotation != Vector3.zero)
            {
                if (x_Rotation + gunRotation.x / 1.2f <= -90)
                {
                    transform.localRotation = Quaternion.Euler(-90, gunRotation.y / 1.2f, gunRotation.z / 1.2f);
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(x_Rotation + gunRotation.x / 1.2f, gunRotation.y / 1.2f, gunRotation.z / 1.2f);
                }
            }
            else
            {
                transform.localRotation = Quaternion.Euler(x_Rotation, 0.0f, 0.0f);
            }

            //transform.localRotation = Quaternion.Euler(x_Rotation, 0.0f, 0.0f);
            playerObject.Rotate(Vector3.up * mouseX);
        }
    }

    public void SetGunRotation(Vector3 gR)
    {
        gunRotation = gR;
    }
}
