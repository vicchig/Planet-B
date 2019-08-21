using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel3_1 : GameManagerCollection
{
    [Header("Level 3_1 Echo Dialogue")]
    public AudioClip objectiveLevelClip3_1;
    public AudioClip objectiveLevelClip3_2;
    public AudioClip objectiveLevelClip3_4;

    EchoMessage objectiveLevelTxt3_1;
    EchoMessage objectiveLevelTxt3_2;
    EchoMessage objectiveLevelTxt3_4;
    EchoMessage frozenBlockHintTxt;

    protected override void Start()
    {
        base.Start();

        objectiveLevelTxt3_1 = new EchoMessage("Collection is the process of water from precipitation and groundwater returning to surface bodies of water to be evaporated again. If this did not happen, eventually all bodies of water would become completely dry.", objectiveLevelClip3_1, 1);
        objectiveLevelTxt3_2 = new EchoMessage("Some of the water from precipitation falls directly into bodies of water. Other water however gets stored in various places such as mountain glaciers and groundwater and has to be released back into surface waters.", objectiveLevelClip3_2, 1);
        objectiveLevelTxt3_4 = new EchoMessage("Congratulations! We have put back the required water in order to sustain evaporation, and with it, the entire cycle. Head to the marker in the middle of this area to proceed.", objectiveLevelClip3_4, 1);

        echo.addMessage(objectiveLevelTxt3_1);
        echo.addMessage(objectiveLevelTxt3_2);
    }

    protected override void levelEchoMsgChecks()
    {
        if (waterInPool1 >= waterNeededInPool1 && waterInPool2 >= waterNeededInPool2 && !objectiveLevelTxt3_4.maxTextShowsReached() && !echo.containsMessage(objectiveLevelTxt3_4))
        {
            echo.addMessage(objectiveLevelTxt3_4);
        }
    }
}
