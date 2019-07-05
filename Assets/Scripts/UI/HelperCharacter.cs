using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelperCharacter : MonoBehaviour
{
    [Header("Game Utilities")]
    public GameObject player;
    
    [Header("UI Objects")]
    public GameObject txtMeshContainer;
    public GameObject txtBackground;
    public GameObject portraitObj;
    public GameObject muteButton;

    [Header("Sounds General")]
    public AudioClip aboutToDieClip;
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
    private float textDuration;

    //GENERAL
    private EchoMessage aboutToDieTxt;
    private EchoMessage airCritical;

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
    private Image portraitImage;
    private Image txtBackgroundImg;

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
        attributes = player.GetComponent<PlayerAttributes>();

        portraitImage = portraitObj.GetComponent<Image>();
        txtBackgroundImg = txtBackground.GetComponent<Image>();

        //GENERAL
        aboutToDieTxt = new EchoMessage("Warning: operator sustaining critical damage.", aboutToDieClip, 1000);
        airCritical = new EchoMessage("Warning: Air supply at critical level.", airCriticalClip, 1000);
    }

    private void Update()
    {
        //text duration timer
        if (textTimer >= textDuration)
        {
            busy = false;
            textTimer = 0;
        }

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
        /*
        if (attributes.GetCurrentHealth() * 100 / attributes.GetMaxHealth() < critHealthThreshold && healthWarningTimer <= 0 && !isBusy()) { 
            sounds.Enqueue(aboutToDieTxt);
            healthWarningTimerEnable = true;
            healthWarningTimer = healthWarningDelay;
        }*/

        //reseting the text mesh
        if (!busy)
        {
            uiTextMesh.text = "";
            txtBackgroundImg.enabled = false;
            portraitImage.enabled = false;
            muteButton.SetActive(false);
        }
        else
        {
            txtBackgroundImg.enabled = true;
            portraitImage.enabled = true;
            muteButton.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        //update text duration timer
        if (busy)
        {
            textTimer += Time.deltaTime;
        }

        //health warning timer
        if (healthWarningTimerEnable)
        {
            healthWarningTimer -= Time.fixedDeltaTime;
        }
        else if (healthWarningTimer <= 0) {
            healthWarningTimer = 0;
            healthWarningTimerEnable = false;
        }

        //air warning timer
        if (airWarningTimerEnable)
        {
            airWarningTimer -= Time.fixedDeltaTime;
        }
        else if (airWarningTimer <= 0) {
            airWarningTimer = 0;
            airWarningTimerEnable = false;
        }
    }

    public void addMessage(EchoMessage msg) {
        sounds.Enqueue(msg);
    }

    public void clearMessages() {
        sounds.Clear();
    }

    public bool containsMessage(EchoMessage msg) {
        return sounds.Contains(msg);
    }

    public bool isBusy() {
        return busy;
    }
}
