using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public float verticalSpeed = 1f;

    void FixedUpdate()
    {
        transform.position += Vector3.up * verticalSpeed * Time.fixedDeltaTime;
    }
}
