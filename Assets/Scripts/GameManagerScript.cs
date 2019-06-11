﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [Header("Level 1 Vars")]
    private int amountOfWaterInPool;
    private int amountOfEvaporatedWater;

    private void Start()
    {
        amountOfWaterInPool = 0;
        amountOfEvaporatedWater = 0;

    }

    private void Update()
    {

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
