using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTutorial : GameManager
{
    [Header("Tutorial Level Echo Dialogue")]
    public AudioClip introClip1;
    public AudioClip introClip2;
    public AudioClip hudExplanationClip;
    public AudioClip healthRegenExplanationClip;

    private EchoMessage introTxt;
    private EchoMessage introTxt2;
    private EchoMessage hudExplanationTxt;
    private EchoMessage healthRegenTxt;

    new protected void Start() {
        base.Start();

        introTxt = new EchoMessage("Hello, I am Echo, your suit's built-in A.I. I hope we can get along, if not, please remember that I control your suit's life support systems.", introClip1, 1);
        introTxt2 = new EchoMessage("Your mission is to terraform a planet's water cycle so that it may become inhabitable. Apparently, your species is dying out and needs another planet to live on. This suit and I were built for the purpose of helping you achieve this goal. We will now begin your training.", introClip2, 1);
        hudExplanationTxt = new EchoMessage("Your suit has a built-in display at the top left of your screen. It tells you useful information about your resources and life support system state.", hudExplanationClip, 1); ;
        healthRegenTxt = new EchoMessage("The planet we are going to will have many environmental hazards. If your health reaches critical levels, the suit will passively regenerate your health.", healthRegenExplanationClip, 1);

        echo.addMessage(introTxt);
        echo.addMessage(introTxt2);
        echo.addMessage(hudExplanationTxt);
    }

    new protected void Update() {
        base.Update();
        checkEchoCollisions();
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
