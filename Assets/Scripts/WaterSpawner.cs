﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawner : MonoBehaviour
{

    public GameObject waterPrefab;

    [Header("Spawn Properties")]
    public float spawnDelay;
    public int spawnAmount;

    private float finalTime;
    private List<GameObject> waterBalls = new List<GameObject>();

    private void Start()
    {
        finalTime = 0;            
    }

    private void Update()
    {
        GameObject tempWaterObject;
        if (spawnAmount > 0) {
            if (finalTime >= spawnDelay) {
                tempWaterObject = Instantiate(waterPrefab, transform.position, Quaternion.identity);
                tempWaterObject.GetComponent<Rigidbody2D>().velocity = new Vector2(20, 0); 
                waterBalls.Add(tempWaterObject);
                spawnAmount -= 1;
                finalTime = 0;
            }
            finalTime += Time.deltaTime;
            tempWaterObject = null;
        }
        
    }
}
