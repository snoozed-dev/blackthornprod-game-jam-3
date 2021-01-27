using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Player : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float mouseSensitivity = 1000f;
    public float verticalClamp = 80f;
    public float jumpForce = 7f;

    public float IS = 1000;
    public float ISDecreaseRate = 10;
    public float ISDecreaseShootAmount = 50;

    public Text ISIndicator;

    public float verticalVelocity = 0f;
    public float gravity = 1.5f;
    float verticalLookRotation;
    Transform cameraTransform;
    CharacterController characterController;
    bool canSecondJump;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = GetComponentInChildren<Camera>().transform;
        ISIndicator = GetComponentInChildren<Text>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        CameraControl();
        MetaControls();
        MovementControl();
        ShootControl();

        ISControl();
    }

    void FixedUpdate()
    {
        FixedMovementControl();
    }

    void ISControl()
    {
        IS -= Time.deltaTime * ISDecreaseRate;
        ISIndicator.text = Mathf.Round(IS).ToString();
    }

    void ShootControl()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Physics.Raycast(new Ray(cameraTransform.position, cameraTransform.forward), out hit);
            if (hit.collider)
            {
            }
        }

    }
    void MovementControl()
    {
        if (Input.GetButtonDown("Jump") && (characterController.isGrounded || canSecondJump))
        {
            verticalVelocity = jumpForce / 50;
            if (!characterController.isGrounded) canSecondJump = false;
        }
        if (characterController.isGrounded) canSecondJump = true;
    }
    void FixedMovementControl()
    {
        Vector2 rawInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rawInput = Vector2.ClampMagnitude(rawInput, 1f);
        float moveX = rawInput.x * moveSpeed * Time.fixedDeltaTime;
        float moveY = rawInput.y * moveSpeed * Time.fixedDeltaTime;

        if (!characterController.isGrounded)
        {
            verticalVelocity -= (gravity / 5) * Time.fixedDeltaTime;
            verticalVelocity = Mathf.Clamp(verticalVelocity, -10, 10);
        }

        characterController.Move(transform.TransformDirection(new Vector3(moveX, verticalVelocity, moveY)));
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
