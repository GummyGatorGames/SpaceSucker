using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject explo;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        Destroy(this.gameObject, 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(explo, transform.position*1.01f, transform.rotation);
            Instantiate(explo, transform.position*0.9f, transform.rotation);
            Instantiate(explo, transform.position*1.1f, transform.rotation);
            Destroy(this.gameObject);
        } else if (collision.gameObject.tag == "Player")
        {
            Instantiate(explo, transform.position * 1.01f, transform.rotation);
            Destroy(this.gameObject);
        }
        Instantiate(explo, transform.position, transform.rotation);
    }


}
