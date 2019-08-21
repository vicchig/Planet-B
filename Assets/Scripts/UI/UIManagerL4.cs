using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerL4 : UIManager
{ 
    private ProgressBar transpirationBar;
    private ILevelManagerTranspiration manager;
    private TreePlantingController treePlanter;
    private Text treeCountText;

    protected override void Start()
    {
        base.Start();

        manager = managerObj.GetComponent<ILevelManagerTranspiration>();

        transpirationBar = transform.GetChild(7).GetComponent<ProgressBar>();
        setInitialBarValues(transpirationBar, 0, manager.GetTranspirationAmountNeeded());

        treePlanter = GameObject.Find("Player3").GetComponent<TreePlantingController>();
        treeCountText = this.transform.GetChild(6).GetComponent<Text>();
        treeCountText.text = ":" + treePlanter.treeAmount;

    }

    protected override void Update()
    {
        base.Update();

        transpirationBar.BarValue = manager.GetTranspirationAmount();
        treeCountText.text = ":" + treePlanter.treeAmount;
    }
}
