using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    void Start()
    {
        //HealthBar - child 0
        this.transform.GetChild(0).GetComponent<ProgressBar>().maxValue = GameObject.Find("Player2").GetComponent<PlayerAttributes>().totalHealth;
        this.transform.GetChild(0).GetComponent<ProgressBar>().BarValue = GameObject.Find("Player2").GetComponent<PlayerAttributes>().totalHealth;
        //AirBar - child 1
        this.transform.GetChild(1).GetComponent<ProgressBar>().maxValue = GameObject.Find("Player2").GetComponent<PlayerAttributes>().totalAir;
        this.transform.GetChild(1).GetComponent<ProgressBar>().BarValue = GameObject.Find("Player2").GetComponent<PlayerAttributes>().totalAir;

        //WaterCount - child 2
        this.transform.GetChild(2).GetComponent<Text>().text = ":" + GameObject.Find("Player2").GetComponent<PlayerAttributes>().waterCollected;

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
    
}
