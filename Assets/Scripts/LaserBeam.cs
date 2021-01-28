using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float timeout = 1f;
    public float strenght;

    // Update is called once per frame
    void Update()
    {
        timeout -= Time.deltaTime;
        GetComponent<LineRenderer>().startWidth = Mathf.Clamp(timeout * (strenght / 100), 0, 5);
        if (timeout <= 0) Destroy(gameObject);
    }
}
