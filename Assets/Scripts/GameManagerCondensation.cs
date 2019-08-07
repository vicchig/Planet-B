using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class GameManagerCondensation : GameManager, ILevelManagerCondensation
{
    public int condensedVapourNeeded;

    public GameObject condensationCloudParent;
    public GameObject precipitationParent;

    protected int condensedVapourAmnt;
    protected List<GameObject> waterVapours;

    protected override void Start()
    {
        base.Start();

        condensedVapourAmnt = 0;
        nextLevelMarker.SetActive(false);

        initWaterVapours();

        //pause the precipitation particle systems
        for (int i = 0; i < precipitationParent.transform.childCount; i++)
        {
            precipitationParent.transform.GetChild(i).GetComponent<ParticleSystem>().Pause();
        }
    }

    protected override void Update()
    {
        base.Update();

        //enable next level marker adn precipitation
        if (condensedVapourAmnt >= condensedVapourNeeded)
        {
            nextLevelMarker.SetActive(true);
            for (int i = 0; i < precipitationParent.transform.childCount; i++)
            {
                precipitationParent.transform.GetChild(i).GetComponent<ParticleSystem>().Play();

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
            objectiveDisplay.text = "Current Objective: \nFind a way to condense the water vapour (steaming orbs) in order to start precipitation.";
        }
        else
        {
            objectiveDisplay.text = "Current Objective: \nGet to the blue marker at the top to proceed";
            dirArrow.GetComponent<DirectionArrowController>().levelCompleted = true;

        }
    }

    private void initWaterVapours()
    {
        GameObject waterVapourParent = GameObject.Find("WaterVapourParent");
        waterVapours = new List<GameObject>();
        for (int i = 0; i < waterVapourParent.transform.childCount; i++)
        {
            waterVapours.Add(waterVapourParent.transform.GetChild(i).gameObject);
        }

    }

    protected override void checkEchoCollisions()
    {

    }


    public void ChangeCloudColour()
    {
        for (int i = 0; i < condensationCloudParent.transform.childCount; i++)
        {
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
}
