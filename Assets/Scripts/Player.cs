using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float mouseSensitivity = 7f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraControl();

        float moveX = Input.GetAxis("Horizontal") * (moveSpeed / 100);
        float moveY = Input.GetAxis("Vertical") * (moveSpeed / 100);
        transform.Translate(new Vector3(moveX,0,moveY));
    }

    void CameraControl()
    {
        // MIRAR COSTADOS
        float lookX = Input.GetAxis("Look-X") * mouseSensitivity;
        float lookY = Input.GetAxis("Look-Y") * mouseSensitivity;
        transform.Rotate(Vector3.up * lookX);
        GetComponentInChildren<Camera>().transform.Rotate(-Vector3.right * lookY);
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
