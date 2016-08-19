using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

	public GameObject bulletObject;
	private float fireRate = 0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(fireRate > 0)
        {
		    fireRate -= Time.deltaTime;
        }
		if (Input.GetKey(KeyCode.Space) && fireRate <= 0)
		{
            Instantiate(bulletObject, transform.position, transform.rotation);
			fireRate = 0.5f;
		}
	}
}
