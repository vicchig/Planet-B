using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public int burnAt;//amount of heatEnergy needed for the plant to start burning
    public int freezeAt; ////amount of heatEnergy needed for the plant to start freezing
    public Vector2Int transpirationRange; //the range of heatEnergy between which the plant transpirates
    public float energyIncrementTime; //time delay before each change in the heatEnergy of the tree
    public int[] energyIncrements; //0 - freeze, 1 - mild, 2 - dry, how much is added or subtracted from the heatEenrgy of the plant in each of the three areas
    public float charTime; //delay between each increment of the colour of the plant while burning
    public float freezeTime; //delay between each colour increment of the plant while freezing
    public int health; //health of the plant, disappears at <= 0
    public float healthDecrementTime; //delay between each health decrement

    private int heatEnergy;//amount of heat energy this plant has
    private int[] areas; //0 - freeze, 1 - mild, 2 - dry
    private float[] areaTimers; //current amount of time since last energy increment for each of the three areas
    private float charTimer;
    private float freezeTimer;
    private int[] states; //0 - burning, 1 - transpirating, 2 - freezing
    private float healthTimer;

    private ParticleSystem fire;
    private ParticleSystem evaporation;
    private SpriteRenderer sr;

    void Start()
    {
        heatEnergy = 0;
        areas = new int[3];
        states = new int[areas.Length];
        areaTimers = new float[areas.Length];
        charTimer = 0f;
        freezeTimer = 0f;
        healthTimer = 0f;

        evaporation = transform.GetChild(0).GetComponent<ParticleSystem>();
        fire = transform.GetChild(1).GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();

        evaporation.Pause();
        fire.Pause();
    }

    void Update()
    {
        energyIncrement();
        energyCheck();

        //health decrement
        if (healthTimer >= healthDecrementTime) {
            healthTimer = 0;
            health -= 1;

            if (heatEnergy >= burnAt)
            {
                colourChangeBurn();
            }
            else if (heatEnergy <= freezeAt)
            {
                colourChangeFreeze();
            }
        }

        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        timerIncrements();
    }

    private void timerIncrements() {
        //time before next heatEnergy increment
        for (int i = 0; i < areas.Length; i++) {
            if (areas[i] == 1) {
                areaTimers[i] += Time.fixedDeltaTime;
            }
        }

        //time before next health decrement
        if (states[0] == 1 || states[2] == 1) {//burning or freezing
            healthTimer += Time.fixedDeltaTime;
        }
        else if (states[1] == 1) {//evaporating

        }

    }

    private void energyIncrement()
    {
        for (int i = 0; i < areaTimers.Length; i++) {
            if (areaTimers[i] >= energyIncrementTime) {
                areaTimers[i] = 0f;
                heatEnergy += energyIncrements[i];
            }
        }
    }

    private void energyCheck() {
        if (heatEnergy > burnAt)
        {
            heatEnergy = burnAt;
            evaporation.Pause();
            evaporation.Clear();
            fire.Play();

            states[0] = 1;
            states[1] = 0;
            states[2] = 0;
        }
        else if (heatEnergy > transpirationRange.x && heatEnergy < transpirationRange.y)
        {
            if (areas[1] == 1) {
                heatEnergy = transpirationRange.x + 1;
            }
            evaporation.Play();
            fire.Pause();
            fire.Clear();

            states[0] = 0;
            states[1] = 1;
            states[2] = 0;
        }
        else if (heatEnergy < freezeAt)
        {
            heatEnergy = freezeAt;
            evaporation.Pause();
            fire.Pause();
            evaporation.Clear();
            fire.Clear();

            states[0] = 0;
            states[1] = 0;
            states[2] = 1;
        }
    }

    private void colourChangeBurn() {
        sr.color = new Color(sr.color.r * 0.8f, sr.color.g * 0.8f, sr.color.b * 0.8f, sr.color.a);
    }

    private void colourChangeFreeze() {
        sr.color = new Color(sr.color.r * 0.8f, sr.color.g * 0.8f, sr.color.b, sr.color.a);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet2") {
            heatEnergy += 2;
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "bullet3") {
            heatEnergy -= 2;
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "bullet1") {
            colourChangeBurn();
            health -= 1;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "TreePlantAreaFreeze") {
            areas[0] = 1;
        }
        else if (collision.gameObject.name == "TreePlantAreaMild") {
            areas[1] = 1;
        }
        else if (collision.gameObject.name == "TreePlantAreaDry") {
            areas[2] = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "TreePlantAreaFreeze")
        {
            areas[0] = 0;
        }
        else if (collision.gameObject.name == "TreePlantAreaMild")
        {
            areas[1] = 0;
        }
        else if (collision.gameObject.name == "TreePlantAreaDry")
        {
            areas[2] = 0;
        }
    }

    public int[] getStates() {
        return states;
    }
}