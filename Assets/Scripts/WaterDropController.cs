using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropController : MonoBehaviour
{
    public GameObject waterPrefab;

    private bool spawnWater;
    private int spawnAmount;

    private void Start()
    {
        spawnWater = false;
        spawnAmount = 5;
    }

    private void Update()
    {
        while (spawnWater && spawnAmount > 0) {
            Instantiate(waterPrefab, gameObject.transform.position, Quaternion.identity);
            spawnAmount -= 1;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "SpawnWaterArea") {
            spawnWater = true;
        }
    }
}
