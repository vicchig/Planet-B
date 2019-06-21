using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject player;
    public GameObject managerObj;

    private PlayerAttributes playerAttributes;
    private GameManagerLevel1 manager;

    private ProgressBar healthBar;
    private ProgressBar heatBar;
    private ProgressBar waterInPoolBar;

    void Start()
    {
        playerAttributes = player.GetComponent<PlayerAttributes>();
        manager = managerObj.GetComponent<GameManagerLevel1>();

        healthBar = this.transform.GetChild(0).GetComponent<ProgressBar>();
        setInitialBarValues(healthBar, playerAttributes.maxHealth, playerAttributes.maxHealth);

        //WaterCount - child 2
        this.transform.GetChild(2).GetComponent<Text>().text = ":" + playerAttributes.waterCollected;

        heatBar = this.transform.GetChild(3).GetComponent<ProgressBar>();
        setInitialBarValues(heatBar, playerAttributes.getCurrentHeat(), playerAttributes.maxHeat);

        if (SceneManager.GetActiveScene().name == "Level 1") {
            waterInPoolBar = this.transform.GetChild(4).GetComponent<ProgressBar>();
            setInitialBarValues(waterInPoolBar, manager.getAmountOfWaterInPool(), manager.waterNeededInPool);
        }
        
    }

    private void Update()
    {
        healthBar.BarValue = playerAttributes.GetCurrentHealth();
        this.transform.GetChild(2).GetComponent<Text>().text = ":" + playerAttributes.GetCurrentWater();
        heatBar.BarValue = playerAttributes.getCurrentHeat();
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            waterInPoolBar.BarValue = manager.getAmountOfWaterInPool();

        }
    }

    private void setInitialBarValues(ProgressBar bar, int curr, int max) {
        bar.maxValue = max;
        bar.BarValue = curr;
    }
}
