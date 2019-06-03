using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockController : MonoBehaviour
{
    [Header("Attributes")]
    public int rockDamage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) {
            Transform player = GameObject.Find("Player2").transform;
            player.GetComponent<PlayerAttributes>().SetCurrentHealth(player.GetComponent<PlayerAttributes>().GetCurrentHealth() - rockDamage);
            Debug.Log(player.GetComponent<PlayerAttributes>().GetCurrentHealth());
            Destroy(gameObject);
        }
        else if (collision.transform.CompareTag("Platforms"))
        {
            Destroy(gameObject);
        }
    }
}
