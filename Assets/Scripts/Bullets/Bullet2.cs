using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : Bullet
{
    protected override void Start()
    {
        Destroy(gameObject, despawnTime); // despawner
        rb = GetComponent<Rigidbody2D>();
        shootDirection = transform.right * playerDir;
        shootDirection.y = -1f;
        rb.velocity = shootDirection.normalized * speed;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("DynamicParticle"))
            {
                GameObject steam = GameObject.Find("RisingSteam");
                Debug.Log(steam);
                RisingSteamManager rsm = steam.GetComponent<RisingSteamManager>();
                Debug.Log(rsm);
                rsm.EnableSteam();
            }
        }
    }
}
