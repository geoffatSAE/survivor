using UnityEngine;
using System.Collections;

public class DistanceMeasurer : MonoBehaviour
{
    private int range;
    void Start()
    {
        range = 2;
    }
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.right + transform.up, range))
        {
            //Destroy(GetComponent<Rigidbody>());
            transform.GetComponent<Rigidbody>().velocity += Vector3.up*3;
        }

        Debug.DrawRay(transform.position, -range * transform.up, Color.green);
        Debug.DrawRay(transform.position, range * transform.up, Color.green);
        Debug.DrawRay(transform.position, -range * transform.right, Color.green);
        Debug.DrawRay(transform.position, range * transform.right, Color.green);
        Debug.DrawRay(transform.position, range * transform.right + range * transform.up, Color.green);
        Debug.DrawRay(transform.position, range * transform.right + -range * transform.up, Color.green);
    }
}
