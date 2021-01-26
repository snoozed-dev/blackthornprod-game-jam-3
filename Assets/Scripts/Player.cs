using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    float moveSpeed = 7f;
    float mouseSensitivity = 7f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraControl();
        MovementControl();
        MetaControls();
    }

    void MovementControl()
    {
        float moveX = Input.GetAxis("Horizontal") * (moveSpeed / 100);
        float moveY = Input.GetAxis("Vertical") * (moveSpeed / 100);
        transform.Translate(new Vector3(moveX,0,moveY));
    }

    void CameraControl()
    {
        Transform cameraTransform = GetComponentInChildren<Camera>().transform;
        float lookX = Input.GetAxis("Look-X") * mouseSensitivity;
        float lookY = Input.GetAxis("Look-Y") * mouseSensitivity;
        Quaternion y = Quaternion.AngleAxis(lookY, -Vector3.right);
        Quaternion targetY = cameraTransform.rotation * y;
        print(Quaternion.Angle(cameraTransform.rotation, targetY));
        if (Quaternion.Angle(cameraTransform.rotation, targetY) < 90) cameraTransform.rotation = targetY;
        transform.Rotate(Vector3.up * lookX);
        // newVerticalRotation = Mathf.Clamp(newVerticalRotation, -90,90);
    }

    void MetaControls()
    {
        // SALIR DEL JUEGITO
        if (Input.GetButton("Cancel"))
        {
            EditorApplication.ExitPlaymode();
        }
    }
}
