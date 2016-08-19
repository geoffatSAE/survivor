using UnityEngine;
using System.Collections;
public class NPCRaycaster:MonoBehaviour{
    public float speed = 0.01f;
	void Start(){}
	void Update(){
        Ray rayForward = new Ray(transform.position, transform.forward);
    }}
