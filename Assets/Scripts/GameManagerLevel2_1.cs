using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class GameManagerLevel2_1 : GameManagerCondensation
{
    [Header("Level Echo Dialogue")]
    public AudioClip objectiveLevelClip2_1;
    public AudioClip objectiveLevelClip2_2;
    public AudioClip objectiveLevelClip2_3;

    private EchoMessage objectiveLevelTxt2_1;
    private EchoMessage objectiveLevelTxt2_2;
    private EchoMessage objectiveLevelTxt2_3;

    protected override void Start()
    {
        base.Start();

        objectiveLevelTxt2_1 = new EchoMessage("Precipitation has many forms in addition to rain. It includes water in any state that falls down to the ground.", objectiveLevelClip2_1, 1);
        objectiveLevelTxt2_2 = new EchoMessage("During colder weather, precipitation can take on the form of snow.", objectiveLevelClip2_2, 1);
        objectiveLevelTxt2_3 = new EchoMessage("The snow falls onto the ground and melts during warmer weather, adding more water to be evaporated in the evaporation stage.", objectiveLevelClip2_3, 1);

        echo.addMessage(objectiveLevelTxt2_1);
        echo.addMessage(objectiveLevelTxt2_2);
    }

    protected override void Update()
    {
        base.Update();

        if (condensedVapourAmnt >= condensedVapourNeeded)
        {
            //AudioManager.playRain(); SHOULD BE SNOW

        }
    }

    protected override void levelEchoMsgChecks()
    {
        if (condensedVapourAmnt >= condensedVapourNeeded)
        {
            if (!echo.containsMessage(objectiveLevelTxt2_3) &&  !objectiveLevelTxt2_3.maxTextShowsReached())
            {
                echo.addMessage(objectiveLevelTxt2_3);
            }
        }
    }


}
