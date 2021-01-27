using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBallScript : MonoBehaviour
{
    public float timeout = 8;

    void Update()
    {
        timeout -= Time.deltaTime;
        if (timeout <= 0) Destroy(gameObject);
    }
}
