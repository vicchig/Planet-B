using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManagerCollection : GameManager
{
    [Header("Level 3 Variables")]
    public int waterNeededInPool1;
    public int waterNeededInPool2;

    //[Header("Level Objects")]

    protected int waterInPool1;
    protected int waterInPool2;
    protected GameObject waterParent;
    protected GameObject waterParent2;
    protected GameObject sunraySpawner;
    protected bool filledPool2;

    protected override void Start()
    {
        base.Start();

        waterInPool1 = 0;
        waterInPool2 = 0;

        waterParent = GameObject.Find("WaterPool");
        waterParent2 = GameObject.Find("WaterPool2");

        sunraySpawner = GameObject.Find("sun2");

        filledPool2 = true;
    }

    protected override void Update()
    {
        base.Update();

        if (waterInPool1 >= waterNeededInPool1 && waterInPool2 >= waterNeededInPool2)
        {
            nextLevelMarker.SetActive(true);
        }

        if (waterInPool1 >= waterNeededInPool1)
        {
            waterInPool1 = waterNeededInPool1;
            AudioManager.playSplash();
            for (int i = 0; i < waterParent.transform.childCount; i++)
            {
                waterParent.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        if (waterInPool2 >= waterNeededInPool2 && filledPool2)
        {
            AudioManager.playSplash();
            for (int i = 0; i < waterParent2.transform.childCount; i++)
            {
                waterParent2.transform.GetChild(i).gameObject.SetActive(true);
            }
            sunraySpawner.GetComponent<HeatSpawner>().targetIce = false;
            sunraySpawner.GetComponent<HeatSpawner>().targetWater = true;
            filledPool2 = false;
        }
    }

    protected override void changeObjectives()
    {
        if (waterInPool1 < waterNeededInPool1)
        {
            objectiveDisplay.text = "Current Objective: Fill the east pool using groundwater from the caves.";
        }
        else if (waterInPool2 < waterNeededInPool2)
        {
            objectiveDisplay.text = "Current Objective: Fill the west pool with liquid water using the ice on top of the mountain.";
        }
        else
        {
            objectiveDisplay.text = "Current Objective: Proceed to the blue marker at the middle to proceed";
        }
    }

    protected override void checkEchoCollisions()
    {
        return;
    }

    protected override void levelEchoMsgChecks()
    {
        return;
    }

    public int getWaterNeededInPool1()
    {
        return waterNeededInPool1;
    }

    public int getWaterNeededInPool2()
    {
        return waterNeededInPool2;
    }

    public int getWaterInPool1()
    {
        return waterInPool1;
    }

    public int getWaterInPool2()
    {
        return waterInPool2;
    }

    public void setWaterInPool1(int waterAmnt)
    {
        waterInPool1 = waterAmnt;
    }

    public void setWaterInPool2(int waterAmnt)
    {
        waterInPool2 = waterAmnt;
    }
}
