using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel5 : GameManager, ILevelManagerCondensation, ILevelManagerTranspiration, ILevelManagerWater
{

    //[Header("Level 5 Echo Dialogue")]


    [Header("Level 5 Variables")]
    public int condensationNeeded = 5;
    public int evaporationNeeded = 75;
    public int transpirationNeeded = 25;
    public int waterNeededInPool = 5;

    [Header("Level 5 Objects")]
    public GameObject condensationCloudParent;

    private int condensedVapourAmount;
    private int evaporationAmount;
    private int transpirationAmount;
    private int waterInPool;
    private TreePlantingController treePlanter;

    protected override void Start()
    {
        base.Start();

        condensedVapourAmount = 0;
        evaporationAmount = 0;
        transpirationAmount = 0;
        waterInPool = 0;
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
       // throw new System.NotImplementedException();
    }

    protected override void checkEchoCollisions()
    {
       // throw new System.NotImplementedException();
    }

    protected override void changeObjectives()
    {
       // throw new System.NotImplementedException();
    }

    public void SetCondensedVapour(int amount)
    {
        condensedVapourAmount = amount;
    }

    public int GetCondensedVapour()
    {
        return condensedVapourAmount;
    }

    public void SetTranspirationAmnt(int amount)
    {
        transpirationAmount = amount;
    }

    public int GetTranspirationAmountNeeded()
    {
        return transpirationNeeded;
    }

    public int GetTranspirationAmount()
    {
        return transpirationAmount;
    }

    public void SetWaterInPool(int amount)
    {
        waterInPool = amount;
    }

    public void SetEvaporatedWater(int amount)
    {
        evaporationAmount = amount;
    }

    public int GetWaterInPool()
    {
        return waterInPool;
    }

    public int GetEvaporatedWater()
    {
        return evaporationAmount + transpirationAmount;
    }

    public int GetWaterNeededInPool()
    {
        return waterNeededInPool;
    }

    public int GetCondensedVapourNeeded()
    {
        return condensationNeeded;
    }

    public int GetEvaporationNeeded()
    {
        return evaporationNeeded;
    }

    public void ChangeCloudColour()
    {
        for (int i = 0; i < condensationCloudParent.transform.childCount; i++)
        {
            condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.r * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.g * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.b * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.a);
        }
    }
}
