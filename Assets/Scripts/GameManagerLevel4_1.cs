using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel4_1 : GameManagerTranspiration
{
    [Header("Level 4_1 Echo Dialogue")]
    public AudioClip objectiveClip4_1;
    public AudioClip objectiveClip4_1_2;
    public AudioClip objectiveClip4_3;
    public AudioClip objectiveClip4_4;



    EchoMessage objectiveLevelTxt4_1;
    EchoMessage objectiveLevelTxt4_1_2;
    EchoMessage objectiveLevelTxt4_3;
    EchoMessage objectiveLevelTxt4_4;

    protected override void Start()
    {
        base.Start();

        objectiveLevelTxt4_1 = new EchoMessage("Transpiration requires a lot of plants to be effective. We need to plant more trees in order to evaporate enough water.", objectiveClip4_1, 1);
        objectiveLevelTxt4_1_2 = new EchoMessage("Transpiration is the process of water evaporating off the surfaces of plants, it can be considered part of the evaporation stage, but with a special name.", objectiveClip4_1_2, 1);
        objectiveLevelTxt4_3 = new EchoMessage("We need to find the proper area to plant them in.", objectiveClip4_3, 1);
        objectiveLevelTxt4_4 = new EchoMessage("Now we have enough evaporation happening from transpiration! Head to the marker on the far right side to proceede.", objectiveClip4_4, 1);

        echo.addMessage(objectiveLevelTxt4_1);
        echo.addMessage(objectiveLevelTxt4_1_2);
        echo.addMessage(objectiveLevelTxt4_3);
    }

    protected override void levelEchoMsgChecks()
    {
        if (transpirationAmnt >= transpirationAmntNeeded && !echo.containsMessage(objectiveLevelTxt4_4) && !objectiveLevelTxt4_4.maxTextShowsReached())
        {
            dirArrow.GetComponent<DirectionArrowController>().levelCompleted = true;
            echo.addMessage(objectiveLevelTxt4_4);
        }
    }
}
