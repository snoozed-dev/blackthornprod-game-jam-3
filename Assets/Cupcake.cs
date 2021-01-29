using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cupcake : MonoBehaviour
{
    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();
        if (player)
        {
            player.EatCupcake();
            Destroy(gameObject);
        }
    }
}
