using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public bool isWeakSpot;
    public float damageMultiplier = 1f;

    Enemy enemy;
    PlayerController player;
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    public void HurtParent(float strenght)
    {
        if (enemy)
        {
            enemy.GetDamaged(strenght * damageMultiplier * (isWeakSpot ? 2 : 1));
        }
    }
}
