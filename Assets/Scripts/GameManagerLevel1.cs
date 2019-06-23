using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> MonoBehaviour for controlling level 1 and tracking player progress in level 1. </summary>
public class GameManagerLevel1 : GameManager
{
    [Header("Level 1 Echo Dialogue")]
    public AudioClip objectiveLevelClip1_0;
    public AudioClip objectiveLevelClip1_1;
    public AudioClip objectiveLevelClip1_2;
    public AudioClip objectiveLevelClip1_3;
    public AudioClip objectiveLevelClip1_4;
    public AudioClip objectiveLevelClip1_5;
    public AudioClip objectiveLevelClip1_6;
    public AudioClip objectiveLevelClip1_7;
    public AudioClip objectiveLevelClip1_8;
    public AudioClip objectiveLevelClip1_9;
    public AudioClip dontWasteWaterReminderClip;
    public AudioClip waterPoolFoundClip;

    [Header("Level 1 Variables")]
    ///<summary> Amount of water needed in the pool to begin evaporation. </summary>
    public int waterNeededInPool;

    [Header("Level Objects")]
    public GameObject nextLevelMarker;
    public GameObject waterDropParent;
    public GameObject movingPlatParent;

    //Level 1 Echo Messages
    EchoMessage objectiveLevelTxt1_0;
    EchoMessage objectiveLevelTxt1_1;
    EchoMessage objectiveLevelTxt1_2;
    EchoMessage objectiveLevelTxt1_3;
    EchoMessage objectiveLevelTxt1_4;
    EchoMessage objectiveLevelTxt1_5;
    EchoMessage objectiveLevelTxt1_6;
    EchoMessage objectiveLevelTxt1_7;
    EchoMessage objectiveLevelTxt1_8;
    EchoMessage objectiveLevelTxt1_9;
    EchoMessage dontWasteWaterReminderTxt;
    EchoMessage waterPoolFoundTxt;

    /// <summary> Script that controls how the player can pour water.</summary>
    private WaterPourController waterPControl;

    /// <summary> Current amount of water in the pool, each water droplet is equivalent to 4 of these</summary>
    private int amountOfWaterInPool;

    /// <summary> Amount of water that has been evaporated from the pool.</summary>
    private int amountOfEvaporatedWater;

    /// <summary> Tracks whether the player is standing inside the pool area collider or not. Uses the Echo collider.</summary>
    private bool playerIsInPool;

    new protected void Start()
    {
        base.Start();

        //LEVEL OBJECTIVES AUDIO 
        objectiveLevelTxt1_0 = new EchoMessage("If we want to colonize this planet, we will need to fix its water cycle first. The first step is to evaporate water from surface bodies of water into the atmosphere.", objectiveLevelClip1_0, 1);
        objectiveLevelTxt1_1 = new EchoMessage("There is no water on the surface, I suggest you check the caves below us for some groundwater that we can collect and use.", objectiveLevelClip1_1, 1);
        objectiveLevelTxt1_2 = new EchoMessage("By my calculations we will need about 14 of these to have enough water to evaporate. You can see the current amount on your HUD.", objectiveLevelClip1_2, 1);
        objectiveLevelTxt1_3 = new EchoMessage("We should have enough water now. Head back to the surface and find a place for an artificial lake.", objectiveLevelClip1_3, 1);
        objectiveLevelTxt1_4 = new EchoMessage("We should probably explore some more to the east.", objectiveLevelClip1_4, 1);
        objectiveLevelTxt1_5 = new EchoMessage("Now we just need to evaporate the water. This can be done using heat energy. We will need to find a source.", objectiveLevelClip1_5, 1);
        objectiveLevelTxt1_6 = new EchoMessage("Use the 2 key to amplify the heat energy collected and use it to heat the water.", objectiveLevelClip1_6, 1);
        objectiveLevelTxt1_7 = new EchoMessage("Congratulations! We have fixed the first stage of the water cycle. As water is heated by the sun, it evaporates in small amounts and rises in the atmosphere, which is where we are going next.", objectiveLevelClip1_7, 1);
        objectiveLevelTxt1_8 = new EchoMessage("There is not enough water in the pool yet. We need some more.", objectiveLevelClip1_8, 1);
        objectiveLevelTxt1_9 = new EchoMessage("There is not enough water left in the caves for us to fill the pool. We should probably restart.", objectiveLevelClip1_9, 1);

        dontWasteWaterReminderTxt = new EchoMessage("Make sure you do not waste it. If you do, check the cave for some more water. If you waste all of it, we will have to restart.", dontWasteWaterReminderClip, 1);
        waterPoolFoundTxt = new EchoMessage("This looks like a good spot to release our water.", waterPoolFoundClip, 1);

        amountOfWaterInPool = 0;
        amountOfEvaporatedWater = 0;

        nextLevelMarker.SetActive(false);

        attributes = player.GetComponent<PlayerAttributes>();
        waterPControl = player.GetComponent<WaterPourController>();

        echo.addMessage(objectiveLevelTxt1_0);
        echo.addMessage(objectiveLevelTxt1_1);
    }

    new protected void Update()
    {
        base.Update();

        levelObjectiveChecks();
        checkEchoCollisions();
    }
    
    new protected void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /**
        <summary> Checks whether specific checkpoints in the level have been reached and performs the appropriate action. </summary> 
    */
    protected override void levelObjectiveChecks() {

        Debug.Log((attributes.GetCurrentWater() + waterDropParent.transform.childCount) * 4  + " / " + (waterNeededInPool - amountOfWaterInPool));
        //check for whether the player has collected enough water
        if (attributes.GetCurrentWater() >= (waterNeededInPool / 4) + 4 && !echo.isBusy() && !objectiveLevelTxt1_3.maxTextShowsReached() && !dontWasteWaterReminderTxt.maxTextShowsReached())
        {
            echo.addMessage(objectiveLevelTxt1_3);
            echo.addMessage(dontWasteWaterReminderTxt);
        }

        //check for whether the pool has enough water to begin evaporation
        if (amountOfWaterInPool >= waterNeededInPool && !objectiveLevelTxt1_5.maxTextShowsReached() && !objectiveLevelTxt1_6.maxTextShowsReached())
        {
            echo.addMessage(objectiveLevelTxt1_5);
            echo.addMessage(objectiveLevelTxt1_6);

        }
        //player did not fill the pool with enough water
        if (playerIsInPool && amountOfWaterInPool < waterNeededInPool && attributes.GetCurrentWater() == 0 && waterPControl.getFPressed() && !objectiveLevelTxt1_8.maxTextShowsReached())
        {
            echo.addMessage(objectiveLevelTxt1_8);
        }

        //not enough water left on level
        if ((attributes.GetCurrentWater() + waterDropParent.transform.childCount) * 4 < (waterNeededInPool - amountOfWaterInPool) && !objectiveLevelTxt1_9.maxTextShowsReached())
        {
            echo.addMessage(objectiveLevelTxt1_9);
        }

        //evaporated enough water
        if (amountOfEvaporatedWater >= GameObject.Find("RisingSteam").GetComponent<RisingSteamManager>().waterThreshold && !objectiveLevelTxt1_7.maxTextShowsReached())
        {
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
                if (echoColliders[i].tag == "ExploreAreaEastLevel1" && attributes.GetCurrentWater() >= waterNeededInPool / 4 && !waterPoolFoundTxt.maxTextShowsReached())
                {
                    echo.addMessage(objectiveLevelTxt1_4);
                    echoColliders[i].enabled = false;
                }
                else if (echoColliders[i].tag == "WaterPoolCollisionArea")
                {
                    playerIsInPool = true;
                    if (!waterPoolFoundTxt.maxTextShowsReached() && attributes.GetCurrentWater() >= waterNeededInPool / 4)
                    {
                        echo.addMessage(waterPoolFoundTxt);
                    }
                    echoColliders[i].enabled = false;
                }
            }
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
