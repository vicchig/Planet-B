using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingSteamManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] steam;

    private void Start()
    {
        for (int i = 0; i < steam.Length; i++)
        {
            ParticleSystem ps = steam[i].GetComponent<ParticleSystem>();
            ps.Stop();
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
}
