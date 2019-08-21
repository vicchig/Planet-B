using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Properties")]
    public GameObject objectPrefab;
    public bool spawnContinuously;//false if you want to spawn a given amount of objects once and not spawn any more of them
    public float spawnDelay;//these two should be 0 for an object that is being spawned continuously (without any delay)
    public int spawnAmount;
    public int spawnChance;//integer percentage
    public bool attachToParent;
    public GameObject parent;

    private float timeUntilNextSpawn;

    private void Start()
    {
        if (spawnContinuously) {
            spawnAmount = 0;
        }
        timeUntilNextSpawn = 0.0f;
    }

    private void Update()
    {
        GameObject spawnedObject = null;
        if (spawnContinuously)
        {
            if (timeUntilNextSpawn >= spawnDelay) {
                int randNum = Random.Range(1, 100);
                if (randNum <= spawnChance) {
                    spawnedObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);
                }
                timeUntilNextSpawn = 0;
            }
        }
        else {
            if (spawnAmount > 0)
            {
                if (timeUntilNextSpawn >= spawnDelay)
                {
                    spawnedObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);
                    spawnAmount -= 1;
                    timeUntilNextSpawn = 0;
                }
            }
        }

        if (spawnedObject != null) {
            if (attachToParent)
            {
                spawnedObject.transform.SetParent(parent.transform);
            }
            else {
                spawnedObject.transform.SetParent(this.transform);
            }
            
        }
        

        timeUntilNextSpawn += Time.deltaTime;
    }


}
