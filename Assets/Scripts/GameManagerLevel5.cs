using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel5 : GameManager, ILevelManagerCondensation, ILevelManagerTranspiration, ILevelManagerWater
{

    public GameObject condensationCloudParent;

    private int condensationNeeded;
    private int condensed;

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
        condensed = amount;
    }

    public int GetCondensedVapour()
    {
        return condensed;
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
        return condensationNeeded;
    }

    public int GetEvaporationNeeded()
    {
        throw new System.NotImplementedException();
    }

    public void ChangeCloudColour()
    {
        for (int i = 0; i < condensationCloudParent.transform.childCount; i++)
        {
            condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.r * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.g * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.b * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.a);
        }
    }
}
