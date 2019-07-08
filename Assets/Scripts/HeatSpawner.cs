using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSpawner : MonoBehaviour
{
    public SunRay sunRay;
    public float timer = 0.5f;
    public float timeInterval = 0.75f;
    public bool targetWater;
    public bool targetIce;
    public GameObject waterTarget;
    public float lifeTime  = 5f;
    private GameObject[] iceTargets;
    float timeRemaining = -1f;

    private bool findIceTargets;
    private GameObject tempIceParent;
    // Start is called before the first frame update
    void Start()
    {
        tempIceParent = GameObject.Find("IceParent");
        findIceTargets = true;
        
    }

    // Update is called once per frame
    void Update()
    {
            
        

        if (timeRemaining < 0f)
        {
            for (int i = 75; i < 100; i += 6)
            {
                Spawn(i);
            }

            timeRemaining = timer;
        }
        timeRemaining -= Time.deltaTime;
    }
    void Spawn(int angle)
    {
        SunRay ray = Instantiate(sunRay);
        ray.targetIce = targetIce;
        ray.targetWater = targetWater;
        ray.waterTarget = waterTarget;

        for (int i = 0; i < tempIceParent.transform.childCount; i++)
        {
            ray.iceTargets.Add(tempIceParent.transform.GetChild(i).gameObject);
        }
        
        

        ray.transform.position = gameObject.transform.position;
        ray.transform.eulerAngles = new Vector3(0f, 0f, (float)angle);
    }
}
