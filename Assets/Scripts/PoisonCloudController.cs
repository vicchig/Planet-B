using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloudController : MonoBehaviour
{
    public GameObject player;
    public float poisonDuration;
    public int poisonDmg;

    private PlayerAttributes attributes;
    private bool poisonActive;
    private float poisonTimer;

    private void Start()
    {
        attributes = player.GetComponent<PlayerAttributes>();
        poisonActive = false;
        poisonTimer = 0;
    }

    private void Update()
    {
        if (poisonActive)
        {
            if (poisonTimer >= poisonDuration)
            {
                attributes.SetCurrentHealth(attributes.GetCurrentHealth() - poisonDmg);
                poisonTimer = 0;
            }
        }
        else {
            poisonTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        if (poisonActive) {
            poisonTimer += Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player") {
            poisonActive = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            poisonActive = false;
        }
    }
}
