using UnityEngine;
using System.Collections;
public class LocalRaycastHit:MonoBehaviour
{
	void Start()
    {

    }
	void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool rayHitSomething = Physics.Raycast(ray, out hit);
        if (rayHitSomething)
        {
            Debug.Log("world: " + hit.point);
            Vector3 localPos = hit.transform.InverseTransformPoint(hit.point);
            Debug.Log("local: " + localPos);
        }
    }
}
