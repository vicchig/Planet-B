using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTutorial : GameManager
{
    [Header("Tutorial Level Echo Dialogue")]
    public AudioClip introClip1;
    public AudioClip hudExplanationClip;
    public AudioClip healthRegenExplanationClip;

    private EchoMessage introTxt;
    private EchoMessage hudExplanationTxt;
    private EchoMessage healthRegenTxt;

    new protected void Start() {
        base.Start();

        introTxt = new EchoMessage("Hello, I am Echo, your suit's built-in A.I. Your mission is to terraform a planet's water cycle to make it inhabitable. We will now begin your training.", introClip1, 1);
        hudExplanationTxt = new EchoMessage("You can see your current state and resources in the top left corner.", hudExplanationClip, 1); ;
        healthRegenTxt = new EchoMessage("Your suit will passively regenerate health.", healthRegenExplanationClip, 1);

        echo.addMessage(introTxt);
        echo.addMessage(hudExplanationTxt);
    }

    new protected void Update() {
        base.Update();
        checkEchoCollisions();
    }

    new protected void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void checkEchoCollisions()
    {
        for (int i = 0; i < echoColliders.Count; i++) {
            if (echoColliders[i].tag == "Lava" && !healthRegenTxt.maxTextShowsReached())
            {
                echo.addMessage(healthRegenTxt);
            }
        }
    }

    protected override void levelObjectiveChecks()
    {
        throw new System.NotImplementedException();
    }
}
