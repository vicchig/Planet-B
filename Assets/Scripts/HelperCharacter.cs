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

    //text mesh where the text will be displayed
    private TextMeshProUGUI uiTextMesh;

    //how long the current text has been displayed for so far
    private float textTimer;

    //Begin Game
    private string intro = "Hello, I am Echo, the built-in A.I. of your suit. I hope we can get along. If not, please remember that I control your suit's life support systems.";
    private string objectiveLevel1_0 = "This planet looks nice. Unfortunately, the water cycle is completely non-existant. If we want to colonize it, you will have to fix the water cycle.";
    private string objectiveLevel1_1 = "The first would be to evaporate some water into the atmosphere using heat energy. There does not seem to be any water on the surface. Maybe we should investigate the cave system below us.";
    private int cycle;
    private bool showIntro;

    //Text Objects
    ObjectText airSourceTxt;
    ObjectText waterDropTxt;
    ObjectText destructibleTxt;

    //whether the display is already dispalying any text
    private bool isBusy; 


    private void Start()
    {
        uiTextMesh = txtMeshContainer.GetComponent<TextMeshProUGUI>();
        textTimer = 0f;
        txtBackground.GetComponent<Image>().enabled = false;
        portraitObj.GetComponent<Image>().enabled = false;
        isBusy = false;
        cycle = 1;
        showIntro = true;

        airSourceTxt = new ObjectText("This weird orb seems to be the only source of breathable air in this cave. Better grab it to replenish the air supply.", false);
        waterDropTxt = new ObjectText("Look! We seem to have found some water. Better collect it.", false);
        destructibleTxt = new ObjectText("The ground in the vicinity seems to be prone to collapse. Try shooting it with your weapon.", false);
    }

    private void Update()
    {
        //hint text and intros
        if (level == 1) {
            if (showIntro && cycle == 1)
            {
                textDuration = AudioManager.intro1.length;
                AudioManager.playIntro1();
                uiTextMesh.text = intro;
                if (textTimer >= textDuration)
                {
                    cycle++;
                    textTimer = 0;
                }
            }
            else if (showIntro && cycle == 2)
            {
                uiTextMesh.text = objectiveLevel1_0;
                if (textTimer >= textDuration)
                {
                    cycle++;
                    textTimer = 0;
                }
            }
            else if (showIntro && cycle == 3)
            {
                uiTextMesh.text = objectiveLevel1_1;
                if (textTimer >= textDuration)
                {
                    cycle++;
                    textTimer = 0;
                }
            }
            else {
                showIntro = false;
                AudioManager.stopPlaying();
            }
        }

        //in-game object descriptions
        if (textTimer < textDuration) {
            if (!isBusy) {
                if (airSourceTxt.isShowText())
                {

                    airSourceTxt.setTextShows(airSourceTxt.getTextShows() + 1);
                    uiTextMesh.text = airSourceTxt.getText();

                    isBusy = true;
                }
                else if (waterDropTxt.isShowText())
                {
                    waterDropTxt.setTextShows(waterDropTxt.getTextShows() + 1);
                    uiTextMesh.text = waterDropTxt.getText();

                    isBusy = true;
                }
                else if (destructibleTxt.isShowText())
                {
                    destructibleTxt.setTextShows(destructibleTxt.getTextShows() + 1);
                    uiTextMesh.text = destructibleTxt.getText();

                    isBusy = true;
                }
            }
        }
        else
        {
            airSourceTxt.setShowText(false);
            waterDropTxt.setShowText(false);
            destructibleTxt.setShowText(false);
            isBusy = false;
        }


        if (!airSourceTxt.isShowText() && !waterDropTxt.isShowText() && !destructibleTxt.isShowText() && !showIntro )
        {
            uiTextMesh.text = "";
            txtBackground.GetComponent<Image>().enabled = false;
            portraitObj.GetComponent<Image>().enabled = false;
        }
        else {
            txtBackground.GetComponent<Image>().enabled = true;
            portraitObj.GetComponent<Image>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (airSourceTxt.isShowText() || waterDropTxt.isShowText() || destructibleTxt.isShowText() || showIntro )
        {
            textTimer += Time.deltaTime;
        }
        else {
            textTimer = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "AirSourceTextArea" && airSourceTxt.getTextShows() < 1) {
            airSourceTxt.setShowText(true);
        }
        else if (collision.tag == "WaterTextArea" && waterDropTxt.getTextShows() < 1) {
            waterDropTxt.setShowText(true);
        }
        else if (collision.tag == "DestructibleTextArea" && destructibleTxt.getTextShows() < 2) {
            destructibleTxt.setShowText(true);
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private class ObjectText {
        private bool showText; //whether this text should be shown right now or not
        private int textShows; //number of times this text has been shown
        private string text; //text to be shown

        public ObjectText(string text, bool showText) {
            this.text = text;
            this.showText = showText;
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
    }
}
