using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEvaporator : MonoBehaviour
{
    private bool evaporate;
    private float temperature;//current temperature of the water ball
    public float evaporationTemp;

    private void Start()
    {
        evaporate = false;
    }

    private void Update()
    {
        if (evaporate == true)
        {
            temperature += 1;
        }

        if (temperature > evaporationTemp)
        {
            evaporate = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "EvapArea")
        {
            evaporate = true;
        }
    }
}
