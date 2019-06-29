using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel5 : GameManager, ILevelManagerCondensation, ILevelManagerTranspiration, ILevelManagerWater
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void levelEchoMsgChecks()
    {
        throw new System.NotImplementedException();
    }

    protected override void checkEchoCollisions()
    {
        throw new System.NotImplementedException();
    }

    protected override void changeObjectives()
    {
        throw new System.NotImplementedException();
    }

    public void SetCondensedVapour(int amount)
    {
        throw new System.NotImplementedException();
    }

    public int GetCondensedVapour()
    {
        throw new System.NotImplementedException();
    }

    public void SetTranspirationAmnt(int amount)
    {
        throw new System.NotImplementedException();
    }

    public int GetTranspirationAmountNeeded()
    {
        throw new System.NotImplementedException();
    }

    public int GetTranspirationAmount()
    {
        throw new System.NotImplementedException();
    }

    public void SetWaterInPool(int amount)
    {
        throw new System.NotImplementedException();
    }

    public void SetEvaporatedWater(int amount)
    {
        throw new System.NotImplementedException();
    }

    public int GetWaterInPool()
    {
        throw new System.NotImplementedException();
    }

    public int GetEvaporatedWater()
    {
        throw new System.NotImplementedException();
    }

    public int GetWaterNeededInPool()
    {
        throw new System.NotImplementedException();
    }

    public int GetCondensedVapourNeeded()
    {
        throw new System.NotImplementedException();
    }
}
