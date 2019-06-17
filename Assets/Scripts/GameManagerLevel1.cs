using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerLevel1 : MonoBehaviour
{
    [Header("Level 1 Echo Dialogue")]
    public AudioClip objectiveLevelClip1_0;
    public AudioClip objectiveLevelClip1_1;
    public AudioClip objectiveLevelClip1_3;
    public AudioClip objectiveLevelClip1_5;
    public AudioClip objectiveLevelClip1_6;
    public AudioClip objectiveLevelClip1_7;
    public AudioClip objectiveLevelClip1_8;
    public AudioClip objectiveLevelClip1_9;
    public AudioClip dontWasteWaterReminderClip;

    [Header("Level 1 Variables")]
    public int waterNeededInPool;

    [Header("Level 1 Objects")]
    public GameObject nextLevelMarker;
    public GameObject helperChar;
    public GameObject player;
    public GameObject waterDropParent;

    private int amountOfWaterInPool;
    private int amountOfEvaporatedWater;
    private HelperCharacter echo;
    private PlayerAttributes attributes;
    private WaterPourController waterPControl;


    EchoMessage objectiveLevelTxt1_0;
    EchoMessage objectiveLevelTxt1_1;
    EchoMessage objectiveLevelTxt1_3;

    EchoMessage objectiveLevelTxt1_5;
    EchoMessage objectiveLevelTxt1_6;
    EchoMessage objectiveLevelTxt1_7;
    EchoMessage objectiveLevelTxt1_8;
    EchoMessage objectiveLevelTxt1_9;

    EchoMessage dontWasteWaterReminderTxt;

    private void Start()
    {
        objectiveLevelTxt1_0 = new EchoMessage("If we want to colonize this planet, we will need to fix its water cycle first. The first step is to evaporate water from surface bodies of water into the atmosphere.", objectiveLevelClip1_0, 1);
        objectiveLevelTxt1_1 = new EchoMessage("There is no water on the surface, I suggest you check the caves below us for some groundwater that we can collect and use.", objectiveLevelClip1_1, 1);
        objectiveLevelTxt1_3 = new EchoMessage("We should have enough water now. Head back to the surface and find a place for an artificial lake.", objectiveLevelClip1_3, 1);
        objectiveLevelTxt1_5 = new EchoMessage("Now we just need to evaporate the water. This can be done using heat energy. We will need to find a source.", objectiveLevelClip1_5, 1);
        objectiveLevelTxt1_6 = new EchoMessage("Use the 2 key to amplify the heat energy collected and use it to heat the water.", objectiveLevelClip1_6, 1);
        objectiveLevelTxt1_7 = new EchoMessage("Congratulations! We have fixed the first stage of the water cycle. As water is heated by the sun, it evaporates in small amounts and rises in the atmosphere, which is where we are going next.", objectiveLevelClip1_7, 1);
        objectiveLevelTxt1_8 = new EchoMessage("There is not enough water in the pool yet. We need some more.", objectiveLevelClip1_8, 1);
        objectiveLevelTxt1_9 = new EchoMessage("There is not enough water left in the caves for us to fill the pool. We should probably restart.", objectiveLevelClip1_9, 1);

        dontWasteWaterReminderTxt = new EchoMessage("Make sure you do not waste it. If you do, check the cave for some more water. If you waste all of it, we will have to restart.", dontWasteWaterReminderClip, 1);

        amountOfWaterInPool = 0;
        amountOfEvaporatedWater = 0;

        nextLevelMarker.SetActive(false);

        attributes = player.GetComponent<PlayerAttributes>();
        waterPControl = player.GetComponent<WaterPourController>();
        echo = helperChar.GetComponent<HelperCharacter>();
        echo.addMessage(objectiveLevelTxt1_0);
        echo.addMessage(objectiveLevelTxt1_1);
    }

    private void Update()
    {
        //check for whether the player has collected enough water
        if (attributes.GetCurrentWater() >= waterNeededInPool / 4 && !echo.isBusy() && !objectiveLevelTxt1_3.maxTextShowsReached())
        {
            echo.addMessage(objectiveLevelTxt1_3);
            echo.addMessage(dontWasteWaterReminderTxt);
        }

        //check for whether the pool has enough water
        if (amountOfWaterInPool >= waterNeededInPool)
        {
            echo.addMessage(objectiveLevelTxt1_5);
            echo.addMessage(objectiveLevelTxt1_6);

        }
        //player did not fill the pool with enough water
        if (echo.isInPoolArea() && amountOfWaterInPool < waterNeededInPool && attributes.GetCurrentWater() == 0 && waterPControl.getFPressed())
        {
            echo.addMessage(objectiveLevelTxt1_8);
        }

        //not enough water left on level
        if ((attributes.GetCurrentWater() + waterDropParent.transform.childCount) * 4 < waterNeededInPool - amountOfWaterInPool)
        {
            echo.addMessage(objectiveLevelTxt1_9);
        }

        //evaporated enough water
        if (amountOfEvaporatedWater >= GameObject.Find("RisingSteam").GetComponent<RisingSteamManager>().waterThreshold)
        {
            echo.addMessage(objectiveLevelTxt1_7);
            nextLevelMarker.SetActive(true);
        }
    }

    public int getAmountOfWaterInPool()
    {
        return amountOfWaterInPool;

    }
    public void setAmountOfWaterInPool(int amount)
    {
        amountOfWaterInPool = amount;
    }


    public int getEvaporated()
    {
        return amountOfEvaporatedWater;

    }
    public void setEvaporated(int amount)
    {
        amountOfEvaporatedWater = amount;
    }
}
