using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerL4 : UIManager
{

    private ProgressBar transpirationBar;
    private GameManagerLevel4 manager;
    private TreePlantingController treePlanter;
    private Text treeCountText;
    protected override void Start()
    {
        base.Start();

        manager = managerObj.GetComponent<GameManagerLevel4>();

        transpirationBar = transform.GetChild(7).GetComponent<ProgressBar>();
        setInitialBarValues(transpirationBar, 0, manager.transpirationAmntNeeded);

        treePlanter = GameObject.Find("Player3").GetComponent<TreePlantingController>();
        treeCountText = this.transform.GetChild(8).GetComponent<Text>();
        treeCountText.text = ":" + treePlanter.treeAmount;

    }

    protected override void Update()
    {
        base.Update();

        transpirationBar.BarValue = manager.getTranspirationAmnt();
        treeCountText.text = ":" + treePlanter.treeAmount;
    }
}
