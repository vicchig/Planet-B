using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel2 : GameManager
{

    public int condensedVapourNeeded;

    private int condensedVapourAmnt;
    private List<GameObject> waterVapours;


    protected override void Start()
    {
        base.Start();

        condensedVapourAmnt = 0;

        initWaterVapours();
        
    }

    protected override void Update()
    {
        base.Update();

        playerInstantDeath();
        checkWaterVapourCollisions();
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

    private void checkWaterVapourCollisions() {
        List<Collider2D> colliders = new List<Collider2D>();
        CircleCollider2D col;

        ContactFilter2D filter = new ContactFilter2D
        {
            useTriggers = true
        };

        for (int i = 0; i < waterVapours.Count; i++) {
            col = waterVapours[i].GetComponent<CircleCollider2D>();
            col.OverlapCollider(filter, colliders);

            for (int j = 0; j < colliders.Count; j++) {
                if (colliders[j].tag == "CondensationArea") {
                    Destroy(waterVapours[i]);
                    waterVapours.RemoveAt(i);
                    condensedVapourAmnt++;
                }
            }
        }

    }

    private void initWaterVapours() {
        GameObject waterVapourParent = GameObject.Find("WaterVapourParent");
        waterVapours = new List<GameObject>();
        for (int i = 0; i < waterVapourParent.transform.childCount; i++)
        {
            waterVapours.Add(waterVapourParent.transform.GetChild(i).gameObject);
        }

    }
}
