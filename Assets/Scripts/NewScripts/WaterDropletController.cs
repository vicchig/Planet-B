using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterDropletController : MonoBehaviour
{

    [Header("Attributes")]
    public int waterAddedOnPickUp = 1; //amount of water added on pick up

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            Transform player = GameObject.Find("Player2").transform;
            player.GetComponent<PlayerAttributes>().SetCurrentWater(player.GetComponent<PlayerAttributes>().GetCurrentWater() + waterAddedOnPickUp);
            AudioManager.playWaterDropPop();
            Destroy(gameObject);
        }
    }

}
