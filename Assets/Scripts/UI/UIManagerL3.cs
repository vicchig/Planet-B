using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerL3 : UIManager
{

    private ProgressBar waterBar1;
    private ProgressBar waterBar2;
    private GameManagerLevel3 manager;


    protected override void Start()
    {
        base.Start();
        manager = managerObj.GetComponent<GameManagerLevel3>();

        waterBar1 = this.transform.GetChild(4).GetComponent<ProgressBar>();
        waterBar2 = this.transform.GetChild(6).GetComponent<ProgressBar>();

        setInitialBarValues(waterBar1, 0, manager.getWaterNeededInPool1());
        setInitialBarValues(waterBar2, 0, manager.getWaterNeededInPool2());

    }

    protected override void Update()
    {
        base.Update();

        waterBar1.BarValue = manager.getWaterInPool1();
        waterBar2.BarValue = manager.getWaterInPool2();
    }

    


}
