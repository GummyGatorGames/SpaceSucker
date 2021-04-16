using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float GRAVITY_PULL = 1f;
    public static float m_GravityRadius;
    public GameObject sucker;
    Vector3 ScaleTemp;
    float gravityIntensity;
    //int maxsize = 8;
    //It never ends


    // Start is called before the first frame update
    void Start()
    {
        m_GravityRadius = GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        ScaleTemp = sucker.transform.localScale;
        ScaleTemp.x += .03f * Time.deltaTime;
        ScaleTemp.y += .03f * Time.deltaTime;
        sucker.transform.localScale = ScaleTemp;

        GRAVITY_PULL += .1f * Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.attachedRigidbody || other.tag == "Bullet")
        {
            return;
        }
        
        if(other.tag == "Player")
        {
            if (other.GetComponent<PlayerControl>().isDead)
            {
                gravityIntensity = Vector3.Distance(transform.position, other.transform.position) / m_GravityRadius;
                other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * GRAVITY_PULL * 50 * Time.smoothDeltaTime);
                Debug.DrawRay(other.transform.position, transform.position - other.transform.position);

            } else
            {
                gravityIntensity = Vector3.Distance(transform.position, other.transform.position) / m_GravityRadius;
                other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * GRAVITY_PULL /5 * Time.smoothDeltaTime);
                Debug.DrawRay(other.transform.position, transform.position - other.transform.position);

            }

        } else 
        {
            gravityIntensity = Vector3.Distance(transform.position, other.transform.position) / m_GravityRadius;
            other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * GRAVITY_PULL/5 * Time.smoothDeltaTime);
            Debug.DrawRay(other.transform.position, transform.position - other.transform.position);
        }
        }
}
