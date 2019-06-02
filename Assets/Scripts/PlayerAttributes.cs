using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [Header("Player Atrtributes")]
    public int totalHealth = 100;
    public int waterCollected = 0;
    public int totalAir = 100;

    [Header("HUD Object")]
    public GameObject hudObject;

    private int currentAir;
    private int currentHealth;
    private float lavaCollisionStartTime;
    private float lavaColisionDuration;
    private bool collidingWithLava;

    private void Start()
    {
        currentAir = totalAir;
        currentHealth = totalHealth;
        lavaCollisionStartTime = 0;
        collidingWithLava = false;
    }

    private void Update()
    {
        hudObject.GetComponent<UIManager>().setWaterCount(waterCollected);
        hudObject.GetComponent<UIManager>().setHealth(currentHealth);
        hudObject.GetComponent<UIManager>().setAir(currentAir);

        Debug.Log(lavaColisionDuration);

    }

    private void FixedUpdate()
    {
        if (collidingWithLava) {
            lavaColisionDuration += Time.deltaTime;
        }

        if (lavaColisionDuration >= 0.2f) {
            lavaColisionDuration = 0;
            currentHealth -= 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "WaterDrop") {
            Destroy(collision.gameObject);
            waterCollected += 1;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.tag == "Lava") {
            collidingWithLava = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Lava")
        {
            collidingWithLava = false;
            lavaColisionDuration = 0f;
            lavaCollisionStartTime = 0f;
        }
    }
}
