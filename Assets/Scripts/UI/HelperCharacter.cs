﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelperCharacter : MonoBehaviour
{
    [Header("Game Utilities")]
    public GameObject managerObj;
    public GameObject player;
    public GameObject waterDropParent;
    
    [Header("UI Objects")]
    public GameObject txtMeshContainer;
    public GameObject txtBackground;
    public GameObject portraitObj;

    [Header("Text Display Attributes")]
    public float textDuration = 4.0f; //for how long the text is displayed
    public int level = 1;

    [Header("Sounds Tutorial")]
    public AudioClip introClip;
    public AudioClip introClip2;
    public AudioClip hudExplanationClip;
    public AudioClip destructibleExplanation;
    public AudioClip wallClimbingExplanationClip;
    public AudioClip shootingExplanation;
    public AudioClip healthRegenExplanation;
    public AudioClip endTutorialClip;

    [Header("Sounds Level 1")]
    public AudioClip objectiveLevelClip1_2;
    public AudioClip objectiveLevelClip1_4;
    public AudioClip waterFound;
    public AudioClip waterPoolFoundClip;

    [Header("Sounds General")]
    public AudioClip airFound;
    public AudioClip aboutToDieClip;
    public AudioClip lavaCommentClip;
    public AudioClip airCriticalClip;

    [Header("Audio Play Conditions")]
    public int critHealthThreshold; //the percentage of health under which the critical damage sound will be played
    public float healthWarningDelay; //amount of time between health warning delays
    public int critAirThreshold; //percentage of air under which the air warning is played
    public float airWarningDelay; //amount of time between air warning delays

    //text mesh where the text will be displayed
    private TextMeshProUGUI uiTextMesh;

    //how long the current text has been displayed for so far
    private float textTimer;

    //TUTORIAL 
    EchoMessage introTxt;
    EchoMessage introTxt2;
    EchoMessage hudExplanationTxt;
    EchoMessage destructibleExplanationTxt;
    EchoMessage wallClimbingExplanationTxt;
    EchoMessage shootingExplanationTxt;
    EchoMessage healthRegenTxt;
    EchoMessage endTutorialTxt;

    //LEVEL 1
    EchoMessage objectiveLevelTxt1_4;
    EchoMessage waterPoolFoundTxt;
    EchoMessage objectiveLevelTxt1_2;



    //GENERAL
    EchoMessage aboutToDieTxt;
    EchoMessage lavaCommentTxt;
    EchoMessage airCritical;
    EchoMessage airSourceTxt;
    EchoMessage waterDropTxt;

    private PlayerAttributes attributes;

    //whether the display is already dispalying any text
    private bool busy;

    private Queue<EchoMessage> sounds;
    private float healthWarningTimer;
    private bool healthWarningTimerEnable;
    private float airWarningTimer;
    private bool airWarningTimerEnable;
    private bool inPoolArea;
    private GameManagerScript manager;
    private WaterPourController playerWaterPourController;
    private bool startedTutorial;

    private void Awake()
    {
        sounds = new Queue<EchoMessage>();
    }

    private void Start()
    {
        uiTextMesh = txtMeshContainer.GetComponent<TextMeshProUGUI>();
        textTimer = 0f;
        txtBackground.GetComponent<Image>().enabled = false;
        portraitObj.GetComponent<Image>().enabled = false;
        busy = false;
        healthWarningTimerEnable = false;
        inPoolArea = false;
        healthWarningTimer = 0;
        airWarningTimer = 0;
        airWarningTimerEnable = false;
        manager = managerObj.GetComponent<GameManagerScript>();
        attributes = player.GetComponent<PlayerAttributes>();
        playerWaterPourController = player.GetComponent<WaterPourController>();

       
        //TUTORIAL
        introTxt = new EchoMessage("Hello, I am Echo, your suit's built-in A.I. I hope we can get along, if not, please remember that I control your suit's life support systems.", introClip, 1);
        introTxt2 = new EchoMessage("Your mission is to terraform a planet's water cycle so that it may become inhabitable. Apparently, your species is dying out and needs another planet to live on. This suit and I were built for the purpose of helping you achieve this goal. We will now begin your training.", introClip2, 1);
        hudExplanationTxt = new EchoMessage("Your suit has a built-in display at the top left of your screen. It tells you useful information about your resources and life support system state.", hudExplanationClip, 1); ;
        destructibleExplanationTxt = new EchoMessage("This material has weak physical properties and can be destroyed with your weapon.", destructibleExplanation, 1); ;
        wallClimbingExplanationTxt = new EchoMessage("To wall climb, hold space during your jump and move close to a wall. Once on a wall, you can jump higher using W and let go of the wall using A or D.", wallClimbingExplanationClip, 1); ;
        shootingExplanationTxt = new EchoMessage("Shoot using the left mouse button while using the mouse to aim. You can use the 1, 2 and 3 keys to change weapon modes. Weapon 1 is your regular bullets. Weapon modes 2 and 3 use heat energy to respectively heat and cool down your environment.", shootingExplanation, 1); ;
        healthRegenTxt = new EchoMessage("The planet we are going to will have many environmental hazards. If your health reaches critical levels, the suit will passively regenerate your health.", healthRegenExplanation, 1);
        endTutorialTxt = new EchoMessage("Let's see what you have learned. Make your way to the marker on the other side of this lava pool to complete the level.", endTutorialClip, 1);

        //LEVEL 1
        objectiveLevelTxt1_4 = new EchoMessage("We should probably explore some more to the east.", objectiveLevelClip1_4, 1);
        waterPoolFoundTxt = new EchoMessage("This looks like a good spot to release our water.", waterPoolFoundClip, 1);
        objectiveLevelTxt1_2 = new EchoMessage("By my calculations we will need about 14 of these to have enough water to evaporate. You can see the current amount on your HUD.", objectiveLevelClip1_2, 1);

        //GENERAL
        aboutToDieTxt = new EchoMessage("Warning: operator sustaining critical damage.", aboutToDieClip, 1000);
        airCritical = new EchoMessage("Warning: Air supply at critical level.", airCriticalClip, 1000);
        lavaCommentTxt = new EchoMessage("It burns! It burns! Make it stop! Just kidding, I cannot feel a thing.", lavaCommentClip, 1);
        airSourceTxt = new EchoMessage("This weird orb seems to be the only source of breathable air here. Better grab it to replenish the air supply.", airFound, 1);
        waterDropTxt = new EchoMessage("Look! We seem to have found some water. Better collect it.", waterFound, 1);

        if (SceneManager.GetActiveScene().name == "TutorialLevel0") {
            sounds.Enqueue(introTxt);
            sounds.Enqueue(introTxt2);
            sounds.Enqueue(hudExplanationTxt);
        }


        if (SceneManager.GetActiveScene().name == "TutorialLevel0")
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            startedTutorial = false;
        }
    }

    private void Update()
    {
        //playing sounds
        if (sounds.Count >= 1 && !busy)
        {
            EchoMessage currText = null;
            currText = sounds.Dequeue();

            if (!currText.maxTextShowsReached()) {
                busy = true;
                textDuration = currText.getDuration();
                currText.incrementTextShows(1);
                uiTextMesh.text = currText.getText();
                currText.playMessage();
            }
        }

        //ENQUEUING SOUNDS
        //health warning
        if (attributes.GetCurrentHealth() * 100 / attributes.GetMaxHealth() < critHealthThreshold && healthWarningTimer <= 0) { 
            sounds.Enqueue(aboutToDieTxt);
            healthWarningTimerEnable = true;
            healthWarningTimer = healthWarningDelay;
        }

        //air warning
        if (attributes.GetCurrentAir() * 100 / attributes.GetMaxAir() < critAirThreshold && airWarningTimer <= 0) { 
            sounds.Enqueue(airCritical);
            airWarningTimerEnable = true;
            airWarningTimer = airWarningDelay;
        }


        //LEVEL 1
        if (SceneManager.GetActiveScene().name == "MainGameScene") {

        }
        //TUTORIAL
        else if (SceneManager.GetActiveScene().name == "TutorialLevel0") {
            if (!startedTutorial && sounds.Count == 0)
            {
                player.GetComponent<PlayerMovement>().enabled = true;
                startedTutorial = true;
            }
        }

        //reseting the text mesh
        if (!busy)
        {
            uiTextMesh.text = "";
            txtBackground.GetComponent<Image>().enabled = false;
            portraitObj.GetComponent<Image>().enabled = false;
        }
        else
        {
            txtBackground.GetComponent<Image>().enabled = true;
            portraitObj.GetComponent<Image>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        //update timer
        if (busy)
        {
            if (textTimer >= textDuration) {
                busy = false;
                textTimer = 0;
            }
            textTimer += Time.deltaTime;
        }

        //health warning timer
        if (healthWarningTimerEnable)
        {
            healthWarningTimer -= Time.deltaTime;
        }
        else if (healthWarningTimer <= 0) {
            healthWarningTimer = 0;
            healthWarningTimerEnable = false;
        }

        //air warning timer
        if (airWarningTimerEnable)
        {
            airWarningTimer -= Time.deltaTime;
        }
        else if (airWarningTimer <= 0) {
            airWarningTimer = 0;
            airWarningTimerEnable = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //LEVEL 1
        if (collision.tag == "ExploreAreaEastLevel1" && attributes.GetCurrentWater() >= manager.waterNeededInPool / 4  && !waterPoolFoundTxt.maxTextShowsReached())
        {
            sounds.Enqueue(objectiveLevelTxt1_4);
        }
        if (collision.tag == "WaterPoolCollisionArea") {
            inPoolArea = true;
            if (!waterPoolFoundTxt.maxTextShowsReached() && attributes.GetCurrentWater() >= manager.waterNeededInPool / 4) {
                sounds.Enqueue(waterPoolFoundTxt);
            }       
        }

        //TUTORIAL
        if (collision.tag == "DestructibleAreaTutorial" && !destructibleExplanationTxt.maxTextShowsReached() && !shootingExplanationTxt.maxTextShowsReached()) {
            sounds.Enqueue(destructibleExplanationTxt);
            sounds.Enqueue(shootingExplanationTxt);
        }
        if (collision.tag == "WallClimbTextArea" && !wallClimbingExplanationTxt.maxTextShowsReached()) {
            sounds.Enqueue(wallClimbingExplanationTxt);
        }
        if (collision.tag == "Lava" && !healthRegenTxt.maxTextShowsReached() && SceneManager.GetActiveScene().name == "TutorialLevel0") {
            sounds.Enqueue(healthRegenTxt);
        }
        if (collision.tag == "EndTutorialArea" && !endTutorialTxt.maxTextShowsReached()) {
            sounds.Enqueue(endTutorialTxt);
        }

        //GENERAL
        if (collision.tag == "AirSourceTextArea" && !airSourceTxt.maxTextShowsReached())
        {
            sounds.Clear();
            sounds.Enqueue(airSourceTxt);
        }
        if (collision.tag == "WaterTextArea" && !waterDropTxt.maxTextShowsReached())
        {
            sounds.Clear();
            sounds.Enqueue(waterDropTxt);
            sounds.Enqueue(objectiveLevelTxt1_2);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name != "TutorialLevel0") {
            if (collision.tag == "RegenExplainArea" && !lavaCommentTxt.maxTextShowsReached() && !busy)
            {
                sounds.Enqueue(lavaCommentTxt);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "WaterPoolCollisionArea")
        {
            inPoolArea = false;
        }
    }

    public void addMessage(EchoMessage msg) {
        sounds.Enqueue(msg);
    }

    public void clearMessages() {
        sounds.Clear();
    }

    public bool isBusy() {
        return busy;
    }

    public bool isInPoolArea() {
        return inPoolArea;
    }
}
