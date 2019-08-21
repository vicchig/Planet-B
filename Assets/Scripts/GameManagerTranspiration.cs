using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManagerTranspiration : GameManager, ILevelManagerTranspiration
{
    [Header("Level 4 Variables")]
    public int transpirationAmntNeeded;

    protected int transpirationAmnt;

    protected override void Start()
    {
        base.Start();

        transpirationAmnt = 0;
    }

    protected override void Update()
    {
        base.Update();

        if (transpirationAmnt >= transpirationAmntNeeded)
        {
            nextLevelMarker.SetActive(true);
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }




    protected override void changeObjectives()
    {
        if (transpirationAmnt <= transpirationAmntNeeded) {
            objectiveDisplay.text = "Current Objective:\nFind a good area to plant the plants in to begin transpiration.\nUse E to plant.";
        }
        else if (transpirationAmnt > transpirationAmntNeeded) {
            objectiveDisplay.text = "Current Objective:\nLocate the marker on the right side to proceed.";
        }
    }

    protected override void checkEchoCollisions()
    {
        return;
    }

    protected override void levelEchoMsgChecks()
    {
        return;
    }


    public void SetTranspirationAmnt(int amount)
    {
        transpirationAmnt = amount;
    }

    public int GetTranspirationAmountNeeded()
    {
        return transpirationAmntNeeded;
    }

    public int GetTranspirationAmount()
    {
        return transpirationAmnt;
    }

}
