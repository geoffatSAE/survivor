using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerClass : MonoBehaviour
{
    Rigidbody rb;
    private bool hitGround;
    private float horrizontalMomentum;
    private float verticalMomentum;
    private Vector2 momentum;
    private float timer = 1;
    private int health;
    private float gravity = 1.30f;
    public GameObject gameOver;
    private GameObject menu;

    public Vector3 minimum;
    public Vector3 maximum;

    void Start ()
	{
        hitGround = true;
        rb = transform.GetComponent<Rigidbody>();
        health = 25;
        gameOver = GameObject.Find("GameOver");
        gameOver.GetComponentInChildren<Text>().transform.position = new Vector3(670, 400, 0);
        gameOver.SetActive(false);

        menu = GameObject.Find("MainMenu");
        menu.SetActive(false);
    }

    void Update()
    {
            if (gameOver.activeSelf && timer <= 0)
            {
                int scene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
                Time.timeScale = 1;
            }

        if (gameOver.activeSelf == false)
        {
                if (Input.GetKey(KeyCode.W) && hitGround)
            {
                verticalMomentum = 10;

            }
            else if (Input.GetKey(KeyCode.S) && !hitGround)
            {
                verticalMomentum -= 24 * Time.deltaTime * gravity;
            }

            if (!hitGround)
            {
                verticalMomentum -= 12 * Time.deltaTime * gravity;
            }

            if (Input.GetKey(KeyCode.A))
            {
                gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
                if (horrizontalMomentum > -0.4f)
                {
                    horrizontalMomentum = -0.4f;
                }
                horrizontalMomentum -= 8 * Time.deltaTime * gravity;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                if (horrizontalMomentum < 0.4f)
                {
                    horrizontalMomentum = 0.4f;
                }
                horrizontalMomentum += 8 * Time.deltaTime * gravity;
            }
            else
            {
                horrizontalMomentum /= 1.5f;
            }

            if (horrizontalMomentum > 4)
            {
                horrizontalMomentum = 4;
            }
            else if (horrizontalMomentum < -4)
            {
                horrizontalMomentum = -4;
            }

            if (rb.velocity.y > -0.01 && rb.velocity.y < 0.01 && !hitGround)
            {
                if (verticalMomentum > 0)
                {
                    verticalMomentum = 0;
                }

                if (verticalMomentum < 0)
                {
                    verticalMomentum = 0;
                }
            }

            momentum = new Vector2(horrizontalMomentum, verticalMomentum);
            rb.velocity = momentum;

            if (verticalMomentum > 0)
            {
                hitGround = false;
            }

            Vector3 Pos = transform.position;
            Pos.x = Mathf.Clamp(Pos.x, minimum.x, maximum.x);
            Pos.y = Mathf.Clamp(Pos.y, minimum.y, maximum.y);
            Pos.z = Mathf.Clamp(Pos.z, minimum.z, maximum.z);
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (gameObject.GetComponent<MeshRenderer>())
            {
                Color setColor = new Color(-timer * 2, -timer * 3.5f + 1, -timer * 2 + 1, 1);
                transform.GetComponent<Renderer>().material.color = setColor;
                if (timer - 0.5f > 0)
                {
                    transform.localScale = new Vector3(((timer - 0.5f) / 3) + 0.5f, -((timer - 0.5f) / 3) + 0.5f, ((timer - 0.5f) / 3) + 0.5f);
                }
            }
        }
    }

    void OnTriggerEnter()
    {
        if(gameOver.activeSelf == false)
        {
            hitGround = true;
            verticalMomentum = 0;
            timer = 0.75f;
        }
    }
    void OnTriggerExit()
    {
        hitGround = false;
        if (verticalMomentum == 0)
        {
            verticalMomentum = -1.5f;
        }
    }

    public void Damage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            AudioSource sound = gameObject.GetComponent<AudioSource>();
            sound.Play();
            Destroy(gameObject.GetComponent<MeshRenderer>());
            gameOver.SetActive(true);
            timer = 5;
        }
    }
}
