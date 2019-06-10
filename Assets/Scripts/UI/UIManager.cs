using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject player;

    private PlayerAttributes playerAttributes;

    void Start()
    {
        playerAttributes = player.GetComponent<PlayerAttributes>();
        //HealthBar - child 0
        this.transform.GetChild(0).GetComponent<ProgressBar>().maxValue = playerAttributes.maxHealth;
        this.transform.GetChild(0).GetComponent<ProgressBar>().BarValue = playerAttributes.maxHealth;
        //AirBar - child 1
        this.transform.GetChild(1).GetComponent<ProgressBar>().maxValue = playerAttributes.maxAir;
        this.transform.GetChild(1).GetComponent<ProgressBar>().BarValue = playerAttributes.maxAir;

        //WaterCount - child 2
        this.transform.GetChild(2).GetComponent<Text>().text = ":" + playerAttributes.waterCollected;

        //HeatBar - child 3
        this.transform.GetChild(3).GetComponent<ProgressBar>().maxValue = playerAttributes.maxHeat;
        this.transform.GetChild(3).GetComponent<ProgressBar>().BarValue = playerAttributes.getCurrentHeat();
    }


    public void setHealth(int newHealth) {
        this.transform.GetChild(0).GetComponent<ProgressBar>().BarValue = newHealth;
    }

    public void setAir(int newAir) {
        this.transform.GetChild(1).GetComponent<ProgressBar>().BarValue = newAir;
    }

    public void setWaterCount(int newWaterCount) {
        this.transform.GetChild(2).GetComponent<Text>().text = ":" + newWaterCount;
    }

    public void setHeat(int newHeat)
    {
        this.transform.GetChild(3).GetComponent<ProgressBar>().BarValue = newHeat;
    }

}
