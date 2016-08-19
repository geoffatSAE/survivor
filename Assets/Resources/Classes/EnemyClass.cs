using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyClass : MonoBehaviour //Parent class for Sub-classes. Sub-classes inherit all functionality of this class; Cleaner code
{
    protected GameObject target;
    protected Rigidbody rb;
    protected int health;
    protected int maxHealth;
    protected int damage;
    protected int value;
    protected float speed;
    protected float radius;
    protected bool hitGround;
    protected bool jumped;
    private float travelPoint;
    private float timer;
    private float timer2;
    private Color mainColor;
    private bool fadookined;

    public RectTransform canvas;
    private GameObject tester;
    private float hitTimer = 0;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        tester = GameObject.Find("Canvas");
        canvas = tester.GetComponent<RectTransform>();
        rb = gameObject.GetComponent<Rigidbody>();
        hitGround = false;
        jumped = false;
        travelPoint = rb.position.y;
        mainColor = transform.GetComponent<Renderer>().material.color;
        fadookined = false;
    }
    void Update()
    {
        hitTimer -= Time.deltaTime;
        if (target.gameObject != null)
        {
            Debug.DrawRay(transform.position, 3 * transform.forward + 3 * transform.up, Color.green);
            Debug.DrawRay(transform.position, 1f * transform.forward + 0.25f * -transform.up, Color.green);
            Debug.DrawRay(transform.position, 3 * transform.up, Color.green);
            /*Debug.DrawRay(transform.position, 0.25f * -transform.up, Color.green);
            if (Physics.Raycast(transform.position, -transform.up, 0.25f))
            {
                hitGround = true;
            }*/
            if (Physics.Raycast(transform.position, 1f * transform.forward + 0.25f * -transform.up, 1) && !Physics.Raycast(transform.position, 0.5f * transform.forward, 0.5f) && hitGround)
            {
                jumped = false;
            }
            else if (!Physics.Raycast(transform.position, 1f * transform.forward + 0.25f * -transform.up, 1) && hitGround)
            {
                jumped = false;
            }
            else
            {
                jumped = true;
            }
            if (Physics.Raycast(transform.position, transform.forward + transform.up, 3) && !Physics.Raycast(transform.position, transform.up, 3) && !Physics.Raycast(transform.position, 0.5f * transform.forward, 0.5f) && target.GetComponent<Rigidbody>().position.y - 1.5f > rb.position.y && hitGround)
            {
                hitGround = false;
                jumped = true;
                transform.GetComponent<Rigidbody>().velocity += Vector3.up * 10;
            }
            else if (Physics.Raycast(transform.position, transform.forward + transform.up, 3) && !Physics.Raycast(transform.position, transform.up, 3) && target.GetComponent<Rigidbody>().position.x - rb.position.x < 2.5f && target.GetComponent<Rigidbody>().position.x - rb.position.x > -2.5f && target.GetComponent<Rigidbody>().position.y - 1f > rb.position.y && hitGround)
            {
                hitGround = false;
                jumped = true;
                transform.GetComponent<Rigidbody>().velocity += Vector3.up * 8;
            }
            if (!Physics.Raycast(transform.position, transform.forward + transform.up, 1) && Physics.Raycast(transform.position, 0.5f * transform.forward, 0.5f) && hitGround)
            {
                hitGround = false;
                transform.GetComponent<Rigidbody>().velocity += Vector3.up * 5;
            }

            if (!Physics.Raycast(transform.position, transform.up, 2) && target.GetComponent<Rigidbody>().position.x - rb.position.x < 4f && target.GetComponent<Rigidbody>().position.x - rb.position.x > -4f && target.GetComponent<Rigidbody>().position.y - rb.position.y > 1.5 && target.GetComponent<Rigidbody>().position.y - rb.position.y < 1.75f && hitGround)
            {
                hitGround = false;
                jumped = true;
                transform.GetComponent<Rigidbody>().velocity += Vector3.up * 10;
            }
            /*if (Physics.Raycast(transform.position, 1f * transform.forward + 0.25f * -transform.up, 1) && target.GetComponent<Rigidbody>().position.y+0.1f < rb.position.y && hitGround)
            {
                jumped = false;
                transform.GetComponent<Rigidbody>().velocity += transform.forward * (speed / 100);
            }
            else if(hitGround)
            {
                jumped = true;
            }*/

            if (target.GetComponent<Rigidbody>().position.x >= rb.position.x && jumped && hitGround)
            {
                rb.transform.rotation = Quaternion.Euler(0, 90, 0);
                jumped = false;
            }
            else if (target.GetComponent<Rigidbody>().position.x < rb.position.x && jumped && hitGround)
            {
                rb.transform.rotation = Quaternion.Euler(0, -90, 0);
                jumped = false;
            }

            if (target.GetComponent<Rigidbody>().position.x >= rb.position.x && hitGround)
            {
                if (target.GetComponent<Rigidbody>().position.y - rb.position.y > -0.5 && target.GetComponent<Rigidbody>().position.y - rb.position.y < 0.5)
                {
                    rb.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                //Debug.Log(target.GetComponent<Rigidbody>().position.y - rb.position.y);

            }
            else if (target.GetComponent<Rigidbody>().position.x < rb.position.x && hitGround)
            {
                if (target.GetComponent<Rigidbody>().position.y - rb.position.y > -0.5 && target.GetComponent<Rigidbody>().position.y - rb.position.y < 0.5)
                {
                    rb.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                //Debug.Log(target.GetComponent<Rigidbody>().position.y - rb.position.y);
            }

            if (Time.timeScale > 0 && !fadookined)
            {
                rb.velocity += transform.forward * (speed / 100); //Moving forwards

                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
                foreach (Collider hit in colliders)
                {
                    if (hit.gameObject == target) //Triggered typically when a Sub-class collides with the tower/fortress/base
                    {
                        //Destroy(gameObject);
                        //target.GetComponent<PlayerClass>().Damage(damage); //Calls a function in the TowerScript, subtracting a value from its hitPoints variable
                    }
                }
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
                timer2 += Time.deltaTime;
                if (gameObject.GetComponent<MeshRenderer>())
                {
                    Color setColor = new Color(-timer * 1, -timer * 1, -timer * 1, 1);
                    transform.GetComponent<Renderer>().material.color = setColor + mainColor;
                }
                if (timer - 0.5f > 0)
                {
                    transform.localScale = new Vector3(((timer - 0.5f) / 3) + 0.5f, -((timer - 0.5f) / 3) + 0.5f, ((timer - 0.5f) / 3) + 0.5f);
                }
            }

            if (fadookined && target.gameObject.GetComponent<Rigidbody>().position.y < rb.position.y && target.gameObject.GetComponent<Rigidbody>().position.x - rb.position.x > -0.5f && target.gameObject.GetComponent<Rigidbody>().position.x - rb.position.x < 0.5f && timer <= 1f && timer > 0f && Input.GetKey(KeyCode.W))
            {
                rb.freezeRotation = false;
                //rb.velocity += new Vector3(0, 1, 0);
                rb.angularVelocity = new Vector3(Random.Range(5, 10), Random.Range(5, 10), Random.Range(5, 10));
                rb.velocity += new Vector3(Random.Range(-0.15f, 0.15f), 0.1f, 0);
                Destroy(gameObject.GetComponent<Collider>());
                //Destroy(gameObject.GetComponent<MeshRenderer>(), 0.5f);
                Destroy(gameObject, 5f);

                if (gameObject.GetComponent<Collider>() == true)
                {
                    GameObject.Find("Spawner").GetComponent<Spawner>().removeEnemy();
                }

                /*Text newText = GameObject.Find("newText").GetComponent<Text>();
                newText.color = new Color(1, 0.1f, 0, 1);
                newText.fontSize = 25;
                newText.fontStyle = FontStyle.Bold;
                newText.text = "POW!";
                GameObject.Find("newText").transform.localScale = new Vector3(1.1f, 1.1f, 0);*/

                /*for (int i = 0; i < 5; i++)
                {
                    GameObject prefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    GameObject instance = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
                    instance.AddComponent<Rigidbody>();
                    Destroy(instance.GetComponent<Collider>());
                    instance.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                }*/
            }
            else if (timer < 1 && gameObject.GetComponent<Collider>())
            {
                fadookined = false;
                //GameObject newText = GameObject.Find("newText");
                //Destroy(newText);
                timer2 = 0;
            }

            if (GameObject.Find("newText"))
            {
                /*Text text = GameObject.Find("Canvas").GetComponent<Text>();

                Vector2 pos = gameObject.transform.position;  // get the game object position
                Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);

                RectTransform textPosition = text.GetComponent<RectTransform>();
                textPosition.transform.position = viewportPoint;

                Debug.Log(viewportPoint);*/

                /*Text newText = GameObject.Find("newText").GetComponent<Text>();
                if (GameObject.Find("newText").transform.localScale.x < 1)
                {
                    newText.color = new Color(1, (timer - 0.75f) * 2, 0, 1);
                    GameObject.Find("newText").transform.localScale = new Vector3(1 / (timer), 1 / (timer), 0);
                    newText.text = "POW";

                    RectTransform newTextRect = GameObject.Find("newText").GetComponent<RectTransform>();
                    Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, rb.position);
                    newTextRect.anchoredPosition = screenPoint - canvas.GetComponent<RectTransform>().sizeDelta / 2f;
                }
                else {
                    newText.color = new Color(1, 0.1f, 0, timer);
                    RectTransform newTextRect = GameObject.Find("newText").GetComponent<RectTransform>();
                    newTextRect.anchoredPosition += new Vector2(0, (2 / (timer2 * 2 + 0.1f)));
                }
                if (newText.color.a <= 0)
                {
                    Destroy(GameObject.Find("newText"));
                }*/
            }
        }
    }
    public void LoseHealth(int dmg) //Called when a Sub-class is hit with a bullet
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject.Find("Spawner").GetComponent<Spawner>().removeEnemy();
        }
    }
    public void ChangeDirection(int direction) //Called whenever a Sub-class collides with a boundary that contains the 'RotateCollider' class.
    {
        rb.transform.Rotate(new Vector3(0, direction, 0));
    }
    public void Difficulty(int wave) //This does not function properly. Unity is shit
    {
        //Debug.Log(wave);
        /*if(wave != 0)
        {
            health *= wave;
            damage *= wave;
            value *= wave;
        }*/
        maxHealth = health;
    }

    void OnTriggerEnter()
    {
        if (target.gameObject != null)
        {
            if (travelPoint - rb.position.y > 1 || travelPoint - rb.position.y < -1)
            {
                if (target.GetComponent<Rigidbody>().position.x >= rb.position.x)
                {
                    rb.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                else if (target.GetComponent<Rigidbody>().position.x < rb.position.x)
                {
                    rb.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
            }
            if (!fadookined)
            {
                hitGround = true;
                timer = 0.75f;
            }
            //Debug.Log(travelPoint - rb.position.y);
        }
    }
    void OnTriggerExit()
    {
        hitGround = false;
        travelPoint = rb.position.y;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.rigidbody)
        {
            if (other.gameObject == target && other.gameObject.GetComponent<Rigidbody>().position.y < rb.position.y-0.1f && other.gameObject.GetComponent<Rigidbody>().position.x - rb.position.x > -0.5f && other.gameObject.GetComponent<Rigidbody>().position.x - rb.position.x < 0.5f && timer < 0.8f && Input.GetKey(KeyCode.W))
            {
                timer = 1.25f;
                timer2 = 0;
                fadookined = true;

                //Text text = GameObject.Find("Canvas").AddComponent<Text>();
                /*GameObject newGO = new GameObject("newText");
                newGO.transform.parent = GameObject.Find("Canvas").transform;

                newGO.transform.localScale = new Vector3(0, 0, 0);

                Text newText = newGO.AddComponent<Text>();

                newText.text = "POW";
                newText.fontSize = 25;
                newText.fontStyle = FontStyle.Normal;
                newText.transform.position = rb.transform.position;
                newText.color = Color.yellow;

                Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                newText.font = ArialFont;
                newText.material = ArialFont.material;

                RectTransform newTextRect = newText.GetComponent<RectTransform>();
                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, rb.position);
                newTextRect.anchoredPosition = screenPoint - canvas.GetComponent<RectTransform>().sizeDelta / 2f;*/
            } else if (other.gameObject == target && hitTimer <= 0)
            {
                target.GetComponent<PlayerClass>().Damage(damage);
                hitTimer = 0.5f;
            }
        }
    }
}