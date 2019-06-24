using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel2 : GameManager
{

    public int condensedVapourNeeded;

    private int condensedVapourAmnt;
    


    protected override void Start()
    {
        base.Start();

        condensedVapourAmnt = 0;
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

    public int getCondensedVapourAmnt() {
        return condensedVapourAmnt;
    }

    public void setCondensedVapourAmnt(int newAmount) {
        condensedVapourAmnt = newAmount;
    }
}
