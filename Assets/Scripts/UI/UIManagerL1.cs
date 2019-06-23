using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerL1 : UIManager
{
    private GameManagerLevel1 manager;

    protected override void Start()
    {
        base.Start();

        manager = managerObj.GetComponent<GameManagerLevel1>();

        //WaterCount - child 2
        this.transform.GetChild(1).GetComponent<Text>().text = ":" + playerAttributes.waterCollected;

        waterInPoolBar = this.transform.GetChild(3).GetComponent<ProgressBar>();
        setInitialBarValues(waterInPoolBar, manager.getAmountOfWaterInPool(), manager.waterNeededInPool);

    }


    protected override void Update()
    {
        base.Update();

        waterInPoolBar.BarValue = manager.getAmountOfWaterInPool();

    }
}
