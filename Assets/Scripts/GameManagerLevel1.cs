using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> MonoBehaviour for controlling level 1 and tracking player progress in level 1. </summary>
public class GameManagerLevel1 : GameManager, ILevelManagerWater
{
    [Header("Level 1 Echo Dialogue")]
    public AudioClip objectiveLevelClip1_0;
    public AudioClip objectiveLevelClip1_1;
    public AudioClip objectiveLevelClip1_2;
    public AudioClip objectiveLevelClip1_3;
    public AudioClip objectiveLevelClip1_5;
    public AudioClip objectiveLevelClip1_6;
    public AudioClip objectiveLevelClip1_7;

    [Header("Level 1 Variables")]
    ///<summary> Amount of water needed in the pool to begin evaporation. </summary>
    public int waterNeededInPool;

    [Header("Level Objects")]
    public GameObject waterDropParent;
    public GameObject movingPlatParent;

    //Level 1 Echo Messages
    EchoMessage objectiveLevelTxt1_0;
    EchoMessage objectiveLevelTxt1_1;
    EchoMessage objectiveLevelTxt1_2;
    EchoMessage objectiveLevelTxt1_3;
    EchoMessage objectiveLevelTxt1_5;
    EchoMessage objectiveLevelTxt1_6;
    EchoMessage objectiveLevelTxt1_7;

    RisingSteamManager steamManager;

    /// <summary> Script that controls how the player can pour water.</summary>
    private WaterPourController waterPControl;

    /// <summary> Current amount of water in the pool, each water droplet is equivalent to 4 of these</summary>
    private int amountOfWaterInPool;

    /// <summary> Amount of water that has been evaporated from the pool.</summary>
    private int amountOfEvaporatedWater;

    /// <summary> Tracks whether the player is standing inside the pool area collider or not. Uses the Echo collider.</summary>
    private bool playerIsInPool;

    private bool lastCPEnabled;

    new protected void Start()
    {
        base.Start();

        //LEVEL OBJECTIVES AUDIO 
        objectiveLevelTxt1_0 = new EchoMessage("If we want to colonize this planet, we will need to fix its water cycle first. The first step is to evaporate water from surface bodies of water into the atmosphere.", objectiveLevelClip1_0, 1);
        objectiveLevelTxt1_1 = new EchoMessage("This empty lake would be a perfect source of evaporation if it had water in it. We can use groundwater to fill it, explore the cave system below us. The process of groundwater seeping into lakes and rivers is known as collection. We will talk about it more later.", objectiveLevelClip1_1, 1);
        objectiveLevelTxt1_2 = new EchoMessage("By my calculations we will need about 14 of these to have enough water to evaporate. You can see the current amount on your HUD or look at the bar that appears near you when you collect water.", objectiveLevelClip1_2, 1);
        objectiveLevelTxt1_3 = new EchoMessage("We should have enough water now. Head back to the surface and fill the lake with it.", objectiveLevelClip1_3, 1);
        objectiveLevelTxt1_5 = new EchoMessage("Now we just need to evaporate the water. This can be done using heat energy. We will need to find a source.", objectiveLevelClip1_5, 1);
        objectiveLevelTxt1_6 = new EchoMessage("Use your force shield to reflect the sunrays to heat the water.", objectiveLevelClip1_6, 1);
        objectiveLevelTxt1_7 = new EchoMessage("Congratulations! We have fixed the first stage of the water cycle. As water is heated by the sun, it evaporates in small amounts and rises in the atmosphere, which is where we are going next.", objectiveLevelClip1_7, 1);


        amountOfWaterInPool = 0;
        amountOfEvaporatedWater = 0;

        nextLevelMarker.SetActive(false);

        attributes = player.GetComponent<PlayerAttributes>();
        waterPControl = player.GetComponent<WaterPourController>();

        echo.addMessage(objectiveLevelTxt1_0);
        echo.addMessage(objectiveLevelTxt1_1);

        lastCPEnabled = false;

        steamManager = GameObject.Find("RisingSteam").GetComponent<RisingSteamManager>();
        
    }

    new protected void Update()
    {
        base.Update();

        //enabled last cp on surface (index 7)
        if (attributes.GetCurrentWater() >= 14 && lastCPEnabled == false) {
            checkpointTracker.checkpoints[checkpointTracker.checkpoints.Length - 1].GetComponent<CheckpointController>().active = true;
            lastCPEnabled = true;
        }
    }
    
    new protected void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /**
        <summary> Checks whether specific checkpoints in the level have been reached and performs the appropriate action. </summary> 
    */
    protected override void levelEchoMsgChecks() {
        //check for whether the player has collected enough water
        if (attributes.GetCurrentWater() >= (waterNeededInPool / 4) + 4 && !echo.isBusy() && !objectiveLevelTxt1_3.maxTextShowsReached())
        {
            echo.addMessage(objectiveLevelTxt1_3);
        }

        //check for whether the pool has enough water to begin evaporation
        if (amountOfWaterInPool >= waterNeededInPool && !objectiveLevelTxt1_5.maxTextShowsReached() && !objectiveLevelTxt1_6.maxTextShowsReached())
        {
            echo.addMessage(objectiveLevelTxt1_5);
            echo.addMessage(objectiveLevelTxt1_6);

        }




        //evaporated enough water
        if (amountOfWaterInPool>= waterNeededInPool && amountOfEvaporatedWater >= GameObject.Find("RisingSteam").GetComponent<RisingSteamManager>().waterThreshold && !objectiveLevelTxt1_7.maxTextShowsReached())
        {
            steamManager.EnableSteam();
            echo.addMessage(objectiveLevelTxt1_7);
            nextLevelMarker.SetActive(true);
        }

        //first water picked up
        if (attributes.GetCurrentWater() > 0 && !objectiveLevelTxt1_2.maxTextShowsReached() && !echo.isBusy()) {
            echo.addMessage(objectiveLevelTxt1_2);
        }   
    }

    /**
        Check whether Echo is colliding with any of the special collision areas and play the appropriate message. 
    */
    protected override void checkEchoCollisions() {
        for (int i = 0; i < echoColliders.Count; i++) {
            if (echoColliders[i] != null) {
                if (echoColliders[i].tag == "WaterPoolCollisionArea")
                {
                    playerIsInPool = true;
                    
                    echoColliders[i].enabled = false;
                }
            }
        }
    }

    protected override void changeObjectives()
    {
        if (attributes.GetCurrentWater() * 4 - 16 < waterNeededInPool && amountOfWaterInPool <= 0)
        {
            objectiveDisplay.text = "Current Objective: Collect 14 groundwater droplets.";
        }
        else if (attributes.GetCurrentWater() * 4 >= waterNeededInPool && !(amountOfWaterInPool >= waterNeededInPool))
        {
            objectiveDisplay.text = "Current Objective: Find an area on the surface to create an artificial lake in and fill it with the collected water. Press F to release water into the lake.";
        }
        else if (amountOfEvaporatedWater >= GameObject.Find("RisingSteam").GetComponent<RisingSteamManager>().waterThreshold) {
            objectiveDisplay.text = "Current Objective: Go to the marker on the far right side to proceed.";
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
        if (amountOfWaterInPool >= waterNeededInPool) {
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

    /*
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
    }*/
}
