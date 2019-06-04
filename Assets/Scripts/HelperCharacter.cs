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


    private float textTimer;//how long the current text has been displayed for so far

    //Begin Game
    private string intro = "Hello, I am Echo, the built-in A.I. of your suit. I hope we can get along. If not, please remember that I control your suit's life support systems.";
    private string objectiveLevel1_0 = "This planet looks nice. Unfortunately, the water cycle is completely non-existant. If we want to colonize it, you will have to fix the water cycle.";
    private string objectiveLevel1_1 = "The first would be to evaporate some water into the atmosphere using heat energy. There does not seem to be any water on the surface. Maybe we should investigate the cave system below us.";

    //Air Source
    private bool showAirSourceText;
    private int countAirSourceTextShows;
    private string airSourceText = "This weird orb seems to be the only source of breathable air in this cave. Better grab it to replenish the air supply.";

    //Water Droplet
    private bool showWaterDropText;
    private int countWaterDropTextShows;
    private string WaterDropletText = "Look! We seem to have found some water. Better collect it.";

    //Destructibles
    private string groundDestructText = "The ground in the vicinity seems to be prone to collapse. Try shooting it with your weapon.";

    //Wall Hang


    private bool isBusy; //whether the display is already dispalying any text


    private void Start()
    {
        uiTextMesh = txtMeshContainer.GetComponent<TextMeshProUGUI>();

        showAirSourceText = false;
        countAirSourceTextShows = 0;
        textTimer = 0f;
        txtBackground.GetComponent<Image>().enabled = false;
        portraitObj.GetComponent<Image>().enabled = false;
        isBusy = false;
    }

    private void Update()
    {
        //hint text and intros
        if (level == 1) {

        }


        //in-game object descriptions
        if (showAirSourceText && textTimer < textDuration)
        {
            if (!isBusy) {
                countAirSourceTextShows++;
                uiTextMesh.text = airSourceText;
            }
            isBusy = true;
        }
        else if (showWaterDropText && textTimer < textDuration) {
            if (!isBusy) {
                countWaterDropTextShows++;
                uiTextMesh.text = WaterDropletText;
            }
            isBusy = true;
        }
        else
        {
            showAirSourceText = false;
            showWaterDropText = false;
            isBusy = false;
        }
        
        

        if (!showAirSourceText && !showWaterDropText)
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
        if (showAirSourceText || showWaterDropText)
        {
            textTimer += Time.deltaTime;
        }
        else {
            textTimer = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "AirSourceTextArea" && countAirSourceTextShows < 1) {
            showAirSourceText = true;
        }
        else if (collision.tag == "WaterTextArea" && countWaterDropTextShows < 1) {
            showWaterDropText = true;
        }
    }
}
