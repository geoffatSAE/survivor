using UnityEngine;
using System.Collections;

public class Boss : EnemyClass
{

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        health = 25;
        damage = 10;
        value = 5;
        speed = 8;
        radius = 0.5f;
    }
}
