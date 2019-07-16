using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatformController : MonoBehaviour
{
    [Header("Platform Attributes")]
    public float disappearTime;
    public float disappearSpeed;
    public bool disappearing;

    public float startReapperingTime = 2.0f;
    private float startReappearing;

    public float reappearTime;
    public bool reappearing;

    private float disappearTimer;
    private float reappearTimer;

    private int activePlatforms;
    
    private void Start()
    {
        disappearTimer = disappearTime;
        activePlatforms = 3;
        reappearing = false;
        reappearTimer = 0;
        startReappearing = startReapperingTime;
    }

    private void Update()
    {
        if (activePlatforms <= 0)
        {
            disappearing = false;
            if (startReappearing <= 0)
            {
                reappearing = true;
            } else
            {
                startReappearing -= Time.deltaTime;
            }
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
        if (reappearing)
        {
            if (reappearTimer <= 0)
            {
                transform.GetChild(activePlatforms).gameObject.SetActive(true);
                transform.GetChild(activePlatforms).gameObject.GetComponent<SpriteRenderer>().color = new Color(this.transform.GetChild(activePlatforms).gameObject.GetComponent<SpriteRenderer>().color.r, this.transform.GetChild(activePlatforms).gameObject.GetComponent<SpriteRenderer>().color.g, this.transform.GetChild(activePlatforms).gameObject.GetComponent<SpriteRenderer>().color.b, 255);
                activePlatforms++;
                reappearTimer = reappearTime;
            }
            if (activePlatforms >= 3)
            {
                reappearing = false;
                startReappearing = 2.0f;
            }
            reappearTimer -= Time.deltaTime;
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
        if (collision.tag == "Player" && !reappearing)
        {
            disappearing = true;
        }
    }
}
