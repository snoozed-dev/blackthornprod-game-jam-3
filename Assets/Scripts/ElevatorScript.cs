using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (!other.transform.parent) other.transform.parent = transform;
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent == transform) other.transform.parent = null;
    }
}
