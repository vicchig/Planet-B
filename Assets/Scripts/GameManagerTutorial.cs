using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTutorial : GameManager
{
    [Header("Tutorial Level Echo Dialogue")]
    public AudioClip introClip1;

    private EchoMessage introTxt;

    protected override void Start() {
        base.Start();

        introTxt = new EchoMessage("Hello, I am Echo, your suit's built-in A.I. Your mission is to terraform a planet's water cycle to make it inhabitable. We will now begin your training.", introClip1, 1);

        echo.addMessage(introTxt);
    }

    protected override void Update() {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void checkEchoCollisions()
    {

    }

    protected override void levelEchoMsgChecks()
    {
        
    }

    protected override void changeObjectives()
    {
        
    }

}
