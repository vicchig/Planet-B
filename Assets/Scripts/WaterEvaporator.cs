using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEvaporator : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "EvapArea")
        {
            GameObject evaporator = GameObject.Find("VapourController");

            for (int i = 0; i < evaporator.transform.childCount; i++) {
                evaporator.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
