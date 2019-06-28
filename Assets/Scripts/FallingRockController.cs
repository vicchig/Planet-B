using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockController : MonoBehaviour
{
    [Header("Attributes")]
    public int rockDamage = 10;

    private PlayerAttributes attributes;

    private void Start()
    {
        attributes = GameObject.Find("Player3").GetComponent<PlayerAttributes>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) {
            attributes.SetCurrentHealth(attributes.GetCurrentHealth() - rockDamage);
            attributes.setTakingDamage(true);
            Destroy(gameObject);
        }
        else if (collision.transform.CompareTag("Platforms") || collision.transform.CompareTag("MovingPlatform"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            attributes.setTakingDamage(false);
        }
    }
}
