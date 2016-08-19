using UnityEngine;
using System.Collections;

public class DefenceTurret : MonoBehaviour
{
    public GameObject target;
    public Rigidbody rb;
    public Rigidbody targetPosition;
    public bool targetFound;
    public bool setDown;
    private int range;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetFound = false;
        setDown = false;
        range = 5;
    }

    void Update ()
    {
        if (setDown && Time.timeScale > 0)
        {
            if (target != null)
            {
                float distance = Vector3.Distance(target.transform.position, gameObject.transform.position);
                if (distance <= range)
                {
                    targetFound = true;
                }
                else
                {
                    targetFound = false;
                }
            }
            else
            {
                targetFound = false;
            }

            if (target != null && targetFound)
            {
                float setrotation = (Mathf.Atan2(targetPosition.position.z - rb.position.z, targetPosition.position.x - rb.position.x) * 180 / Mathf.PI) - 90; //Finds the true bearing from this object, to its target.
                transform.rotation = Quaternion.Euler(0, -setrotation, 0);
            }
            else if (GameObject.FindGameObjectWithTag("Enemy") != null)
            {
                target = GameObject.FindGameObjectWithTag("Enemy");
                targetPosition = target.GetComponent<Rigidbody>();

                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");//Gets the instances of every enemy in the level
                float[] distances = new float[gameObjects.Length];//Will be used to store the distances of each enemy in the level

                //Debug.Log(gameObjects.Length);
                int count = 0;
                foreach (GameObject found in gameObjects)//For every enemy in the level...
                {
                    distances[count] = Vector3.Distance(found.transform.position, gameObject.transform.position);//...get the distance of each enemy
                    count++;
                }

                if (distances.Length > 1)
                {
                    for (int i = 0; i < distances.Length; i++) //Sort distances[] and gameObjects[] in ascending order in relation to distances[];
                    {
                        if (i + 1 < distances.Length)
                        {
                            if (distances[i] > distances[i + 1])
                            {
                                float d1 = distances[i];
                                float d2 = distances[i + 1];
                                distances[i] = d2;
                                distances[i + 1] = d1;

                                //Sorting gameObjects[] in ascending distance from this gameObject
                                GameObject g1 = gameObjects[i];
                                GameObject g2 = gameObjects[i + 1];
                                gameObjects[i] = g2;
                                gameObjects[i + 1] = g1;
                                i = -1;
                            }
                        }
                    }
                }

                if (targetFound == false)
                {
                    target = gameObjects[0]; //Gets closest enemy. (Minimum distance target)
                    targetPosition = target.GetComponent<Rigidbody>();
                }
            }
        }
    }

    /*public GameObject GetTarget()
    {
        return target;
    }*/
}