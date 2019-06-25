using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel3 : GameManager
{
    //[Header("Level 1 Echo Dialogue")]

    [Header("Level 1 Variables")]
    public int waterNeededInPool1;
    public int waterNeededInPool2;

    //[Header("Level Objects")]


    private int waterInPool1;
    private int waterInPool2;

    protected override void Start()
    {
        base.Start();

        waterInPool1 = 0;
        waterInPool2 = 0;
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

    public int getWaterNeededInPool1() {
        return waterNeededInPool1;
    }

    public int getWaterNeededInPool2()
    {
        return waterNeededInPool2;
    }

    public int getWaterInPool1() {
        return waterInPool1;
    }

    public int getWaterInPool2()
    {
        return waterInPool2;
    }

    public void setWaterInPool1(int waterAmnt) {
        waterInPool1 = waterAmnt;
    }

    public void setWaterInPool2(int waterAmnt)
    {
        waterInPool2 = waterAmnt;
    }
}
