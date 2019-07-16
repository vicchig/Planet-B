using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel2 : GameManager, ILevelManagerCondensation
{
    [Header("Level 2 Echo Dialogue")]
    public AudioClip objectiveLevelClip2_1;
    public AudioClip objectiveLevelClip2_1_2;
    public AudioClip objectiveLevelClip2_2;
    public AudioClip objectiveLevelClip2_3;
    public AudioClip poisonPlatformCommentClip;

    [Header("Level 2 Variables")]
    public int condensedVapourNeeded;

    [Header("Level 2 Objects")]
    public GameObject condensationCloudParent;
    public GameObject rainParent;
    

    private EchoMessage objectiveLevelTxt2_1;
    private EchoMessage objectiveLevelTxt2_1_2;
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
        objectiveLevelTxt2_1 = new EchoMessage("This is two stages combined into one. First the condensation stage of the cycle occurs and then the precipitation stage. Although they are closely related, they are considered to be separate stages.", objectiveLevelClip2_1_2, 1);
        objectiveLevelTxt2_2 = new EchoMessage("Congratulations! We have successfully completed the second step in the water cycle -- precipitation. Without it, there would be no way for the water to get back from the atmosphere to the ground. ", objectiveLevelClip2_2, 1);
        objectiveLevelTxt2_3 = new EchoMessage("Make your way to the marker on the topmost platform to proceed.", objectiveLevelClip2_3, 1);
        poisonPlatformCommentTxt = new EchoMessage("I would not stay on these for too long.", poisonPlatformCommentClip, 1);

        echo.addMessage(objectiveLevelTxt2_1);

        for (int i = 0; i < rainParent.transform.childCount; i++) {
            rainParent.transform.GetChild(i).GetComponent<ParticleSystem>().Pause();
        }
    }

    protected override void Update()
    {
        base.Update();

        //playerInstantDeath();
        checkWaterVapourCollisions();

        //enable next level marker
        if (condensedVapourAmnt >= condensedVapourNeeded) {
            nextLevelMarker.SetActive(true);
            AudioManager.playRain();
            for (int i = 0; i < rainParent.transform.childCount; i++)
            {
                rainParent.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
                
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void changeObjectives()
    {
        if (condensedVapourAmnt < condensedVapourNeeded)
        {
            objectiveDisplay.text = "Current Objective: \nFind a way to condense the water vapour (steaming orbs) and fill the condensation bar.";
        }
        else {
            objectiveDisplay.text = "Current Objective: \nGet to the blue marker at the top to proceed";
        }
    }

    protected override void checkEchoCollisions()
    {
        for (int i = 0; i < echoColliders.Count; i++)
        {
            if (echoColliders[i] != null)
            {
                if (echoColliders[i].tag == "PoisonousCloud" && !echo.containsMessage(poisonPlatformCommentTxt) && !poisonPlatformCommentTxt.maxTextShowsReached() && !echo.isBusy())
                {
                    echo.addMessage(poisonPlatformCommentTxt);
                }
                
            }
        }
    }

    protected override void levelEchoMsgChecks()
    {
        if (condensedVapourAmnt >= condensedVapourNeeded)
        {
            if (!echo.containsMessage(objectiveLevelTxt2_2) && !echo.containsMessage(objectiveLevelTxt2_3) && !objectiveLevelTxt2_2.maxTextShowsReached() && !objectiveLevelTxt2_3.maxTextShowsReached())
            {
                echo.addMessage(objectiveLevelTxt2_2);
                echo.addMessage(objectiveLevelTxt2_3);
            }
        }
    }

    private void checkWaterVapourCollisions() {
        /*
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
        }*/

    }

    private void initWaterVapours() {
        GameObject waterVapourParent = GameObject.Find("WaterVapourParent");
        waterVapours = new List<GameObject>();
        for (int i = 0; i < waterVapourParent.transform.childCount; i++)
        {
            waterVapours.Add(waterVapourParent.transform.GetChild(i).gameObject);
        }

    }

    public void ChangeCloudColour() {
        for (int i = 0; i < condensationCloudParent.transform.childCount; i++) {
            condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.r * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.g * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.b * 0.9f, condensationCloudParent.transform.GetChild(i).GetComponent<SpriteRenderer>().color.a);
        }
    }

    public void SetCondensedVapour(int amount)
    {
        condensedVapourAmnt = amount;
    }

    public int GetCondensedVapour()
    {
        return condensedVapourAmnt;
    }

    public int GetCondensedVapourNeeded()
    {
        return condensedVapourNeeded;
    }



    /*
    public int getCondensedVapourAmnt() {
        return condensedVapourAmnt;
    }

    public void setCondensedVapourAmnt(int newAmount) {
        condensedVapourAmnt = newAmount;
    }*/
}
