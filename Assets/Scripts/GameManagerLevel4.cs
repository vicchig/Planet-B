using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel4 : GameManager
{

    [Header("Level 1 Variables")]
    public int transpirationAmntNeeded;
    
    private int transpirationAmnt;

    protected override void Start()
    {
        base.Start();
        transpirationAmnt = 0;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void changeObjectives()
    {
    }

    protected override void checkEchoCollisions()
    {
    }

    protected override void levelEchoMsgChecks()
    {
    }

    public int getTranspirationAmnt() {
        return transpirationAmnt;
    }

    public void setTranspirationAmnt(int amnt) {
        transpirationAmnt = amnt;
    }
}
