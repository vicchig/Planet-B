using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerL5 : UIManager
{
    private ProgressBar waterInPoolBar;
    private ProgressBar evaporationBar;
    private ProgressBar condensationBar;
    private Text waterCount;
    private Text treeCount;

    private GameManagerLevel5 manager;
    private TreePlantingController treePlanter;

    protected override void Start()
    {
        base.Start();

        manager = managerObj.GetComponent<GameManagerLevel5>();
        treePlanter = GameObject.Find("Player3").GetComponent<TreePlantingController>();

        waterInPoolBar = this.transform.GetChild(3).GetComponent<ProgressBar>();
        evaporationBar = this.transform.GetChild(7).GetComponent<ProgressBar>();
        condensationBar = this.transform.GetChild(4).GetComponent<ProgressBar>();

        waterCount = this.transform.GetChild(1).GetComponent<Text>();
        treeCount = this.transform.GetChild(6).GetComponent<Text>();

        setInitialBarValues(evaporationBar, manager.GetEvaporatedWater(), manager.GetEvaporationNeeded());
        setInitialBarValues(condensationBar, manager.GetCondensedVapour(), manager.GetCondensedVapourNeeded());
        setInitialBarValues(waterInPoolBar, manager.GetWaterInPool(), manager.GetWaterNeededInPool());

        waterCount.text = ": " + playerAttributes.GetCurrentWater();
        treeCount.text = ": " + treePlanter.treeAmount;
    }

    protected override void Update()
    {
        base.Update();

        evaporationBar.BarValue = manager.GetEvaporatedWater();
        condensationBar.BarValue = manager.GetCondensedVapour();
        waterInPoolBar.BarValue = manager.GetWaterInPool();

        waterCount.text = ": " + playerAttributes.GetCurrentWater();
        treeCount.text = ": " + treePlanter.treeAmount;
    }
}
