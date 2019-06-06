using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelperCharacter : MonoBehaviour
{
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
    public AudioClip airFound;
    public AudioClip waterFound;
    public AudioClip destructibleFound;
    public AudioClip waterPoolFoundClip;
    public AudioClip butterlfyCommentClip;
    public AudioClip rockWarningClip;
    public AudioClip aboutToDieClip;
    public AudioClip lavaCommentClip;

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

    ObjectText waterPoolFoundTxt;

    ObjectText butterlfyCommentTxt;
    ObjectText rockWarningTxt;
    ObjectText aboutToDieTxt;
    ObjectText lavaCommentTxt;

    //whether the display is already dispalying any text
    private bool isBusy;

    private Queue<ObjectText> sounds;

    private void Start()
    {
        uiTextMesh = txtMeshContainer.GetComponent<TextMeshProUGUI>();
        textTimer = 0f;
        txtBackground.GetComponent<Image>().enabled = false;
        portraitObj.GetComponent<Image>().enabled = false;
        isBusy = false;
        cycle = 1;
        showIntro = true;

        airSourceTxt = new ObjectText("This weird orb seems to be the only source of breathable air in this cave. Better grab it to replenish the air supply.", false, 4, airFound, 1);
        waterDropTxt = new ObjectText("Look! We seem to have found some water. Better collect it.", false, 4, waterFound, 1);
        destructibleTxt = new ObjectText("The ground in the vicinity seems to be prone to collapse. Try shooting it with your weapon.", false, 4, destructibleFound, 2);

        introTxt = new ObjectText("Hello, I am Echo, the built-in A.I. of your suit. I hope we can get along. If not, please remember that I control your suit's life support systems.", false, 4, introClip, 1);
        objectiveLevelTxt1_0 = new ObjectText("This planet looks nice. Unfortunately, the water cycle is completely non-existent. If we want to colonize it, you will have to fix the water cycle.", false, objectiveLevelClip1_0.length, objectiveLevelClip1_0, 1);
        objectiveLevelTxt1_1 = new ObjectText("The first step would be to evaporate some water into the atmosphere using heat energy. There does not seem to be any water on the surface. Maybe we should investigate the cave system below us.", false, objectiveLevelClip1_1.length, objectiveLevelClip1_1, 1);
        objectiveLevelTxt1_2 = new ObjectText("By my calculations we will need about 9 of these to have enough water to evaporate.", false, 4, objectiveLevelClip1_2, 1);
        objectiveLevelTxt1_3 = new ObjectText("We have enough water now. We should find a way back to the surface and see if there is any area that we can use to create a lake.", false, 4, objectiveLevelClip1_3, 1);
        objectiveLevelTxt1_4 = new ObjectText("We should probably explore some more to the east.", false, 4, objectiveLevelClip1_4, 1);
        objectiveLevelTxt1_5 = new ObjectText("This should be enough. Now we just need to evaporate it. Unfortunately, this planet's Sun is too weak to do that. We will have to collect heat from it and amplify it.", false, 4, objectiveLevelClip1_5, 1);
        objectiveLevelTxt1_6 = new ObjectText("Remember that your weapon mode 2 amplifies heat energy.", false, 4, objectiveLevelClip1_6, 1);

        waterPoolFoundTxt = new ObjectText("This looks like a good spot to release our water. Remember, you can do that by holding down F.", false, 4, waterPoolFoundClip, 1);

        butterlfyCommentTxt = new ObjectText("Wow. Aren't these beautiful!", false, 4, butterlfyCommentClip, 1);
        rockWarningTxt = new ObjectText("The ceiling of the cave in this area is prone to collapse. Watch your head!", false, 4, rockWarningClip, 1);
        aboutToDieTxt = new ObjectText("Warning: operator sustaining critical damage.", false, 4, aboutToDieClip, 1);
        lavaCommentTxt = new ObjectText("Better not stay in here for too long, your suit won't be able to take this much heat.", false, 4, lavaCommentClip, 1);

        sounds = new Queue<ObjectText>();
        sounds.Enqueue(objectiveLevelTxt1_0);
        sounds.Enqueue(objectiveLevelTxt1_1);
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

        //reseting the text mesh
        if (!isBusy)
        {
            uiTextMesh.text = "";
            txtBackground.GetComponent<Image>().enabled = false;
            portraitObj.GetComponent<Image>().enabled = false;
        }
        else {
            txtBackground.GetComponent<Image>().enabled = true;
            portraitObj.GetComponent<Image>().enabled = true;
        }

        //enqueuing new sounds
    }

    private void FixedUpdate()
    {
        Debug.Log("Duration: " +textDuration);
        Debug.Log("Timer: " + textTimer);

        //update timer
        if (isBusy)
        {
            if (textTimer >= textDuration) {
                isBusy = false;
                textTimer = 0;
            }
            textTimer += Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "AirSourceTextArea" && airSourceTxt.getTextShows() < 1)
        {
            sounds.Enqueue(airSourceTxt);
        }
        else if (collision.tag == "WaterTextArea" && waterDropTxt.getTextShows() < 1)
        {
            sounds.Enqueue(waterDropTxt);
        }
        else if (collision.tag == "DestructibleTextArea" && destructibleTxt.getTextShows() < 2 && !isBusy)
        {
            sounds.Enqueue(destructibleTxt);
            collision.GetComponent<BoxCollider2D>().enabled = false;
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
