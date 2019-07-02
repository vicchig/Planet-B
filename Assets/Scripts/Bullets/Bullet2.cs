using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : Bullet
{
    private GameObject manager;
    private ILevelManagerWater managerScript;
    protected override void Start()
    {
        Destroy(gameObject, despawnTime); // despawner
        rb = GetComponent<Rigidbody2D>();
        shootDirection = transform.right * playerDir;
        shootDirection.y = -1f;
        rb.velocity = shootDirection.normalized * speed;

        manager = GameObject.Find("GameManager");
        managerScript = manager.GetComponent<ILevelManagerWater>();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("DynamicParticle"))
            {
                GameObject steam = GameObject.Find("RisingSteam");
                RisingSteamManager rsm = steam.GetComponent<RisingSteamManager>();
                rsm.EnableSteam();
                managerScript.SetEvaporatedWater(managerScript.GetEvaporatedWater() + 1);
            }
            if (collision.gameObject.CompareTag("DynamicParticleL3"))
            {
                Level3DynamicParticleScript d3 = collision.gameObject.GetComponent<Level3DynamicParticleScript>();
                if (d3.isInPool && d3.evapLeft > 0)
                {
                    managerScript.SetEvaporatedWater(managerScript.GetEvaporatedWater() + 1);
                    d3.evapLeft -= 1;
                }
            }
        }
    }
}
