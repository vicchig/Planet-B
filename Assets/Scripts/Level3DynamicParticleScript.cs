using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3DynamicParticleScript : MonoBehaviour
{
    public float heatEnergyThreshold;
    public float heatEnergyIncrement;

    private void Start()
    {

    }

    private void Update()
    {
        if (heatEnergyThreshold <= 0) {
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = true;
            this.transform.localScale = new Vector3(5, 5, 1);
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "FreezeCollider") {
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = true;
            this.GetComponent<CircleCollider2D>().enabled = false;
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (collision.transform.tag == "bullet2") {
            Debug.Log("Getting heated");
            heatEnergyThreshold -= heatEnergyIncrement;
            Destroy(collision.gameObject);
        }
    }
}
