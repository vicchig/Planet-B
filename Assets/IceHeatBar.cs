using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceHeatBar : MonoBehaviour
{
    // Start is called before the first frame update
    private ProgressBar pb;
    private Level3DynamicParticleScript dParticleL3;

    //public int maxVal = 14;

    private float lastFrameWater = 0f;

    public float timer = 1f;
    private float timeRemaining = 0f;

    void Start()
    {
        pb = transform.GetChild(0).GetComponent<ProgressBar>();

        dParticleL3 = transform.parent.gameObject.GetComponent<Level3DynamicParticleScript>();

        pb.maxValue = (int)dParticleL3.heatEnergyThreshold;
        pb.BarValue = (int)dParticleL3.heatEnergyThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        if (dParticleL3.heatEnergyThreshold != lastFrameWater)
        {
            timeRemaining = timer;
            lastFrameWater = dParticleL3.heatEnergyThreshold;
        }
        if (timeRemaining >= 0)
        {
            pb.gameObject.SetActive(true);
        }
        //if (timeRemaining < 0)
        //{
        //    pb.gameObject.SetActive(false);
        //}
        pb.BarValue = dParticleL3.heatEnergyThreshold;

        timeRemaining -= Time.deltaTime;

        if (pb.BarValue <= 0)
        {
            pb.gameObject.SetActive(false);
        }

    }
}
