using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBarLevel1 : MonoBehaviour
{
    // Start is called before the first frame update
    private ProgressBar pb;
    private PlayerAttributes pa;

    public int maxVal = 14;

    private int lastFrameWater = 0;

    public float timer = 1f;
    private float timeRemaining = 0f;

    void Start()
    {
        pb = transform.GetChild(0).GetComponent<ProgressBar>();
        pb.maxValue = maxVal;
        pb.BarValue = 0;

        pa = transform.parent.gameObject.GetComponent<PlayerAttributes>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pa.waterCollected != lastFrameWater)
        {
            timeRemaining = timer;
            lastFrameWater = pa.waterCollected;
        }
        if (timeRemaining >= 0)
        {
            pb.gameObject.SetActive(true);
        }
        if (timeRemaining < 0)
        {
            pb.gameObject.SetActive(false);
        }
        pb.BarValue = pa.waterCollected;

        timeRemaining -= Time.deltaTime;

    }
}
