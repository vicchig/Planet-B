﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel5 : GameManager, ILevelManagerCondensation, ILevelManagerTranspiration, ILevelManagerWater
{

    [Header("Level 5 Echo Dialogue")]
    public AudioClip objectiveClip5_0;
    public AudioClip objectiveClip5_1;
    public AudioClip objectiveClip5_2;
    public AudioClip objectiveClip5_3;
    public AudioClip objectiveClip5_4;
    public AudioClip objectiveClip5_5;

    [Header("Level 5 Variables")]
    public int condensationNeeded = 5;
    public int evaporationNeeded = 75;
    public int transpirationNeeded = 25;
    public int waterNeededInPool = 5;

    private int totalEvaporationNeeded;

    [Header("Level 5 Objects")]
    public GameObject condensationCloudParent;
    public GameObject waterVapourParent1;
    public GameObject waterVapourParent2;
    public GameObject rainParent;

    private int condensedVapourAmount;
    private int evaporationAmount;
    private int transpirationAmount;
    private int waterInPool;
    private TreePlantingController treePlanter;

    EchoMessage objectiveLevelTxt5_0;
    EchoMessage objectiveLevelTxt5_1;
    EchoMessage objectiveLevelTxt5_2;
    EchoMessage objectiveLevelTxt5_3;
    EchoMessage objectiveLevelTxt5_4;
    EchoMessage objectiveLevelTxt5_5;

    RisingSteamManager steamManager;

    GameObject waterParentSmall;
    GameObject waterParentLarge;
    GameObject bubbleParent;
    HeatSpawner sun;

    protected override void Start()
    {
        base.Start();

        condensedVapourAmount = 0;
        evaporationAmount = 0;
        transpirationAmount = 0;
        waterInPool = 0;

        totalEvaporationNeeded = transpirationNeeded + evaporationNeeded;

        objectiveLevelTxt5_0 = new EchoMessage("Now you have to implement the entire cycle on your own. Let's see what you remember!", objectiveClip5_0, 1);
        objectiveLevelTxt5_1 = new EchoMessage("You have finished the collection part of the water cycle. But what comes next?", objectiveClip5_1, 1);
        objectiveLevelTxt5_2 = new EchoMessage("You have successfully evaporated water from surface bodies of water, but remember that some of the evaporated water has to come from some other source.", objectiveClip5_2, 1);
        objectiveLevelTxt5_3 = new EchoMessage("Now we just need to make sure the water we evaporated comes back to the ground. Time to explore the mountain to the west to find a way up into the atmosphere.", objectiveClip5_3, 1);
        objectiveLevelTxt5_4 = new EchoMessage("You have completed the water cycle! The condensed water vapour falls back onto the ground and the cycle begins anew with collection, followed by evaporation/transpiration and finishing with condensation.", objectiveClip5_4, 1);
        objectiveLevelTxt5_5 = new EchoMessage("You have successfully initiated the process of transpiration! But the majority of the evaporated water has to come from some other source.", objectiveClip5_5, 1);

        echo.addMessage(objectiveLevelTxt5_0);

        for (int i = 0; i < rainParent.transform.childCount; i++)
        {
            rainParent.transform.GetChild(i).GetComponent<ParticleSystem>().Pause();
        }


        steamManager = GameObject.Find("RisingSteam").GetComponent<RisingSteamManager>();

        waterParentSmall = GameObject.Find("WaterPoolS");
        waterParentLarge = GameObject.Find("WaterPoolL");
        bubbleParent = GameObject.Find("BubbleParent");
        sun = GameObject.Find("sun2").GetComponent<HeatSpawner>();
    }

    protected override void Update()
    {
        base.Update();
        if (condensedVapourAmount >= condensationNeeded)
        {
            nextLevelMarker.SetActive(true);
            dirArrow.GetComponent<DirectionArrowController>().levelCompleted = true;
            for (int i = 0; i < rainParent.transform.childCount; i++)
            {
                rainParent.transform.GetChild(i).GetComponent<ParticleSystem>().Play();

            }
        }


        if (evaporationAmount >= evaporationNeeded) {
            steamManager.EnableSteam();
            for (int i = 0; i < bubbleParent.transform.childCount; i++) {
                bubbleParent.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        //change camera perspective when they are going t the clouds
        if (player.GetComponent<Rigidbody2D>().position.y > 12) {
            Camera.main.orthographic = true;
        }
        else {
            Camera.main.orthographic = false;
        }

        //turn on large and small water pools

        if (waterInPool >= waterNeededInPool)
        {
            for (int i = 0; i < waterParentLarge.transform.childCount; i++)
            {
                waterParentSmall.transform.GetChild(i).gameObject.SetActive(false);
                waterParentLarge.transform.GetChild(i).gameObject.SetActive(true);
            }
            sun.targetIce = false;
            sun.targetWater = true;
            
        }
        else if (waterInPool >= 1)
        {
            for (int i = 0; i < waterParentSmall.transform.childCount; i++)
            {
                waterParentSmall.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void levelEchoMsgChecks()
    {
        if (waterInPool >= waterNeededInPool && !echo.containsMessage(objectiveLevelTxt5_1) && !objectiveLevelTxt5_1.maxTextShowsReached())
        {
            echo.addMessage(objectiveLevelTxt5_1);
        }

        //finished transpiration before pool evaporation
        if (transpirationAmount >= transpirationNeeded && evaporationAmount < evaporationNeeded && !echo.containsMessage(objectiveLevelTxt5_5) && !objectiveLevelTxt5_5.maxTextShowsReached())
        {
            echo.addMessage(objectiveLevelTxt5_5);
        }

        //finished evaporation from pool before transpiration
        if (evaporationAmount >= evaporationNeeded && transpirationAmount < transpirationNeeded && !echo.containsMessage(objectiveLevelTxt5_2) && !objectiveLevelTxt5_2.maxTextShowsReached())
        {
            echo.addMessage(objectiveLevelTxt5_2);
        }

        //finished evaporation
        if ((evaporationAmount + transpirationAmount) >= totalEvaporationNeeded && !echo.containsMessage(objectiveLevelTxt5_3) && !objectiveLevelTxt5_3.maxTextShowsReached())
        {
            echo.addMessage(objectiveLevelTxt5_3);
        }

        //finished condensation
        if (condensedVapourAmount >= condensationNeeded && !echo.containsMessage(objectiveLevelTxt5_4) && !objectiveLevelTxt5_4.maxTextShowsReached())
        {
            echo.addMessage(objectiveLevelTxt5_4);
            AudioManager.playRain();
        }


        //putting water in pool using collected water
        if (attributes.isInPool() && attributes.GetCurrentWater() >= 1) {
            waterInPool += attributes.GetCurrentWater() * 4;
            attributes.SetCurrentWater(0);
            AudioManager.playSplash();
        }



    }

    protected override void checkEchoCollisions()
    {
       // throw new System.NotImplementedException();
    }

    protected override void changeObjectives()
    {
        objectiveDisplay.text = "Complete the 4 stages of the water cycle covered to create a fully functioning water cycle";
    }

    public void SetCondensedVapour(int amount)
    {
        condensedVapourAmount = amount;
    }

    public int GetCondensedVapour()
    {
        return condensedVapourAmount;
    }

    public void SetTranspirationAmnt(int amount)
    {
        transpirationAmount = amount;
        if (transpirationAmount >= transpirationNeeded)
        {
            transpirationAmount = transpirationNeeded;
        }
        CheckEnableVapour();
    }

    public int GetTranspirationAmountNeeded()
    {
        return transpirationNeeded;
    }

    public int GetTranspirationAmount()
    {
        return transpirationAmount;
    }

    public void SetWaterInPool(int amount)
    {
        waterInPool = amount;
    }

    public void SetEvaporatedWater(int amount)
    {
        if (waterInPool >= waterNeededInPool) {
            evaporationAmount = amount - transpirationAmount;//get evaporation returns evaporation + transpiration, but evaporation amount is just the evaporation amount from the water pool, so had to subtract that here, probably need to change this to makeit easier to use
        }
        if (evaporationAmount > evaporationNeeded)
        {
            evaporationAmount = evaporationNeeded;
        }
        CheckEnableVapour();
    }

    public int GetWaterInPool()
    {
        return waterInPool;
    }

    public int GetEvaporatedWater()
    {
        return evaporationAmount + transpirationAmount;
    }

    public int GetWaterNeededInPool()
    {
        return waterNeededInPool;
    }

    public int GetCondensedVapourNeeded()
    {
        return condensationNeeded;
    }

    public int GetEvaporationNeeded()
    {
        return evaporationNeeded + transpirationNeeded;
    }

    public void ChangeCloudColour()
    {
        for (int i = 0; i < condensationCloudParent.transform.childCount; i++)
        {
            condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.r * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.g * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.b * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.a);
        }
    }
    private void CheckEnableVapour()
    {
        if (evaporationAmount >= evaporationNeeded)
        {
            waterVapourParent1.SetActive(true);
        }
        if (transpirationAmount >= transpirationNeeded)
        {
            waterVapourParent2.SetActive(true);
        }
    }
}
