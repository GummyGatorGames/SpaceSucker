using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public GameObject shieldObj;
    public ParticleSystem Engines;

    float thrust = 10f;
    float rotSpeed = 20f;
    float maxSpeed = 4.5f;
    Vector3 mousePos;
    float angle;
    Rigidbody2D rb;
    AudioManager audioM;
    bool canShoot = true;
    bool shield = false;
    bool alarm = false;
    float secondsBetweenShoot;
    public float elapsedTime = 0.0f;
    public float health = 100f;
    public float fuel = 100f;
    public int score;
    public bool isDead = false;

    void Start()
    {
        Engines.Stop();
        secondsBetweenShoot = .25f;
        rb = GetComponent<Rigidbody2D>();
        audioM = GetComponent<AudioManager>();
    }


    private void FixedUpdate()
    {
        if (health > 0 && fuel > 0)
        {
            ControlRocket();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            audioM.Play("NoFuel");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health> 0)
        {
            PointatMouse(); 
            if (fuel > 0)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    audioM.Play("Thrust");
                    Engines.Play();
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    audioM.Play("Shielded");
                }
                if (Input.GetMouseButtonDown(0) && (canShoot && fuel > 5))
                {
                    Shoot();
                }
                if (Input.GetKey(KeyCode.Space) && fuel > 10)
                {
                    activateShield();
                    shieldObj.SetActive(true);

                }

            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && fuel < 10)
        {
            audioM.Play("NoFuel");
        }
        if (Input.GetKeyUp(KeyCode.W) || fuel <= 0)
        {
            audioM.Stop("Thrust");
            Engines.Stop();
        }
        if (Input.GetKeyUp(KeyCode.Space) || fuel <=10)
        {
            shield = false;
            audioM.Stop("Shielded");
            shieldObj.SetActive(false);
        }
        CheckPosition();
        if(fuel <= 0)
        {
            health -= 100f * Time.deltaTime;
            if (health <= 0 && !isDead)
            {
                audioM.Play("Death");
                isDead = true;
            }
            if (!alarm)
            {
                this.GetComponent<AudioSource>().PlayOneShot(audioM.sounds[6].clip);
                alarm = true;
            }
        }

    }

    private void activateShield()
    {
        shield = true;
        fuel -= 10f * Time.deltaTime;
    }

    private void Shoot()
    {
        Debug.Log("Playing" + "shoot" + UnityEngine.Random.Range(1, 3));
        audioM.Play("shoot" + UnityEngine.Random.Range(1, 3));
        elapsedTime = 0;
        GameObject BulletClone = Instantiate(bullet, new Vector2(bullet.transform.position.x, bullet.transform.position.y), transform.rotation);
        BulletClone.SetActive(true);
        BulletClone.GetComponent<Rigidbody2D>().AddForce(transform.right * 500);
        canShoot = false;
        fuel-=5;
    }

    private void ControlRocket()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.right * thrust * Input.GetAxis("Vertical"));

            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
            fuel -= 1f * Time.fixedDeltaTime;
        }
    }

    private void PointatMouse()
    {
        elapsedTime += Time.fixedDeltaTime;

        if (elapsedTime > secondsBetweenShoot)
        {
            canShoot = true;
        }

        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotSpeed*Time.deltaTime);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward * rotSpeed * Time.fixedDeltaTime);
    }

    private void CheckPosition()
    {

        var sceneWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        var sceneHeight = Camera.main.orthographicSize * 2;

        var sceneRightEdge = sceneWidth / 2;
        var sceneLeftEdge = sceneRightEdge * -1;
        var sceneTopEdge = sceneHeight / 2;
        var sceneBottomEdge = sceneTopEdge * -1;

        if (transform.position.x > sceneRightEdge)
        {
            audioM.Play("Teleport");
            transform.position = new Vector2(sceneLeftEdge, transform.position.y);
        }
        if (transform.position.x < sceneLeftEdge)
        {
            audioM.Play("Teleport");
            transform.position = new Vector2(sceneRightEdge, transform.position.y); 
        }
        if (transform.position.y > sceneTopEdge)
        {
            audioM.Play("Teleport");
            transform.position = new Vector2(transform.position.x, sceneBottomEdge);
        }
        if (transform.position.y < sceneBottomEdge)
        {
            audioM.Play("Teleport");
            transform.position = new Vector2(transform.position.x, sceneTopEdge);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Bullet")
        {
            if(shield)
            {
                audioM.Play("Shield");
                return;
            }
            if (!isDead)
            {

                audioM.Play("Hit");
                health -= 3 * collision.relativeVelocity.magnitude;
                if (health <= 0)
                {
                    audioM.Play("Death");
                    isDead = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isDead)
        { return;
        }
        if(collision.gameObject.tag == "Energy")
        {
            if (fuel >= 75)
            {
                fuel = 100;
            } else
            {
                fuel += 25;
            }
            audioM.Play("Energy");
            score += 20;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "InstaDeath")
        {
            audioM.Play("Alarm");
            if (health <= 0 && !isDead)
            {
                audioM.Play("Death");
                isDead = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isDead)
        {
            return;
        }
        if (collision.gameObject.tag == "InstaDeath")
        {
            audioM.Stop("Alarm");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "InstaDeath")
        {
            if (!isDead)
            {
                if (fuel > 0)
                {
                    health -= 10f * Time.deltaTime;
                }
                else
                {
                    health -= 200f * Time.deltaTime;
                }

                if (health <= 0)
                {
                    audioM.Play("Death");
                    isDead = true;
                }
            }
        }
    }
}
