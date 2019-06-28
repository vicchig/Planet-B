using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [Header("Player Atrtributes")]
    public int maxHealth = 100;
    public int waterCollected = 0;
    public int maxHeat = 0;
    public int currentHeat = 0;

    [Header("Attribute Timers")]
    public float lavaCollisionTime = 0.2f;
    public float healthRegenTime = 3f;

    [Header("Attribute Decrement Rates")]
    public int healthDecrementLava = 2;

    [Header("Attribute Increment Rates")]
    public int healthRegenRate = 1;
    public int sunRayValue = 15;

    [Header("HUD Object")]
    public GameObject hudObject;

    private int currentHealth;
    private float lavaColisionDuration;
    private bool collidingWithLava;

    private float outOfAirHealthDepletionTimer;
    private float healthRegenTimer;

    private bool takingDamage;

    private void Start()
    {
        currentHealth = maxHealth;
        collidingWithLava = false;
        healthRegenTimer = healthRegenTime;
        takingDamage = false;
    }


    private void Update()
    {
        //lava timed health decrement
        if (lavaColisionDuration >= lavaCollisionTime)
        {
            lavaColisionDuration = 0;
            currentHealth -= healthDecrementLava;
        }

        //Attribute bounds and air health decrement
        //Health
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        else if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        //Heat Energy
        if (currentHeat < 0)
        {
            currentHeat = 0;
        }
        else if (currentHeat > maxHeat)
        {
            currentHeat = maxHeat;
        }

        //health regen
        if (healthRegenTimer > healthRegenTime)
        {
            healthRegenTimer = 0;

            if (GetMaxHealth() - currentHealth >= healthRegenRate)
            {
                currentHealth += healthRegenRate;
            }
            else
            {
                currentHealth = GetMaxHealth();
            }
        }
    }

    private void FixedUpdate()
    {
        //lava timer 
        if (collidingWithLava) {
            lavaColisionDuration += Time.fixedDeltaTime;
        }

        healthRegenTimer += Time.fixedDeltaTime;
        outOfAirHealthDepletionTimer += Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Lava")
        {
            collidingWithLava = true;
            takingDamage = true;
            currentHealth -= healthDecrementLava;
        }
        if (collision.CompareTag("Sun Ray"))
        {
            currentHeat += sunRayValue;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Lava") {
            collidingWithLava = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Lava")
        {
            collidingWithLava = false;
            lavaColisionDuration = 0f;
            takingDamage = false;
        }
    }

    public void SetCurrentHealth(int health) {
        currentHealth = health;
    }

    public int GetCurrentHealth() {
        return currentHealth;
    }

    public void SetCurrentWater(int water) {
        waterCollected = water;
    }

    public int GetCurrentWater() {
        return waterCollected;
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public int GetMaxHeat() {
        return maxHeat;
    }

    public int getCurrentHeat() {
        return currentHeat;
    }

    public void SetCurrentHeat(int heat) {
        currentHeat = heat;
    }

    public bool isTakingDamage(){
        return takingDamage;
    }

    public void setTakingDamage(bool tf) {
        takingDamage = tf;
    }
}
