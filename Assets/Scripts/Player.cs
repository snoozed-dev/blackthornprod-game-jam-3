using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float mouseSensitivity = 1000f;
    public float verticalClamp = 80f;
    public float jumpForce = 320f;

    float verticalLookRotation;
    Transform cameraTransform;
    Rigidbody rigidbody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = GetComponentInChildren<Camera>().transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CameraControl();
        MetaControls();
        MovementControl();
    }

    void FixedUpdate()
    {
        FixedMovementControl();    
    }
    void MovementControl()
    {

        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(Vector3.up * jumpForce);
        }
    }
    void FixedMovementControl()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(new Vector3(moveX, 0, moveY)));
    }

    void CameraControl()
    {
        float lookX = Input.GetAxis("Look-X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * lookX);

        float lookY = Input.GetAxis("Look-Y") * mouseSensitivity * Time.deltaTime;
        verticalLookRotation += lookY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -verticalClamp, verticalClamp);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void MetaControls()
    {
        // SALIR DEL JUEGITO
        if (Input.GetButton("Cancel"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
