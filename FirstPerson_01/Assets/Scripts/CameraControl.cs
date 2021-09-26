using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;

    public Transform playerObject;

    public float x_Rotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        x_Rotation -= mouseY;
        x_Rotation = Mathf.Clamp(x_Rotation, -90.0f, 90f);

        //playerObject.GetComponent<Gun>().Recoil(playerObject.GetComponent<Gun>().isShooting, x_Rotation, transform);

        transform.localRotation = Quaternion.Euler(x_Rotation, 0.0f, 0.0f);
        playerObject.Rotate(Vector3.up * mouseX);
    }
}
