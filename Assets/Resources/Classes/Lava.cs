using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == GameObject.Find("Player"))
        {
            GameObject.Find("Player").GetComponent<PlayerClass>().Damage(100);
        }
    }
}
