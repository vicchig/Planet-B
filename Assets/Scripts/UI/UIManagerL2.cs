using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerL2 : UIManager
{
    private GameManagerLevel2 manager;
    private ProgressBar condensationBar;

    protected override void Start()
    {
        base.Start();

        manager  = managerObj.GetComponent<GameManagerLevel2>();

        //condensation bar
        condensationBar = this.transform.GetChild(4).gameObject.GetComponent<ProgressBar>();
        setInitialBarValues(condensationBar, manager.getCondensedVapourAmnt(), manager.condensedVapourNeeded);


    }

    protected override void Update()
    {
        base.Update();
        condensationBar.BarValue = manager.getCondensedVapourAmnt();
    }
}
