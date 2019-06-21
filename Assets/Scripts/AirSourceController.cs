using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSourceController : MonoBehaviour
{
    [Header("Attributes")]
    public int addedAir = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            Transform player = GameObject.Find("Player2").transform;
            //player.GetComponent<PlayerAttributes>().SetCurrentAir(player.GetComponent<PlayerAttributes>().GetCurrentAir() + addedAir);

            AudioManager.playAirSourcePop();

            Destroy(gameObject);
        }
    }
}
