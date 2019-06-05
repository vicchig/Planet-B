using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : Bullet
{
    //public Transform firePoint;
    //public Transform firePoint2;
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //shootDirection = firePoint2.position - firePoint.position;
        shootDirection = transform.right;
        shootDirection.y = -1f;
        rb.velocity = shootDirection.normalized * speed;
        //Debug.Log(rb.velocity);
    }

    //private void LateUpdate()
    //{
    //    shootDirection = firePoint2.position - firePoint.position;

    //    rb.velocity = -shootDirection.normalized * speed;
    //}
    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.CompareTag("Player"))
        {
            if (collision.CompareTag("DynamicParticle"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            Debug.Log(collision.transform.position);
            Debug.Log(collision.tag);
            
        }
    }
}
