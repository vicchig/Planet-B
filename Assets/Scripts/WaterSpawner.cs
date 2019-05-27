using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawner : MonoBehaviour
{

    public GameObject waterPrefab;

    [Header("Water Properties")]
    public Vector3 waterScale;
    public float spawnDelay;
    public int spawnAmount;

    private float finalTime;


    private void Start()
    {
        finalTime = 0;

        waterPrefab.transform.localScale = waterScale;
            
    }
    private void Update()
    {
        if (spawnAmount > 0) {
            if (finalTime >= spawnDelay) {
                Instantiate(waterPrefab, transform.position, Quaternion.identity, transform);
                spawnAmount -= 1;
                finalTime = 0;
            }
            finalTime += Time.deltaTime;
        }
        
    }
}
