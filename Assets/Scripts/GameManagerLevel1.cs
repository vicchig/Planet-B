using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> MonoBehaviour for controlling level 1 and tracking player progress in level 1. </summary>
public class GameManagerLevel1 : GameManagerEvaporation
{
    [Header("Level 1 Echo Dialogue")]
    public AudioClip objectiveLevelClip1_0;
    public AudioClip objectiveLevelClip1_1;
    public AudioClip objectiveLevelClip1_2;
    public AudioClip objectiveLevelClip1_3;
    public AudioClip objectiveLevelClip1_5;
    public AudioClip objectiveLevelClip1_6;
    public AudioClip objectiveLevelClip1_7;

    //Level 1 Echo Messages
    EchoMessage objectiveLevelTxt1_0;
    EchoMessage objectiveLevelTxt1_1;
    EchoMessage objectiveLevelTxt1_2;
    EchoMessage objectiveLevelTxt1_3;
    EchoMessage objectiveLevelTxt1_5;
    EchoMessage objectiveLevelTxt1_6;
    EchoMessage objectiveLevelTxt1_7;


    protected override void Start()
    {
        base.Start();

        //LEVEL OBJECTIVES AUDIO 
        objectiveLevelTxt1_0 = new EchoMessage("If we want to colonize this planet, we will need to fix its water cycle first. The first step is to evaporate water from surface bodies of water into the atmosphere.", objectiveLevelClip1_0, 1);
        objectiveLevelTxt1_1 = new EchoMessage("This empty lake would be a perfect source of evaporation if it had water in it. We can use groundwater to fill it, explore the cave system below us. The process of groundwater seeping into lakes and rivers is known as collection. We will talk about it more later.", objectiveLevelClip1_1, 1);
        objectiveLevelTxt1_2 = new EchoMessage("By my calculations we will need about 14 of these to have enough water to evaporate. You can see the current amount on your HUD or look at the bar that appears near you when you collect water.", objectiveLevelClip1_2, 1);
        objectiveLevelTxt1_3 = new EchoMessage("We should have enough water now. Head back to the surface and fill the lake with it.", objectiveLevelClip1_3, 1);
        objectiveLevelTxt1_5 = new EchoMessage("Now we just need to evaporate the water. This can be done using heat energy. We will need to find a source.", objectiveLevelClip1_5, 1);
        objectiveLevelTxt1_6 = new EchoMessage("Use your force shield to reflect the sunrays to heat the water.", objectiveLevelClip1_6, 1);
        objectiveLevelTxt1_7 = new EchoMessage("Congratulations! We have fixed the first stage of the water cycle. As water is heated by the sun, it evaporates in small amounts and rises in the atmosphere.", objectiveLevelClip1_7, 1);


        echo.addMessage(objectiveLevelTxt1_0);
        echo.addMessage(objectiveLevelTxt1_1);
    }

    protected override void Update()
    {
        base.Update();
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /**
        <summary> Checks whether specific checkpoints in the level have been reached and performs the appropriate action. </summary> 
    */
    protected override void levelEchoMsgChecks() {
        //check for whether the pool has enough water to begin evaporation
        if (amountOfWaterInPool >= waterNeededInPool && !objectiveLevelTxt1_5.maxTextShowsReached() && !objectiveLevelTxt1_6.maxTextShowsReached() && !echo.containsMessage(objectiveLevelTxt1_6) && !echo.containsMessage(objectiveLevelTxt1_5))
        {
            echo.addMessage(objectiveLevelTxt1_5);
            echo.addMessage(objectiveLevelTxt1_6);
        }

        //evaporated enough water
        if (amountOfWaterInPool>= waterNeededInPool && amountOfEvaporatedWater >= GameObject.Find("RisingSteam").GetComponent<RisingSteamManager>().waterThreshold && !objectiveLevelTxt1_7.maxTextShowsReached() && !echo.containsMessage(objectiveLevelTxt1_7))
        {
            steamManager.EnableSteam();
            echo.addMessage(objectiveLevelTxt1_7);
            nextLevelMarker.SetActive(true);
        }

        //first water picked up
        if (attributes.GetCurrentWater() > 0 && !objectiveLevelTxt1_2.maxTextShowsReached() && !echo.isBusy() && attributes.GetCurrentWater() <= 1) {
            echo.addMessage(objectiveLevelTxt1_2);
        }   
    }

    /**
        Check whether Echo is colliding with any of the special collision areas and play the appropriate message. 
    */
    protected override void checkEchoCollisions() {
        
    }

    protected override void changeObjectives()
    {
        base.changeObjectives();

        if (attributes.GetCurrentWater() * 3 < waterNeededInPool && amountOfWaterInPool <= 0)
        {
            objectiveDisplay.text = "Current Objective: Collect 14 groundwater droplets.";
        }
    }
}
