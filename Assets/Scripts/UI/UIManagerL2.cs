using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerL2 : UIManager
{
    private ILevelManagerCondensation manager;
    private ProgressBar condensationBar;

    protected override void Start()
    {
        base.Start();

        manager  = managerObj.GetComponent<ILevelManagerCondensation>();

        //condensation bar
        condensationBar = this.transform.GetChild(4).gameObject.GetComponent<ProgressBar>();
        setInitialBarValues(condensationBar, manager.GetCondensedVapour(), manager.GetCondensedVapourNeeded());


    }

    protected override void Update()
    {
        base.Update();
        condensationBar.BarValue = manager.GetCondensedVapour();
    }
}
