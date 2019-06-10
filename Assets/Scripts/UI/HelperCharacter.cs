using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Sounds")]
    public AudioClip introClip;
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
    public AudioClip airFound;
    public AudioClip waterFound;
    public AudioClip destructibleFound;
    public AudioClip waterPoolFoundClip;
    public AudioClip butterlfyCommentClip;
    public AudioClip rockWarningClip;
    public AudioClip aboutToDieClip;
    public AudioClip lavaCommentClip;
    public AudioClip airCriticalClip;
    public AudioClip dontWasteWaterReminderClip;

    [Header("Audio Play Conditions")]
    public int critHealthThreshold; //the percentage of health under which the critical damage sound will be played
    public float healthWarningDelay; //amount of time between health warning delays
    public int critAirThreshold; //percentage of air under which the air warning is played
    public float airWarningDelay; //amount of time between air warning delays
    public int waterNeededInPool; //amount of water object collisions needed to fill the pool

    //text mesh where the text will be displayed
    private TextMeshProUGUI uiTextMesh;

    //how long the current text has been displayed for so far
    private float textTimer;

    private int cycle;
    private bool showIntro;

    //Text Objects
    ObjectText airSourceTxt;
    ObjectText waterDropTxt;
    ObjectText destructibleTxt;

    ObjectText introTxt;
    ObjectText objectiveLevelTxt1_0;
    ObjectText objectiveLevelTxt1_1;
    ObjectText objectiveLevelTxt1_2;
    ObjectText objectiveLevelTxt1_3;
    ObjectText objectiveLevelTxt1_4;
    ObjectText objectiveLevelTxt1_5;
    ObjectText objectiveLevelTxt1_6;
    ObjectText objectiveLevelTxt1_7;
    ObjectText objectiveLevelTxt1_8;
    ObjectText objectiveLevelTxt1_9;

    ObjectText waterPoolFoundTxt;
    ObjectText dontWasteWaterReminderTxt;

    ObjectText butterlfyCommentTxt;
    ObjectText rockWarningTxt;
    ObjectText aboutToDieTxt;
    ObjectText lavaCommentTxt;
    ObjectText airCritical;

    private PlayerAttributes attributes;

    //whether the display is already dispalying any text
    private bool isBusy;

    private Queue<ObjectText> sounds;
    private float healthWarningTimer;
    private bool healthWarningTimerEnable;
    private float airWarningTimer;
    private bool airWarningTimerEnable;
    private int waterInPool;
    private bool inPoolArea;
    private float pourTimer;
    private GameManagerScript manager;
    private WaterPourController playerWaterPourController;

    private void Start()
    {
        uiTextMesh = txtMeshContainer.GetComponent<TextMeshProUGUI>();
        textTimer = 0f;
        txtBackground.GetComponent<Image>().enabled = false;
        portraitObj.GetComponent<Image>().enabled = false;
        isBusy = false;
        cycle = 1;
        showIntro = true;
        healthWarningTimerEnable = false;
        inPoolArea = false;
        healthWarningTimer = 0;
        airWarningTimer = 0;
        waterInPool = 0;
        pourTimer = 0;
        airWarningTimerEnable = false;
        manager = managerObj.GetComponent<GameManagerScript>();
        attributes = player.GetComponent<PlayerAttributes>();
        playerWaterPourController = player.GetComponent<WaterPourController>();

        airSourceTxt = new ObjectText("This weird orb seems to be the only source of breathable air in this cave. Better grab it to replenish the air supply.", false, airFound.length + 0.5f, airFound, 1);
        waterDropTxt = new ObjectText("Look! We seem to have found some water. Better collect it.", false, waterFound.length + 0.5f, waterFound, 1);
        destructibleTxt = new ObjectText("The ground in the vicinity seems to be prone to collapse. Try shooting it with your weapon.", false, destructibleFound.length + 0.5f, destructibleFound, 2);

        introTxt = new ObjectText("Hello, I am Echo, your suit's built-in A.I. I hope we can get along, if not, please remember that I control your suit's life support systems.", false, introClip.length + 0.5f, introClip, 1);
        objectiveLevelTxt1_0 = new ObjectText("This planet looks nice. Unfortunately, its water cycle is not functioning. If we want to colonize it, we will have to fix that first.", false, objectiveLevelClip1_0.length + 0.5f, objectiveLevelClip1_0, 1);
        objectiveLevelTxt1_1 = new ObjectText("The first step would be to evaporate some water into the atmosphere. However, there does not seem to be any water on the surface. Maybe we should check if we can release some groundwater in the caves below us.", false, objectiveLevelClip1_1.length + 0.5f, objectiveLevelClip1_1, 1);
        objectiveLevelTxt1_2 = new ObjectText("By my calculations we will need about 12 of these to have enough water to evaporate.", false, objectiveLevelClip1_2.length + 0.5f, objectiveLevelClip1_2, 1);
        objectiveLevelTxt1_3 = new ObjectText("We should have enough water now. Head back to the surface and see if there is somewhere where we can create an artificial lake.", false, objectiveLevelClip1_3.length + 0.5f, objectiveLevelClip1_3, 1);
        objectiveLevelTxt1_4 = new ObjectText("We should probably explore some more to the east.", false, objectiveLevelClip1_4.length + 0.5f, objectiveLevelClip1_4, 1);
        objectiveLevelTxt1_5 = new ObjectText("This should be enough. Now we just need to evaporate it. Unfortunately, this planet's Sun is too weak to do that. We will have to amplify it's heat.", false, objectiveLevelClip1_5.length + 0.5f, objectiveLevelClip1_5, 1);
        objectiveLevelTxt1_6 = new ObjectText("Remember that your weapon mode 2 amplifies heat energy.", false, objectiveLevelClip1_6.length, objectiveLevelClip1_6, 1);
        objectiveLevelTxt1_7 = new ObjectText("Congratulations! We have fixed the first stage of the water cycle -- evaporation. Now we will go up into the atmosphere and see if we can get precipitation to work as well.", false, objectiveLevelClip1_7.length + 0.5f, objectiveLevelClip1_7, 1);
        objectiveLevelTxt1_8 = new ObjectText("There is not enough water in the pool yet. We need some more.", false, objectiveLevelClip1_8.length + 0.5f, objectiveLevelClip1_8, 1);
        objectiveLevelTxt1_9 = new ObjectText("There is not enough water left in the caves for us to fill the pool. We should probably restart.", false, objectiveLevelClip1_9.length + 0.5f, objectiveLevelClip1_9, 1);


        waterPoolFoundTxt = new ObjectText("This looks like a good spot to release our water. Remember, you can do that by holding down F. I will let you know when we have enough water in the pool.", false, waterPoolFoundClip.length + 0.5f, waterPoolFoundClip, 1);
        dontWasteWaterReminderTxt = new ObjectText("Make sure you do not waste it. If you do, check the cave for some more water. If you waste all of it, we will have to restart.", false, dontWasteWaterReminderClip.length + 0.5f, dontWasteWaterReminderClip, 1);

        butterlfyCommentTxt = new ObjectText("Wow. Aren't these beautiful!", false, butterlfyCommentClip.length + 0.5f, butterlfyCommentClip, 1);
        rockWarningTxt = new ObjectText("The ceiling of the cave in this area is prone to collapse. Watch your head!", false, rockWarningClip.length + 0.5f, rockWarningClip, 4);
        aboutToDieTxt = new ObjectText("Warning: operator sustaining critical damage.", false, aboutToDieClip.length +0.5f, aboutToDieClip, 1000);
        airCritical = new ObjectText("Warning: Air supply at critical level.", false, airCriticalClip.length + 0.5f, airCriticalClip, 1000);
        lavaCommentTxt = new ObjectText("It burns! It burns! Make it stop! Just kidding, I cannot feel a thing.", false, lavaCommentClip.length + 0.5f, lavaCommentClip, 1);

        sounds = new Queue<ObjectText>();
        //sounds.Enqueue(objectiveLevelTxt1_0);
        //sounds.Enqueue(objectiveLevelTxt1_1);
    }

    private void Update()
    {
        //playing sounds
        if (sounds.Count >= 1 && !isBusy)
        {
            ObjectText currText = null;
            currText = sounds.Dequeue();

            if (currText.getTextShows() < currText.getMaxTextShows()) {
                isBusy = true;
                textDuration = currText.getDuration();
                currText.setTextShows(currText.getTextShows() + 1);
                uiTextMesh.text = currText.getText();
                AudioManager.playClip(currText.getClip());
            }
        }

        //ENQUEUING SOUNDS
        //health warning
        if (attributes.GetCurrentHealth() * 100 / attributes.GetMaxHealth() < critHealthThreshold && healthWarningTimer <= 0) { 
            sounds.Enqueue(aboutToDieTxt);
            healthWarningTimerEnable = true;
            healthWarningTimer = healthWarningDelay;
        }

        //objectiveLevel1_2
        if (attributes.GetCurrentWater() >= waterNeededInPool / 4 && !isBusy && objectiveLevelTxt1_3.getTextShows() < objectiveLevelTxt1_3.getMaxTextShows()) { 
            sounds.Enqueue(objectiveLevelTxt1_3);
            sounds.Enqueue(dontWasteWaterReminderTxt);
        }

        //air warning
        if (attributes.GetCurrentAir() * 100 / attributes.GetMaxAir() < critAirThreshold && airWarningTimer <= 0) { 
            sounds.Enqueue(airCritical);
            airWarningTimerEnable = true;
            airWarningTimer = airWarningDelay;
        }

        //enough water in pool
        if (manager.getAmountOfWaterInPool() >= waterNeededInPool) {
            sounds.Enqueue(objectiveLevelTxt1_5);
            sounds.Enqueue(objectiveLevelTxt1_6);
        }

        //player did not get enough water in the pool
        if (inPoolArea && manager.getAmountOfWaterInPool() < waterNeededInPool && attributes.GetCurrentWater() == 0 && playerWaterPourController.getFPressed()) {
            sounds.Enqueue(objectiveLevelTxt1_8);
        }

        //not enough water left on the level 1 Water Drop = 4 WaterParticles
        if ((attributes.GetCurrentWater() +  waterDropParent.transform.childCount) * 4 < waterNeededInPool - manager.getAmountOfWaterInPool()) {
            sounds.Enqueue(objectiveLevelTxt1_9);
        }

        //reseting the text mesh
        if (!isBusy)
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
        if (isBusy)
        {
            if (textTimer >= textDuration) {
                isBusy = false;
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
        if (collision.tag == "AirSourceTextArea" && airSourceTxt.getTextShows() < airSourceTxt.getMaxTextShows())
        {
            sounds.Enqueue(airSourceTxt);
        }
        if (collision.tag == "WaterTextArea" && waterDropTxt.getTextShows() < waterDropTxt.getMaxTextShows())
        {
            sounds.Enqueue(waterDropTxt);
            sounds.Enqueue(objectiveLevelTxt1_2);
        }
        if (collision.tag == "DestructibleTextArea" && !isBusy)
        {
            sounds.Enqueue(destructibleTxt);
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (collision.tag == "FireflyArea")
        {
            sounds.Enqueue(butterlfyCommentTxt);
        }

        if (collision.tag == "ExploreAreaEastLevel1" && attributes.GetCurrentWater() >= waterNeededInPool / 4  && waterPoolFoundTxt.getTextShows() < waterPoolFoundTxt.getMaxTextShows())
        {
            sounds.Enqueue(objectiveLevelTxt1_4);
        }
        if (collision.tag == "WaterPoolCollisionArea") {
            inPoolArea = true;
            if (waterPoolFoundTxt.getTextShows() < waterPoolFoundTxt.getMaxTextShows() && attributes.GetCurrentWater() >= waterNeededInPool / 4) {
                sounds.Enqueue(waterPoolFoundTxt);
            }       
        }
        if (collision.tag == "FallingRockArea" && rockWarningTxt.getTextShows() < rockWarningTxt.getMaxTextShows() && !isBusy) {
            sounds.Enqueue(rockWarningTxt);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Lava" && lavaCommentTxt.getTextShows() < lavaCommentTxt.getMaxTextShows() && !isBusy)
        {
            sounds.Enqueue(lavaCommentTxt);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "WaterPoolCollisionArea")
        {
            inPoolArea = false;
        }
    }

    private class ObjectText {
        private bool showText; //whether this text should be shown right now or not
        private int textShows; //number of times this text has been shown
        private string text; //text to be shown
        private float duration;
        private int maxTextShows;
        private AudioClip clip;

        public ObjectText(string text, bool showText, float duration, AudioClip clip, int maxTextShows) {
            this.text = text;
            this.showText = showText;
            this.duration = duration;
            this.clip = clip;
            this.maxTextShows = maxTextShows;
            textShows = 0;
        }


        public bool isShowText() {
            return showText;
        }
        public void setShowText(bool tf) {
            showText = tf;
        }
        public int getTextShows() {
            return textShows;
        }
        public void setTextShows(int textShows) {
            this.textShows = textShows;
        }
        public string getText() {
            return text;
        }
        public void setText(string text) {
            this.text = text;
        }
        public void setDuration(float duration) {
            this.duration = duration;
        }
        public float getDuration() {
            return duration;
        }
        public AudioClip getClip() {
            return clip;
        }
        public int getMaxTextShows() {
            return maxTextShows;
        }
    }
}
