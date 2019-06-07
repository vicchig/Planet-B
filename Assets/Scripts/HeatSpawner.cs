using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSpawner : MonoBehaviour
{
    public SunRay sunRay;
    public float timer = 0.5f;
    public float timeInterval = 0.75f;
    float timeRemaining = -1f;
    // Start is called before the first frame update
    void Start()
    {
        
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
        ray.transform.position = gameObject.transform.position;
        ray.transform.eulerAngles = new Vector3(0f, 0f, (float)angle);
    }
}
