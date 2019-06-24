﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel2 : GameManager
{
    public int condensedVapourNeeded;
    public GameObject condensationCloudParent;

    public AudioClip objectiveLevelClip2_1;
    public AudioClip objectiveLevelClip2_2;
    public AudioClip objectiveLevelClip2_3;
    public AudioClip poisonPlatformCommentClip;

    private EchoMessage objectiveLevelTxt2_1;
    private EchoMessage objectiveLevelTxt2_2;
    private EchoMessage objectiveLevelTxt2_3;
    private EchoMessage poisonPlatformCommentTxt;

    private int condensedVapourAmnt;
    private List<GameObject> waterVapours;

    protected override void Start()
    {
        base.Start();

        condensedVapourAmnt = 0;
        nextLevelMarker.SetActive(false);

        initWaterVapours();

        objectiveLevelTxt2_1 = new EchoMessage("As water vapour rises through the atmosphere it cools down and condenses back into water. When enough vapour condenses, it falls back to the ground as precipitation. Our goal now is to condense enough water vapour.", objectiveLevelClip2_1, 1);
        objectiveLevelTxt2_2 = new EchoMessage("Congratulations! We have successfully completed the second step in the water cycle -- precipitation. Without it, there would be no way for the water to get back from the atmosphere to the ground. ", objectiveLevelClip2_2, 1);
        objectiveLevelTxt2_3 = new EchoMessage("Make your way to the marker on the topmost platform to proceede.", objectiveLevelClip2_3, 1);
        poisonPlatformCommentTxt = new EchoMessage("I would not stay on these for too long.", poisonPlatformCommentClip, 1);

        echo.addMessage(objectiveLevelTxt2_1);
    }

    protected override void Update()
    {
        base.Update();

        playerInstantDeath();
        checkWaterVapourCollisions();

        //enable next level marker
        if (condensedVapourAmnt >= condensedVapourNeeded) {
            nextLevelMarker.SetActive(true);
            if (!echo.containsMessage(objectiveLevelTxt2_2) && !echo.containsMessage(objectiveLevelTxt2_3) && !objectiveLevelTxt2_2.maxTextShowsReached() && !objectiveLevelTxt2_3.maxTextShowsReached()) {
                echo.addMessage(objectiveLevelTxt2_2);
                echo.addMessage(objectiveLevelTxt2_3);
            }
        }
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

                    changeCloudColour();
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

    private void changeCloudColour() {
        for (int i = 0; i < condensationCloudParent.transform.childCount; i++) {
            condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.r * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.g * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.b * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.a);
        }
    }
}
