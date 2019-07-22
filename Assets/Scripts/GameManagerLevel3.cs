using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManagerLevel3 : GameManagerCollection
{
    [Header("Level 3 Echo Dialogue")]
    public AudioClip objectiveLevelClip3_1;
    public AudioClip objectiveLevelClip3_2;
    public AudioClip objectiveLevelClip3_3;
    public AudioClip objectiveLevelClip3_4;

    EchoMessage objectiveLevelTxt3_1;
    EchoMessage objectiveLevelTxt3_2;
    EchoMessage objectiveLevelTxt3_3;
    EchoMessage objectiveLevelTxt3_4;
    EchoMessage frozenBlockHintTxt;
    
    protected override void Start()
    {
        base.Start();

        objectiveLevelTxt3_1 = new EchoMessage("Now we have to do collection. Collection is when precipitation falls down onto the ground and fills various bodies of water on the surface.", objectiveLevelClip3_1, 1);
        objectiveLevelTxt3_2 = new EchoMessage("Some of the water falls into already existing surface bodies of water. And some of it goes into the groundwater or freezes on top of mountains.", objectiveLevelClip3_2, 1);
        objectiveLevelTxt3_3 = new EchoMessage("If this water does not make it back into surface bodies of water, it cannot evaporate and rejoin the water cycle. Our goal is to now use groundwater and the frozen water on top of the mountain to the west to fill the two water pools in this area.", objectiveLevelClip3_3, 1);
        objectiveLevelTxt3_4 = new EchoMessage("We have collected enough water here. But other areas are still empty. Head to the marker on the right side to proceed.", objectiveLevelClip3_4, 1);

        echo.addMessage(objectiveLevelTxt3_1);
        echo.addMessage(objectiveLevelTxt3_2);
        echo.addMessage(objectiveLevelTxt3_3);
    }

    protected override void levelEchoMsgChecks()
    {
        if (waterInPool1 >= waterNeededInPool1 && waterInPool2 >= waterNeededInPool2 && !objectiveLevelTxt3_4.maxTextShowsReached() && !echo.containsMessage(objectiveLevelTxt3_4)) {
            echo.addMessage(objectiveLevelTxt3_4);
        }
    }

    
}
