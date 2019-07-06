using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerL1 : UIManager
{
    private ILevelManagerWater manager;

    private ProgressBar waterInPoolBar;

    protected override void Start()
    {
        base.Start();

        manager = managerObj.GetComponent<ILevelManagerWater>();

        //WaterCount - child 2
        this.transform.GetChild(1).GetComponent<Text>().text = ":" + playerAttributes.waterCollected;

        waterInPoolBar = this.transform.GetChild(4).GetComponent<ProgressBar>();
        setInitialBarValues(waterInPoolBar, manager.GetWaterInPool(), manager.GetWaterNeededInPool());

    }


    protected override void Update()
    {
        base.Update();

        waterInPoolBar.BarValue = manager.GetWaterInPool();
        this.transform.GetChild(1).GetComponent<Text>().text = ":" + playerAttributes.GetCurrentWater();
    }
}
