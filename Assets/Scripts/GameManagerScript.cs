﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [Header("Level 1 Vars")]
    public GameObject nextLevelMarker;


    private int amountOfWaterInPool;
    private int amountOfEvaporatedWater;

    private void Start()
    {
        amountOfWaterInPool = 0;
        amountOfEvaporatedWater = 0;
        nextLevelMarker.SetActive(false);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainGameScene" && amountOfEvaporatedWater >= GameObject.Find("RisingSteam").GetComponent<RisingSteamManager>().waterThreshold) {
            nextLevelMarker.SetActive(true);
        }
    }

    public int getAmountOfWaterInPool()
    {
        return amountOfWaterInPool;

    }
    public void setAmountOfWaterInPool(int amount)
    {
        amountOfWaterInPool = amount;
    }


    public int getEvaporated()
    {
        return amountOfEvaporatedWater;

    }
    public void setEvaporated(int amount)
    {
        amountOfEvaporatedWater = amount;
    }
}
