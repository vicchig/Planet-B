using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManagerCollection : GameManager, ILevelManagerCollection
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
    protected GameObject waterParentSmall;
    protected bool filledPool2;

    protected override void Start()
    {
        base.Start();

        waterInPool1 = 0;
        waterInPool2 = 0;

        waterParent = GameObject.Find("WaterPool");
        waterParent2 = GameObject.Find("WaterPool2");
        waterParentSmall = GameObject.Find("WaterPool2S");

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
            for (int i = 0; i < waterParent.transform.childCount; i++)
            {
                waterParent.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        

        if (waterInPool2 >= waterNeededInPool2 && filledPool2)
        {
            for (int i = 0; i < waterParent2.transform.childCount; i++)
            {
                waterParent2.transform.GetChild(i).gameObject.SetActive(true);
                waterParentSmall.transform.GetChild(i).gameObject.SetActive(false);

            }

            filledPool2 = false;
        }
        
        else if (waterInPool2 > 0 && filledPool2)
        {
            for (int i = 0; i < waterParentSmall.transform.childCount; i++)
            {
                waterParentSmall.transform.GetChild(i).gameObject.SetActive(true);
            }
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

    public void setWaterInPool1(int amount)
    {
        waterInPool1 = amount;
    }

    public void setWaterInPool2(int amount)
    {
        waterInPool2 = amount;
    }

    public int getWaterInPool1()
    {
        return waterInPool1;
    }

    public int getWaterInPool2()
    {
        return waterInPool2;
    }

    public int getWaterNeededInPool1()
    {
        return waterNeededInPool1;
    }

    public int getWaterNeededInPool2()
    {
        return waterNeededInPool2;
    }
}
