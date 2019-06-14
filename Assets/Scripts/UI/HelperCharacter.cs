using System.Collections;
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
    public AudioClip controlExplanationClip;
    public AudioClip hudExplanationClip;
    public AudioClip destructibleExplanation;
    public AudioClip wallClimbingExplanationClip;
    public AudioClip shootingExplanation;
    public AudioClip healthRegenExplanation;
    public AudioClip endTutorialClip;

    [Header("Sounds Level 1")]
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
    public AudioClip waterFound;
    public AudioClip waterPoolFoundClip;
    public AudioClip butterlfyCommentClip;
    public AudioClip rockWarningClip;
    public AudioClip dontWasteWaterReminderClip;
    public AudioClip echoDirectionClip1;
    public AudioClip echoDirectionClip2;

    [Header("Sounds General")]
    public AudioClip airFound;
    public AudioClip aboutToDieClip;
    public AudioClip lavaCommentClip;
    public AudioClip airCriticalClip;
    public AudioClip destructibleFound;

    [Header("Audio Play Conditions")]
    public int critHealthThreshold; //the percentage of health under which the critical damage sound will be played
    public float healthWarningDelay; //amount of time between health warning delays
    public int critAirThreshold; //percentage of air under which the air warning is played
    public float airWarningDelay; //amount of time between air warning delays

    //text mesh where the text will be displayed
    private TextMeshProUGUI uiTextMesh;

    //how long the current text has been displayed for so far
    private float textTimer;

    private int cycle;
    private bool showIntro;

    //TUTORIAL 
    ObjectText introTxt;
    ObjectText introTxt2;
    ObjectText controlExplanationTxt;
    ObjectText hudExplanationTxt;
    ObjectText destructibleExplanationTxt;
    ObjectText wallClimbingExplanationTxt;
    ObjectText shootingExplanationTxt;
    ObjectText healthRegenTxt;
    ObjectText endTutorialTxt;

    //LEVEL 1
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

    ObjectText echoDirectionTxt1;
    ObjectText echoDirectionTxt2;

    ObjectText waterPoolFoundTxt;
    ObjectText dontWasteWaterReminderTxt;
    ObjectText butterlfyCommentTxt;
    ObjectText rockWarningTxt;

    //GENERAL
    ObjectText aboutToDieTxt;
    ObjectText lavaCommentTxt;
    ObjectText airCritical;
    ObjectText airSourceTxt;
    ObjectText waterDropTxt;
    ObjectText destructibleTxt;

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
    private bool startedTutorial;

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

       
        //TUTORIAL
        introTxt = new ObjectText("Hello, I am Echo, your suit's built-in A.I. I hope we can get along, if not, please remember that I control your suit's life support systems.", false, introClip.length + 0.5f, introClip, 1);
        introTxt2 = new ObjectText("Your mission is to terraform a planet's water cycle so that it may become inhabitable. Apparently, your species is dying out and needs another planet to live on. This suit and I were built for the purpose of helping you achieve this goal. We will now begin your training.", false, introClip2.length + 0.5f, introClip2, 1);
        controlExplanationTxt = new ObjectText("Use the A and D keys to move. Use W to jump.", false, controlExplanationClip.length + 0.5f, controlExplanationClip, 1);
        hudExplanationTxt = new ObjectText("Your suit has a built-in display at the top left of your screen. It tells you useful information about your resources and life support system state.", false, hudExplanationClip.length + 0.5f, hudExplanationClip, 1); ;
        destructibleExplanationTxt = new ObjectText("This material has weak physical properties and can be destroyed with your weapon.", false, destructibleExplanation.length + 0.5f, destructibleExplanation, 1); ;
        wallClimbingExplanationTxt = new ObjectText("To wall climb, hold SPACE near a wall during your jump. To jump off the wall, use W while not holding A and D.", false, wallClimbingExplanationClip.length + 0.5f, wallClimbingExplanationClip, 1); ;
        shootingExplanationTxt = new ObjectText("Shoot using the left mouse button. You can use the 1, 2 and 3 keys to change weapon modes. Weapon 1 is your regular bullets. Weapon modes 2 and 3 use heat energy to respectively heat and cool down your environment.", false, shootingExplanation.length + 0.5f, shootingExplanation, 1); ;
        healthRegenTxt = new ObjectText("The planet we are going to will have many environmental hazards. Your suit will protect you from most of them. However, if your health reaches critical levels, the suit will passively regenerate your health.", false, healthRegenExplanation.length + 0.5f, healthRegenExplanation, 1);
        endTutorialTxt = new ObjectText("Let's see what you have learned. Make your way to the marker on the other side of this lava pool to complete the level.", false, endTutorialClip.length + 0.5f, endTutorialClip, 1);

        //LEVEL 1
        objectiveLevelTxt1_0 = new ObjectText("If we want to colonize this planet, we will need to fix its water cycle first. The first step is to evaporate water from surface bodies of water into the atmosphere.", false, objectiveLevelClip1_0.length + 0.5f, objectiveLevelClip1_0, 1);
        objectiveLevelTxt1_1 = new ObjectText("There is no water on the surface, I suggest you check the caves below us for some groundwater that we can collect and use.", false, objectiveLevelClip1_1.length + 0.5f, objectiveLevelClip1_1, 1);
        objectiveLevelTxt1_2 = new ObjectText("By my calculations we will need about 14 of these to have enough water to evaporate. You can see the current amount on your HUD.", false, objectiveLevelClip1_2.length + 0.5f, objectiveLevelClip1_2, 1);
        objectiveLevelTxt1_3 = new ObjectText("We should have enough water now. Head back to the surface and find a place for an artificial lake.", false, objectiveLevelClip1_3.length + 0.5f, objectiveLevelClip1_3, 1);
        objectiveLevelTxt1_4 = new ObjectText("We should probably explore some more to the east.", false, objectiveLevelClip1_4.length + 0.5f, objectiveLevelClip1_4, 1);
        objectiveLevelTxt1_5 = new ObjectText("Now we just need to evaporate the water. This can be done using heat energy. We will need to find a source.", false, objectiveLevelClip1_5.length + 0.5f, objectiveLevelClip1_5, 1);
        objectiveLevelTxt1_6 = new ObjectText("Use the 2 key to amplify the heat energy collected and use it to heat the water.", false, objectiveLevelClip1_6.length, objectiveLevelClip1_6, 1);
        objectiveLevelTxt1_7 = new ObjectText("Congratulations! We have fixed the first stage of the water cycle. As water is heated by the sun, it evaporates in small amounts and rises in the atmosphere, which is where we are going next.", false, objectiveLevelClip1_7.length + 0.5f, objectiveLevelClip1_7, 1);

        echoDirectionTxt1 = new ObjectText("I do not see any way across. We should probably turn around for now.", false, echoDirectionClip1.length + 0.5f, echoDirectionClip1, 1);
        echoDirectionTxt2 = new ObjectText("I think this platform can be a shortcut, try jumping on it.", false, echoDirectionClip2.length + 0.5f, echoDirectionClip2, 1);

        //TODO: this should be done by a progress bar or something
        objectiveLevelTxt1_8 = new ObjectText("There is not enough water in the pool yet. We need some more.", false, objectiveLevelClip1_8.length + 0.5f, objectiveLevelClip1_8, 1);
        objectiveLevelTxt1_9 = new ObjectText("There is not enough water left in the caves for us to fill the pool. We should probably restart.", false, objectiveLevelClip1_9.length + 0.5f, objectiveLevelClip1_9, 1);

        //TODO: this should be done by a pop up
        waterPoolFoundTxt = new ObjectText("This looks like a good spot to release our water. Remember, you can do that by holding down F. I will let you know when we have enough water in the pool.", false, waterPoolFoundClip.length + 0.5f, waterPoolFoundClip, 1);
        dontWasteWaterReminderTxt = new ObjectText("Make sure you do not waste it. If you do, check the cave for some more water. If you waste all of it, we will have to restart.", false, dontWasteWaterReminderClip.length + 0.5f, dontWasteWaterReminderClip, 1);

        butterlfyCommentTxt = new ObjectText("Wow. Aren't these beautiful!", false, butterlfyCommentClip.length + 0.5f, butterlfyCommentClip, 1);
        rockWarningTxt = new ObjectText("The ceiling of the cave in this area is prone to collapse. Watch your head!", false, rockWarningClip.length + 0.5f, rockWarningClip, 4);
        
        
        //GENERAL
        aboutToDieTxt = new ObjectText("Warning: operator sustaining critical damage.", false, aboutToDieClip.length +0.5f, aboutToDieClip, 1000);
        airCritical = new ObjectText("Warning: Air supply at critical level.", false, airCriticalClip.length + 0.5f, airCriticalClip, 1000);
        lavaCommentTxt = new ObjectText("It burns! It burns! Make it stop! Just kidding, I cannot feel a thing.", false, lavaCommentClip.length + 0.5f, lavaCommentClip, 1);
        airSourceTxt = new ObjectText("This weird orb seems to be the only source of breathable air here. Better grab it to replenish the air supply.", false, airFound.length + 0.5f, airFound, 1);
        waterDropTxt = new ObjectText("Look! We seem to have found some water. Better collect it.", false, waterFound.length + 0.5f, waterFound, 1);
        destructibleTxt = new ObjectText("The ground in the vicinity seems to be prone to collapse. Try shooting it with your weapon.", false, destructibleFound.length + 1f, destructibleFound, 2);


        sounds = new Queue<ObjectText>();

        if (SceneManager.GetActiveScene().name == "MainGameScene") {
            sounds.Enqueue(objectiveLevelTxt1_0);
            sounds.Enqueue(objectiveLevelTxt1_1);
        }
        else if (SceneManager.GetActiveScene().name == "TutorialLevel0") {
            sounds.Enqueue(introTxt);
            sounds.Enqueue(introTxt2);
            sounds.Enqueue(controlExplanationTxt);
            sounds.Enqueue(hudExplanationTxt);
        }


        if (SceneManager.GetActiveScene().name == "TutorialLevel0")
        {
            //player.GetComponent<PlayerMovement>().enabled = false;
            //startedTutorial = false;
        }
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
                AudioManager.playVoiceClip(currText.getClip());
            }
        }

        //ENQUEUING SOUNDS
        //health warning
        if (attributes.GetCurrentHealth() * 100 / attributes.GetMaxHealth() < critHealthThreshold && healthWarningTimer <= 0) { 
            sounds.Enqueue(aboutToDieTxt);
            healthWarningTimerEnable = true;
            healthWarningTimer = healthWarningDelay;
        }

        //enough water collected
        if (attributes.GetCurrentWater() >= manager.waterNeededInPool / 4 && !isBusy && objectiveLevelTxt1_3.getTextShows() < objectiveLevelTxt1_3.getMaxTextShows()) { 
            sounds.Enqueue(objectiveLevelTxt1_3);
            sounds.Enqueue(dontWasteWaterReminderTxt);
        }

        //air warning
        if (attributes.GetCurrentAir() * 100 / attributes.GetMaxAir() < critAirThreshold && airWarningTimer <= 0) { 
            sounds.Enqueue(airCritical);
            airWarningTimerEnable = true;
            airWarningTimer = airWarningDelay;
        }


        //LEVEL 1
        if (SceneManager.GetActiveScene().name == "MainGameScene") {
            //enough water in pool
            if (manager.getAmountOfWaterInPool() >= manager.waterNeededInPool)
            {
                sounds.Enqueue(objectiveLevelTxt1_5);
                sounds.Enqueue(objectiveLevelTxt1_6);
            }

            //player did not get enough water in the pool
            if (inPoolArea && manager.getAmountOfWaterInPool() < manager.waterNeededInPool && attributes.GetCurrentWater() == 0 && playerWaterPourController.getFPressed())
            {
                sounds.Enqueue(objectiveLevelTxt1_8);
            }

            //not enough water left on the level 1 Water Drop = 4 WaterParticles
            if ((attributes.GetCurrentWater() + waterDropParent.transform.childCount) * 4 < manager.waterNeededInPool - manager.getAmountOfWaterInPool())
            {
                sounds.Enqueue(objectiveLevelTxt1_9);
            }

            //evaporated enough water
            if (manager.getEvaporated() >= GameObject.Find("RisingSteam").GetComponent<RisingSteamManager>().waterThreshold) {
                sounds.Enqueue(objectiveLevelTxt1_7);
            }
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
        //LEVEL 1
        if (collision.tag == "FireflyArea" && !isBusy && butterlfyCommentTxt.getTextShows() < butterlfyCommentTxt.getMaxTextShows())
        {
            sounds.Enqueue(butterlfyCommentTxt);
        }

        if (collision.tag == "ExploreAreaEastLevel1" && attributes.GetCurrentWater() >= manager.waterNeededInPool / 4  && waterPoolFoundTxt.getTextShows() < waterPoolFoundTxt.getMaxTextShows())
        {
            sounds.Enqueue(objectiveLevelTxt1_4);
        }
        if (collision.tag == "WaterPoolCollisionArea") {
            inPoolArea = true;
            if (waterPoolFoundTxt.getTextShows() < waterPoolFoundTxt.getMaxTextShows() && attributes.GetCurrentWater() >= manager.waterNeededInPool / 4) {
                sounds.Enqueue(waterPoolFoundTxt);
            }       
        }
        if (collision.tag == "FallingRockArea" && rockWarningTxt.getTextShows() < rockWarningTxt.getMaxTextShows() && !isBusy) {
            sounds.Enqueue(rockWarningTxt);
        }
        if (collision.tag == "EchoDirection1" && echoDirectionTxt1.getTextShows() < echoDirectionTxt1.getMaxTextShows()) {
            sounds.Enqueue(echoDirectionTxt1);
        }
        if (collision.tag == "EchoDirectionArea2" && echoDirectionTxt2.getTextShows() < echoDirectionTxt2.getMaxTextShows()) {
            sounds.Enqueue(echoDirectionTxt2);
        }

        //TUTORIAL
        if (collision.tag == "DestructibleAreaTutorial" && destructibleExplanationTxt.getTextShows() < destructibleExplanationTxt.getMaxTextShows() && shootingExplanationTxt.getTextShows() < shootingExplanationTxt.getMaxTextShows()) {
            sounds.Enqueue(destructibleExplanationTxt);
            sounds.Enqueue(shootingExplanationTxt);
        }
        if (collision.tag == "WallClimbTextArea" && wallClimbingExplanationTxt.getTextShows() < wallClimbingExplanationTxt.getMaxTextShows()) {
            sounds.Enqueue(wallClimbingExplanationTxt);
        }
        if (collision.tag == "Lava" && healthRegenTxt.getTextShows() < healthRegenTxt.getMaxTextShows() && SceneManager.GetActiveScene().name == "TutorialLevel0") {
            sounds.Enqueue(healthRegenTxt);
        }
        if (collision.tag == "EndTutorialArea" && endTutorialTxt.getTextShows() < endTutorialTxt.getMaxTextShows()) {
            sounds.Enqueue(endTutorialTxt);
        }
        //GENERAL
        if (collision.tag == "AirSourceTextArea" && airSourceTxt.getTextShows() < airSourceTxt.getMaxTextShows())
        {
            sounds.Clear();
            sounds.Enqueue(airSourceTxt);
        }
        if (collision.tag == "WaterTextArea" && waterDropTxt.getTextShows() < waterDropTxt.getMaxTextShows())
        {
            sounds.Clear();
            sounds.Enqueue(waterDropTxt);
            sounds.Enqueue(objectiveLevelTxt1_2);
        }
        if (collision.tag == "DestructibleTextArea" && !isBusy)
        {
            sounds.Enqueue(destructibleTxt);
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name != "TutorialLevel0") {
            if (collision.tag == "RegenExplainArea" && lavaCommentTxt.getTextShows() < lavaCommentTxt.getMaxTextShows() && !isBusy)
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
