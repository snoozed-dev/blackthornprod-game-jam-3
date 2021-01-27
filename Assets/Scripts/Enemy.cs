using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float health = 2000;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HealthControl();
        AIControl();
    }

    void AIControl()
    {
        Vector3 playerPosition = FindObjectOfType<Player>().transform.position;
        transform.LookAt(new Vector3(playerPosition.x, 0, playerPosition.z));
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void HealthControl()
    {
        if (health <= 0) Destroy(gameObject);

    }

    public void GetDamaged(float damage)
    {
        health -= damage;
    }

}
