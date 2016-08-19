using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    private Rigidbody rb;
    //public GameObject ps;
    void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * 5);
        Destroy(gameObject, 1);

        //Instantiate(ps, transform.position, transform.rotation);
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject != gameObject.transform.parent)
        {
            if(other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyClass>().LoseHealth(3);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().Damage(-1);//Heals the player
            }
            else if(other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerClass>().Damage(1);
            }
            Destroy(gameObject);
        }
    }
}
