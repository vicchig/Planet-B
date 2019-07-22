using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel4 : GameManagerTranspiration
{

    [Header("Level 4 Echo Dialogue")]
    public AudioClip objectiveClip4_1;
    public AudioClip objectiveClip4_1_2;
    public AudioClip objectiveClip4_2;
    public AudioClip objectiveClip4_3;
    public AudioClip objectiveClip4_4;



    EchoMessage objectiveLevelTxt4_1;
    EchoMessage objectiveLevelTxt4_1_2;
    EchoMessage objectiveLevelTxt4_2;
    EchoMessage objectiveLevelTxt4_3;
    EchoMessage objectiveLevelTxt4_4;

    protected override void Start()
    {
        base.Start();

        objectiveLevelTxt4_1 = new EchoMessage("Surface bodies of water are not the only sources of evaporation. Transpiration is another form of evaporation that occurs when moisture evaporates from the surface of plants on the ground.", objectiveClip4_1, 1);
        objectiveLevelTxt4_1_2 = new EchoMessage("On planets with large plant populations it can contribute a lot to the evaporation stage of the water cycle.", objectiveClip4_1_2, 1);
        objectiveLevelTxt4_2 = new EchoMessage("We brought some plants with us that we hope will not only thrive on this planet, but also contribute to transpiration. You can plant these plants using E.", objectiveClip4_2, 1);
        objectiveLevelTxt4_3 = new EchoMessage("We need to find the proper area for them to transpirate. Putting them somewhere too cold or too dry will not help us.", objectiveClip4_3, 1);
        objectiveLevelTxt4_4 = new EchoMessage("Nice! We have successfully started the process of transpiration on the planet, but we will need to evaporate some more water before condensation can occur in sufficient amounts. Head to the marker on the far right side to proceede.", objectiveClip4_4, 1);

        echo.addMessage(objectiveLevelTxt4_1);
        echo.addMessage(objectiveLevelTxt4_2);
        echo.addMessage(objectiveLevelTxt4_3);
    }

    protected override void levelEchoMsgChecks()
    {
        if (transpirationAmnt >= transpirationAmntNeeded && !echo.containsMessage(objectiveLevelTxt4_4) && !objectiveLevelTxt4_4.maxTextShowsReached())
        {
            echo.addMessage(objectiveLevelTxt4_4);

        }
    }




}
