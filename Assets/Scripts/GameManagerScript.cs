using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    


    //Level 1 Vars
    private int amountOfWaterInPool;


    private void Start()
    {
        amountOfWaterInPool = 0;
    }


    public int getAmountOfWaterInPool()
    {
        return amountOfWaterInPool;

    }


    public void setAmountOfWaterInPool(int amount)
    {
        amountOfWaterInPool = amount;
    }
}
