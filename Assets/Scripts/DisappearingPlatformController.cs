using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatformController : MonoBehaviour
{
    [Header("Platform Attributes")]
    public float disappearTime;
    public float disappearSpeed;
    public float reappearTime;
    public float reappearSpeed;
    
    private bool reappearing;

    private float disappearTimer;
    private float reappearTimer;

    private int activePlatforms;
    private bool disappearing;

    private void Start()
    {
        disappearTimer = disappearTime;
        activePlatforms = 3;
        reappearing = false;
        reappearTimer = 0;
    }

    private void Update()
    {
        if (activePlatforms <= 0)
        {
            disappearing = false;
            reappearing = true;
        }

        if (reappearing)
        {
            transform.GetChild(activePlatforms).gameObject.GetComponent<SpriteRenderer>().color = new Color(this.transform.GetChild(activePlatforms).gameObject.GetComponent<SpriteRenderer>().color.r, this.transform.GetChild(activePlatforms).gameObject.GetComponent<SpriteRenderer>().color.g, this.transform.GetChild(activePlatforms).gameObject.GetComponent<SpriteRenderer>().color.b, (reappearTimer * 100 / reappearTime));
            transform.GetChild(activePlatforms).gameObject.SetActive(true);


            if (reappearTimer >= reappearTime)
            {
                transform.GetChild(activePlatforms).gameObject.GetComponent<BoxCollider2D>().enabled = true;
                activePlatforms++;
                reappearTimer = 0;

                if (activePlatforms >= 3)
                {
                    reappearing = false;
                }
            }
        }
        else if (disappearing){
           
                this.transform.GetChild(activePlatforms - 1).gameObject.GetComponent<SpriteRenderer>().color = new Color(this.transform.GetChild(activePlatforms - 1).gameObject.GetComponent<SpriteRenderer>().color.r, this.transform.GetChild(activePlatforms - 1).gameObject.GetComponent<SpriteRenderer>().color.g, this.transform.GetChild(activePlatforms - 1).gameObject.GetComponent<SpriteRenderer>().color.b, this.transform.GetChild(activePlatforms - 1).gameObject.GetComponent<SpriteRenderer>().color.a * (disappearTimer / disappearTime));
                transform.GetChild(activePlatforms - 1).gameObject.GetComponent<BoxCollider2D>().enabled = false;

                if (disappearTimer <= 0) {
                    disappearTimer = disappearTime;
                    activePlatforms--;
                    this.transform.GetChild(activePlatforms).gameObject.SetActive(false);
                }
            
        }
    }

    private void FixedUpdate()
    {
        if (disappearing && activePlatforms > 0)
        {
            disappearTimer -= disappearSpeed;
        }
        else if (reappearing) {
            reappearTimer += reappearSpeed;
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
