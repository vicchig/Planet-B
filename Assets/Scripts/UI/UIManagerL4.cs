using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerL4 : UIManager
{

    private ProgressBar transpirationBar;
    private GameManagerLevel4 manager;

    protected override void Start()
    {
        base.Start();

        manager = managerObj.GetComponent<GameManagerLevel4>();

        transpirationBar = transform.GetChild(7).GetComponent<ProgressBar>();
        setInitialBarValues(transpirationBar, 0, manager.transpirationAmntNeeded);
    }

    protected override void Update()
    {
        base.Update();

        transpirationBar.BarValue = manager.getTranspirationAmnt();
    }
}
