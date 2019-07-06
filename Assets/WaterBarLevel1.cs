using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBarLevel1 : MonoBehaviour
{
    // Start is called before the first frame update
    private ProgressBar pb;
    private PlayerAttributes pa;

    public int maxVal = 14;
    void Start()
    {
        pb = GetComponent<ProgressBar>();
        pb.maxValue = maxVal;
        pb.BarValue = 0;

        pa = transform.parent.parent.gameObject.GetComponent<PlayerAttributes>();
    }

    // Update is called once per frame
    void Update()
    {
        pb.BarValue = pa.waterCollected;
    }
}
