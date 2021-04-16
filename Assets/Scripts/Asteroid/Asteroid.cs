using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject[] Asteroids;
    public GameObject energyPickup;
    //public Gameplay gameplay;
    float maxRotation;
    float rotationZ;
    Rigidbody2D rb;
    float maxSpeed;
    float speedX;
    int selectorX;
    float dirX;
    float finalSpeedX;
    float speedY; 
    int selectorY;
    float dirY;
    float finalSpeedY;
    public int AsteroidSize;
    public int health;
    GameObject AsteroidClone;

    void Start()
    {
        maxRotation = 25f;
        rotationZ = Random.Range(-maxRotation, maxRotation);

        rb = this.GetComponent<Rigidbody2D>();

        



        Destroy(this.gameObject, 60);
    }

    public void SetSize(int size)
    {
        AsteroidSize = size;
        health = size;
    }

    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, rotationZ) * Time.deltaTime);
        //rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -dynamicMaxSpeed, dynamicMaxSpeed), Mathf.Clamp(rb.velocity.y, -dynamicMaxSpeed, dynamicMaxSpeed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;

            if (health == 0 && AsteroidSize != 1)
            {
                CreateSmallAsteriods(2);
                Destroy();
            }
            else if (health == 0)
            {
                Debug.Log("Make Energy");
                Instantiate(energyPickup, new Vector3(transform.position.x, transform.position.y, 0f), transform.rotation);

                Destroy();

            }
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "InstaDeath")
        {
            Destroy();
        }

        /*if (collisionInfo.collider.name == "Rocket")
        {
            gameplay.RocketFail();
        }*/
    }

    void CreateSmallAsteriods(int asteroidsNum)
    {
        int newsize = AsteroidSize - 1;
        for (int i = 1; i <= asteroidsNum; i++)
        {
            switch (newsize)
            {
                case 1:
                    AsteroidExplosion(newsize);

                    break;
                case 2:
                    AsteroidExplosion(newsize);
                    break;
                default:
                    break;
            }
        }
    }

    private void AsteroidExplosion(int newsize)
    {

        speedX = Random.Range(20f, 40f);
        selectorX = Random.Range(0, 2);
        dirX = 0;
        if (selectorX == 1) { dirX = -1; }
        else { dirX = 1; }
        finalSpeedX = speedX * dirX;

        speedY = Random.Range(20f, 40f);
        selectorY = Random.Range(0, 2);
        dirY = 0;
        if (selectorY == 1) { dirY = -1; }
        else { dirY = 1; }
        finalSpeedY = speedY * dirY;

        AsteroidClone = Instantiate(Asteroids[newsize - 1], new Vector3(transform.position.x+(speedX/30), transform.position.y+(speedY / 30), 0f), transform.rotation);
        AsteroidClone.GetComponent<Asteroid>().SetSize(newsize);

        AsteroidClone.GetComponent<Rigidbody2D>().AddForce(transform.right * finalSpeedX);
        AsteroidClone.GetComponent<Rigidbody2D>().AddForce(transform.right * finalSpeedY);
    }

    public void Destroy()
    {
        //gameplay.asterodDestroyed();
        Destroy(gameObject, 0.01f);
    }


}
