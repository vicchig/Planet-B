using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public int burnAt;
    public int freezeAt;
    public Vector2Int transpirationRange;
    public float energyIncrementTime;
    public int[] energyIncrements; //0 - freeze, 1 - mild, 2 - dry

    private int heatEnergy;
    private int[] areas; //0 - freeze, 1 - mild, 2 - dry
    private float[] areaTimers;
    void Start()
    {
        heatEnergy = 0;
        areas = new int[3];
        areaTimers = new float[areas.Length];
    }

    void Update()
    {
        energyIncrement();
        energyCheck();
    }

    private void FixedUpdate()
    {
        timerIncrement();
    }

    private void timerIncrement() {
        for (int i = 0; i < areas.Length; i++) {
            if (areas[i] == 1) {
                areaTimers[i] += Time.fixedDeltaTime;
            }
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
        if (heatEnergy >= burnAt)
        {
            heatEnergy = burnAt;
            Debug.Log("Burning");
        }
        else if (heatEnergy >= transpirationRange.x && heatEnergy <= transpirationRange.y)
        {
            if (areas[1] == 1) {
                heatEnergy = transpirationRange.x + 1;
            }
            Debug.Log("Transpirating");
        }
        else if (heatEnergy <= freezeAt)
        {
            heatEnergy = freezeAt;
            Debug.Log("Freezing");
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
}
