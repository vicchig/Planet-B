using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject player;
    public GameObject managerObj;

    private PlayerAttributes playerAttributes;
    private GameManagerLevel1 manager;

    private ProgressBar healthBar;
    private ProgressBar airBar;
    private ProgressBar heatBar;
    private ProgressBar waterInPoolBar;

    void Start()
    {
        playerAttributes = player.GetComponent<PlayerAttributes>();
        manager = managerObj.GetComponent<GameManagerLevel1>();

        healthBar = this.transform.GetChild(0).GetComponent<ProgressBar>();
        setInitialBarValues(healthBar, playerAttributes.maxHealth, playerAttributes.maxHealth);

        airBar = this.transform.GetChild(1).GetComponent<ProgressBar>();
        setInitialBarValues(airBar, playerAttributes.maxAir, playerAttributes.maxAir);

        //WaterCount - child 2
        this.transform.GetChild(2).GetComponent<Text>().text = ":" + playerAttributes.waterCollected;

        heatBar = this.transform.GetChild(3).GetComponent<ProgressBar>();
        setInitialBarValues(heatBar, playerAttributes.getCurrentHeat(), playerAttributes.maxHeat);

        waterInPoolBar = this.transform.GetChild(4).GetComponent<ProgressBar>();
        setInitialBarValues(waterInPoolBar, manager.getAmountOfWaterInPool(), manager.waterNeededInPool - 8);
    }

    private void Update()
    {
        healthBar.BarValue = playerAttributes.GetCurrentHealth();
        airBar.BarValue = playerAttributes.GetCurrentAir();
        this.transform.GetChild(2).GetComponent<Text>().text = ":" + playerAttributes.GetCurrentWater();
        heatBar.BarValue = playerAttributes.getCurrentHeat();
        waterInPoolBar.BarValue = manager.getAmountOfWaterInPool();
    }

    private void setInitialBarValues(ProgressBar bar, int curr, int max) {
        bar.maxValue = max;
        bar.BarValue = curr;
    }
}
