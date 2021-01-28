using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 12f;
    public float decelerationSpeedOnAir = 4f;
    public float verticalClamp = 80f;
    public float jumpForce = 7f;

    public float IS = 1000;
    public float ISDecreaseRate = 10;
    public float ISDecreaseShootAmount = 50;

    public Text ISIndicator;

    public float gravity = 12f;
    float verticalLookRotation;
    Vector3 characterHorizontalVelocityOnAir;
    public Vector3 characterVelocity;
    Transform cameraTransform;
    CharacterController characterController;
    public bool canDie;
    bool canSecondJump;

    PlayerInputManager inputManager;

    bool isGrounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = GetComponentInChildren<Camera>().transform;
        ISIndicator = GetComponentInChildren<Text>();
        characterController = GetComponent<CharacterController>();
        inputManager = GetComponent<PlayerInputManager>();
    }

    void Update()
    {
        GoundControl();
        CameraControl();
        MetaControls();
        MovementControl();
        ShootControl();

        ISControl();
        if (canDie) HealthControl();
    }

    void HealthControl()
    {
        if (IS <= 0)
        {
            Destroy(gameObject);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void ISControl()
    {
        IS -= Time.deltaTime * ISDecreaseRate;
        ISIndicator.text = Mathf.Round(IS).ToString();
    }
    void GoundControl()
    {
        if (characterController.isGrounded)
        {
            if (!isGrounded) characterHorizontalVelocityOnAir = Vector3.zero;
            isGrounded = true;
        }
        else
        {
            if (isGrounded) characterHorizontalVelocityOnAir = Vector3.ProjectOnPlane(characterVelocity, Vector3.up);
            isGrounded = false;
        }
    }

    void ShootControl()
    {
        if (inputManager.GetFireDown())
        {
            RaycastHit hit;
            Physics.Raycast(new Ray(cameraTransform.position, cameraTransform.forward), out hit, 100, ~(1 << 9));
            GetComponentInChildren<GunController>().AnimateShoot(IS, hit.point);
            if (hit.collider)
            {
                if (hit.collider.GetComponent<Enemy>())
                {
                    hit.collider.GetComponent<Enemy>().GetDamaged(IS);
                }
            }
            IS -= ISDecreaseShootAmount;
        }

    }
    void MovementControl()
    {
        if (Input.GetButtonDown("Jump") && (isGrounded || canSecondJump))
        {
            characterVelocity = Vector3.ProjectOnPlane(characterVelocity, Vector3.up) + Vector3.up * jumpForce;
            if (!characterController.isGrounded) canSecondJump = false;
        }
        if (isGrounded) canSecondJump = true;



        Vector3 moveInput = transform.TransformVector(inputManager.GetMoveInput());

        if (isGrounded)
        {
            moveInput *= moveSpeed;
            characterVelocity = Vector3.Lerp(characterVelocity, moveInput, 15f * Time.deltaTime);
        }
        else
        {

            float verticalVelocity = characterVelocity.y;
            characterHorizontalVelocityOnAir = Vector3.Lerp(characterHorizontalVelocityOnAir, moveInput * moveSpeed, decelerationSpeedOnAir * Time.deltaTime);
            characterVelocity = characterHorizontalVelocityOnAir + (Vector3.up * verticalVelocity);

            characterVelocity += Vector3.down * gravity * Time.deltaTime;
        }

        characterController.Move(characterVelocity * Time.deltaTime);
    }

    void CameraControl()
    {
        transform.Rotate(Vector3.up * inputManager.GetLookInputsHorizontal(), Space.Self);

        float lookY = inputManager.GetLookInputsVertical();
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
