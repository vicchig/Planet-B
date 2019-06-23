using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Inherited")]
    public GameObject player;
    public GameObject managerObj;

    protected PlayerAttributes playerAttributes;

    protected ProgressBar healthBar;
    protected ProgressBar heatBar;
    protected ProgressBar waterInPoolBar;

    protected virtual void Start()
    {
        playerAttributes = player.GetComponent<PlayerAttributes>();

        healthBar = this.transform.GetChild(0).GetComponent<ProgressBar>();
        setInitialBarValues(healthBar, playerAttributes.maxHealth, playerAttributes.maxHealth);

        heatBar = this.transform.GetChild(2).GetComponent<ProgressBar>();
        setInitialBarValues(heatBar, playerAttributes.getCurrentHeat(), playerAttributes.maxHeat);
    }

    protected virtual void Update()
    {
        healthBar.BarValue = playerAttributes.GetCurrentHealth();
        heatBar.BarValue = playerAttributes.getCurrentHeat();
    }

    protected void setInitialBarValues(ProgressBar bar, int curr, int max) {
        bar.maxValue = max;
        bar.BarValue = curr;
    }
}
