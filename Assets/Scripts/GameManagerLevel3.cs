using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManagerLevel3 : GameManager
{
    [Header("Level 3 Echo Dialogue")]
    public AudioClip objectiveLevelClip3_1;
    public AudioClip objectiveLevelClip3_2;
    public AudioClip objectiveLevelClip3_3;
    public AudioClip objectiveLevelClip3_4;

    [Header("Level 3 Variables")]
    public int waterNeededInPool1;
    public int waterNeededInPool2;

    //[Header("Level Objects")]

    private int waterInPool1;
    private int waterInPool2;

    EchoMessage objectiveLevelTxt3_1;
    EchoMessage objectiveLevelTxt3_2;
    EchoMessage objectiveLevelTxt3_3;
    EchoMessage objectiveLevelTxt3_4;
    EchoMessage frozenBlockHintTxt;

    private GameObject waterParent;
    private GameObject waterParent2;

    protected override void Start()
    {
        base.Start();

        waterInPool1 = 0;
        waterInPool2 = 0;

        objectiveLevelTxt3_1 = new EchoMessage("Now we have to do collection. Collection is when precipitation falls down onto the ground and fills various bodies of water on the surface.", objectiveLevelClip3_1, 1);
        objectiveLevelTxt3_2 = new EchoMessage("Some of the water falls into already existing surface bodies of water. And some of it goes into the groundwater or freezes on top of mountains.", objectiveLevelClip3_2, 1);
        objectiveLevelTxt3_3 = new EchoMessage("If this water does not make it back into surface bodies of water, it cannot evaporate and rejoin the water cycle. Our goal is to now use groundwater and the frozen water on top of the mountain to the west to fill the two water pools in this area.", objectiveLevelClip3_3, 1);
        objectiveLevelTxt3_4 = new EchoMessage("Congratulations! We have successfully performed the collection part of the water cycle. Now we will move on to something special, another form of evaporation known as transpiration! Head to the marker in the middle of this area to proceed.", objectiveLevelClip3_4, 1);

        echo.addMessage(objectiveLevelTxt3_1);
        echo.addMessage(objectiveLevelTxt3_2);
        echo.addMessage(objectiveLevelTxt3_3);

        waterParent = GameObject.Find("WaterPool");
        waterParent2 = GameObject.Find("WaterPool2");
    }

    protected override void Update()
    {
        base.Update();

        if (waterInPool1 >= waterNeededInPool1 && waterInPool2 >= waterNeededInPool2) {
            nextLevelMarker.SetActive(true);
        }

        if (waterInPool1 >= waterNeededInPool1) {
            waterInPool1 = waterNeededInPool1;
            AudioManager.playSplash();
            for (int i = 0; i < waterParent.transform.childCount; i++)
            {
                waterParent.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        if (waterInPool2 >= waterNeededInPool2) {
            waterInPool1 = waterNeededInPool2;
            for (int i = 0; i < waterParent2.transform.childCount; i++)
            {
                waterParent2.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void changeObjectives()
    {
        if (waterInPool1 < waterNeededInPool1)
        {
            objectiveDisplay.text = "Current Objective: Fill the east pool using groundwater from the caves.";
        }
        else if (waterInPool2 < waterNeededInPool2)
        {
            objectiveDisplay.text = "Current Objective: Fill the west pool with liquid water using the ice on top of the mountain.";
        } else
        {
            objectiveDisplay.text = "Current Objective: Proceed to the blue marker at the middle to proceed";
        }
    }

    protected override void checkEchoCollisions()
    {

    }

    protected override void levelEchoMsgChecks()
    {
        if (waterInPool1 >= waterNeededInPool1 && waterInPool2 >= waterNeededInPool2 && !objectiveLevelTxt3_4.maxTextShowsReached() && !echo.containsMessage(objectiveLevelTxt3_4)) {
            echo.addMessage(objectiveLevelTxt3_4);
        }
    }

    public int getWaterNeededInPool1() {
        return waterNeededInPool1;
    }

    public int getWaterNeededInPool2()
    {
        return waterNeededInPool2;
    }

    public int getWaterInPool1() {
        return waterInPool1;
    }

    public int getWaterInPool2()
    {
        return waterInPool2;
    }

    public void setWaterInPool1(int waterAmnt) {
        waterInPool1 = waterAmnt;
    }

    public void setWaterInPool2(int waterAmnt)
    {
        waterInPool2 = waterAmnt;
    }
}
