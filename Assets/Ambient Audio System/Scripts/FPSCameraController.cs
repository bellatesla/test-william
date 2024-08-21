using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCameraController : MonoBehaviour
{
    public Transform playerTarget;


    public float mouseSensitivity = 100f;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor in the middle of the screen
    }

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate player horizontally
        playerTarget.Rotate(Vector3.up * mouseX);

        // Rotate camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit vertical rotation to prevent flipping
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}

