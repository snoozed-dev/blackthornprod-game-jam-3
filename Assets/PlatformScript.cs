using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public float verticalSpeed = 1f;

    Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rigidbody.MovePosition(rigidbody.position + Vector3.up * verticalSpeed * Time.deltaTime);
    }
}
