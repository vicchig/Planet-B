using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [Header("Player Atrtributes")]
    public int totalHealth = 100;
    public int waterCollected = 0;
    public int totalAir = 100;
    public int heat = 0;

    [Header("Attribute Timers")]
    public float airDepletionTime = 2f;
    public float lavaCollisionTime = 0.2f;
    public float outOfAirHealthDecrementTime = 0.5f;
    public float healthRegenTime = 3f;

    [Header("Attribute Decrement Rates")]
    public int healthDecrementOutOfAir = 1;
    public int healthDecrementLava = 2;
    public int airDecrement = 1;
    public int healthRegenRate = 1;

    [Header("HUD Object")]
    public GameObject hudObject;

    private int currentAir;
    private int currentHealth;
    private float lavaColisionDuration;
    private bool collidingWithLava;

    private float airDepletionTimer;
    private float outOfAirHealthDepletionTimer;
    private float healthRegenTimer;

    private void Start()
    {
        currentAir = totalAir;
        currentHealth = totalHealth;
        collidingWithLava = false;
        healthRegenTimer = healthRegenTime;
    }

    private void Update()
    {
        if (currentHealth == 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        
        hudObject.GetComponent<UIManager>().setWaterCount(waterCollected);
        hudObject.GetComponent<UIManager>().setHealth(currentHealth);
        hudObject.GetComponent<UIManager>().setAir(currentAir);
        hudObject.GetComponent<UIManager>().setHeat(heat);
    }

    private void FixedUpdate()
    {
        //lava timer 
        if (collidingWithLava) {
            lavaColisionDuration += Time.deltaTime;
        }

        //lava timed health decrement
        if (lavaColisionDuration >= lavaCollisionTime) {
            lavaColisionDuration = 0;
            currentHealth -= healthDecrementLava;
        }

        //air timer
        if (airDepletionTimer >= airDepletionTime) {
            currentAir -= airDecrement;
            airDepletionTimer = 0;
        }

        //Attribute bounds and air health decrement
        if (currentAir < 0) {
            currentAir = 0;
        }
        else if (currentAir == 0 && outOfAirHealthDepletionTimer >= airDepletionTime) {
            currentHealth -= healthDecrementOutOfAir;
            outOfAirHealthDepletionTimer = 0;
        }
        else if (currentAir > totalAir) {
            currentAir = totalAir;
        }

        if (currentHealth <= 0) {
            currentHealth = 0;
        }
        else if (currentHealth >= totalHealth) {
            currentHealth = totalHealth;
        }
        Debug.Log(healthRegenTimer);

        if (healthRegenTimer < healthRegenTime)
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

        healthRegenTimer += Time.deltaTime;
        outOfAirHealthDepletionTimer += Time.deltaTime;
        airDepletionTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Lava")
        {
            collidingWithLava = true;
        }
        if (collision.CompareTag("Sun Ray"))
        {
            heat += 1;
            Destroy(collision.gameObject);
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
        }
    }

    public void SetCurrentHealth(int health) {
        currentHealth = health;
    }

    public int GetCurrentHealth() {
        return currentHealth;
    }

    public int GetCurrentAir() {
        return currentAir;
    }

    public void SetCurrentAir(int air) {
        currentAir = air;
    }

    public void SetCurrentWater(int water) {
        waterCollected = water;
    }

    public int GetCurrentWater() {
        return waterCollected;
    }

    public int GetMaxHealth() {
        return totalHealth;
    }

    public int GetMaxAir()
    {
        return totalAir;
    }
}
