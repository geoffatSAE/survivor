using UnityEngine;
using System.Collections;

public class NPCGun : MonoBehaviour
{
    public GameObject bulletObject;
    public bool fire;

    public float fireRate;
    public float maxFireRate;

    void Start()
    {
        fire = false;

        maxFireRate = 1f;
        fireRate = maxFireRate;
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (fireRate > 0)
            {
                fireRate -= Time.deltaTime;
            }
            if (fireRate <= 0)
            {
                if (fire)
                {
                    Instantiate(bulletObject, transform.position, transform.rotation); //Instantiates the bullet in a way so that its values may be modified post instantiation
                    fireRate = maxFireRate;
                }
            }
        }
    }
}
