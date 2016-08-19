using UnityEngine;
using System.Collections;

public class EnemyBasic : EnemyClass
{

	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        health = 5;
        damage = 5;
        value = 5;
        speed = 5;
        radius = 0.5f;
	}
}
