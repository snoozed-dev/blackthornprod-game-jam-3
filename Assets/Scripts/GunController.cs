using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GunController : MonoBehaviour
{
    Vector3 currentVelocity;
    Vector3 initialPosition;
    Transform playerTransform;
    Transform cameraTransform;
    Vector3 elastic;

    public float recoilMultiplier = 2f;
    float currentRecoil;
    float currentRecoilRef;



    void Start()
    {
        playerTransform = GetComponentInParent<PlayerController>().transform;
        cameraTransform = GetComponentInParent<Camera>().transform;
        initialPosition = transform.localPosition;
    }
    void Update()
    {
        // sway  
        elastic = FindObjectOfType<PlayerController>().characterVelocity / 20;
        float verticalVelocity = elastic.y;
        Vector3 horizontalVelocity = Vector3.ProjectOnPlane(elastic, Vector3.up) / 5;
        elastic = horizontalVelocity + (Vector3.up * verticalVelocity);
        elastic = Quaternion.Euler(0, -playerTransform.rotation.eulerAngles.y, 0) * elastic;
        elastic += Vector3.forward * currentRecoil;

        // recoil
        currentRecoil = Mathf.SmoothDamp(currentRecoil, 0f, ref currentRecoilRef, Time.deltaTime);


        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition - elastic, 4f * Time.deltaTime);
    }

    public void AnimateShoot(float strenght, Vector3 point)
    {
        if (point == Vector3.zero)
        {
            point = cameraTransform.position + cameraTransform.forward * 100;
        }
        Debug.DrawLine(transform.position, point, Color.red, 10f);

        LineRenderer lr = Instantiate(Resources.Load("Prefabs/LaserBeam") as GameObject).GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position - transform.right);
        lr.SetPosition(1, point);
        lr.GetComponent<LaserBeam>().strenght = strenght;
        currentRecoil = Mathf.Clamp(strenght * recoilMultiplier, 2f, 10f);

        Collider[] colliders = Physics.OverlapSphere(point, 1f);
        foreach (Collider hit in colliders)
        {
            if (hit.GetComponent<Rigidbody>())
            {
                hit.GetComponent<Rigidbody>().AddForce((point - cameraTransform.position).normalized * strenght);
            }
        }
    }
}
