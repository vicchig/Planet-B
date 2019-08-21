using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManagerEvaporation : GameManager, ILevelManagerWater
{
    ///<summary> Amount of water needed in the pool to begin evaporation. </summary>
    public int waterNeededInPool;
    public GameObject waterDropParent;
    public GameObject movingPlatParent;

    protected RisingSteamManager steamManager;

    /// <summary> Script that controls how the player can pour water.</summary>
    protected WaterPourController waterPControl;

    /// <summary> Current amount of water in the pool, each water droplet is equivalent to 4 of these</summary>
    protected int amountOfWaterInPool;

    /// <summary> Amount of water that has been evaporated from the pool.</summary>
    protected int amountOfEvaporatedWater;


    //water objects
    protected GameObject waterParentL;
    protected GameObject bubbleParent;

    protected GameObject waterParentS;

    protected override void Start()
    {
        base.Start();

        amountOfWaterInPool = 0;
        amountOfEvaporatedWater = 0;

        nextLevelMarker.SetActive(false);

        attributes = player.GetComponent<PlayerAttributes>();
        waterPControl = player.GetComponent<WaterPourController>();

        steamManager = GameObject.Find("RisingSteam").GetComponent<RisingSteamManager>();
        waterParentL = GameObject.Find("WaterPoolL");
        waterParentS = GameObject.Find("WaterPoolS");
        bubbleParent = GameObject.Find("BubbleParent");
    }

    protected override void Update()
    {
        base.Update();

        //fill pool with water
        if (attributes.isInPool() && attributes.GetCurrentWater() >= 1)
        {
            amountOfWaterInPool += attributes.GetCurrentWater() * 3;

            attributes.SetCurrentWater(0);
            AudioManager.playSplash();
        }

        //turn on small and large water pools
        if (amountOfWaterInPool >= waterNeededInPool)
        {
            for (int i = 0; i < waterParentS.transform.childCount; i++)
            {
                waterParentL.transform.GetChild(i).gameObject.SetActive(true);
                waterParentS.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if (amountOfWaterInPool >= 1)
        {
            for (int i = 0; i < waterParentS.transform.childCount; i++)
            {
                waterParentS.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        //turn on bubbles
        if (amountOfEvaporatedWater >= steamManager.waterThreshold)
        {
            for (int i = 0; i < bubbleParent.transform.childCount; i++)
            {
                bubbleParent.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void changeObjectives()
    {
        if (attributes.GetCurrentWater() * 3 >= waterNeededInPool && !(amountOfWaterInPool >= waterNeededInPool))
        {
            objectiveDisplay.text = "Current Objective: Fill the empty lake with the collected water.";
        }
        else if (amountOfEvaporatedWater >= GameObject.Find("RisingSteam").GetComponent<RisingSteamManager>().waterThreshold)
        {
            objectiveDisplay.text = "Current Objective: Go to the marker on the far right side to proceed.";
            dirArrow.GetComponent<DirectionArrowController>().levelCompleted = true;

        }
        else if (amountOfWaterInPool >= waterNeededInPool)
        {
            objectiveDisplay.text = "Current Objective: Evaporate the water in the lake using heat energy.";
        }
    }

    public void SetWaterInPool(int amount)
    {
        amountOfWaterInPool = amount;
    }

    public void SetEvaporatedWater(int amount)
    {
        if (amountOfWaterInPool >= waterNeededInPool && amountOfEvaporatedWater < steamManager.waterThreshold)
        {
            amountOfEvaporatedWater = amount;
        }

    }

    public int GetWaterInPool()
    {
        return amountOfWaterInPool;
    }

    public int GetEvaporatedWater()
    {
        return amountOfEvaporatedWater;
    }

    public int GetWaterNeededInPool()
    {
        return waterNeededInPool;
    }

    public int GetEvaporationNeeded()
    {
        return 0;
    }
}
