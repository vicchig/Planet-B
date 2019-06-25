using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3DynamicParticleScript : MonoBehaviour
{
    public float heatEnergyThreshold;
    public float heatEnergyIncrement;

    CircleCollider2D cc;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == ("SeepingCollider"))
        {
            cc.enabled = false;
            Debug.Log(cc.enabled + " enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == ("SeepingCollider"))
        {
            cc.enabled = true;
            Debug.Log(cc.enabled + " exit");
        }
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
