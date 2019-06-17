using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingSteamManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] steam;
    public GameObject manager;
    public int waterThreshold = 20;

    private GameManagerLevel1 managerScript;

    private void Start()
    {
        DisableSteam();
        managerScript = manager.GetComponent<GameManagerLevel1>();
    }

    private void Update()
    {
        if(managerScript.getAmountOfWaterInPool() < waterThreshold)
        {
            DisableSteam();
        }
    }

    public void EnableSteam()
    {
        for (int i = 0; i < steam.Length; i++)
        {
            ParticleSystem ps = steam[i].GetComponent<ParticleSystem>();
            ps.Play();
        }
    }
    public void DisableSteam()
    {
        for (int i = 0; i < steam.Length; i++)
        {
            ParticleSystem ps = steam[i].GetComponent<ParticleSystem>();
            ps.Stop();
        }
    }
}
