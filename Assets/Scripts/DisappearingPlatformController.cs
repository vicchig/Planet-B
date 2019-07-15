using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatformController : MonoBehaviour
{
    [Header("Platform Attributes")]
    public float disappearTime;
    public float disappearSpeed;
    public bool disappearing;

    private float disappearTimer;
    private int activePlatforms;
    
    private void Start()
    {
        disappearTimer = disappearTime;
        activePlatforms = 3;
    }

    private void Update()
    {
        if (activePlatforms <= 0)
        {
            disappearing = false;
        }
        else {
            if (disappearing)
            {
                this.transform.GetChild(activePlatforms - 1).gameObject.GetComponent<SpriteRenderer>().color = new Color(this.transform.GetChild(activePlatforms - 1).gameObject.GetComponent<SpriteRenderer>().color.r, this.transform.GetChild(activePlatforms - 1).gameObject.GetComponent<SpriteRenderer>().color.g, this.transform.GetChild(activePlatforms - 1).gameObject.GetComponent<SpriteRenderer>().color.b, this.transform.GetChild(activePlatforms - 1).gameObject.GetComponent<SpriteRenderer>().color.a * (disappearTimer / disappearTime));

                if (disappearTimer <= 0) {
                    disappearTimer = disappearTime;
                    activePlatforms--;
                    this.transform.GetChild(activePlatforms).gameObject.SetActive(false);
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (disappearing && activePlatforms > 0) {
            disappearTimer -= disappearSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            disappearing = true;
        }
    }
}
