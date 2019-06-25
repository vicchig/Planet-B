using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3DynamicParticleScript : MonoBehaviour
{
    public float heatEnergyThreshold;
    public float heatEnergyIncrement;
    public GameObject seepingCheckpoint;
    public LayerMask groundLayer;
    public bool seepingEnabled;

    CircleCollider2D cc;
    BoxCollider2D bc;
    SpriteRenderer sr;
    private Rigidbody2D rb;
    private bool seeped;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        seeped = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "bullet2")
        {
            heatEnergyThreshold -= heatEnergyIncrement;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name == "SeepingCollider") {
            cc.enabled = false;
            bc.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SeepingCollider")
        {
            cc.enabled = true;
            bc.enabled = false;
        }
    }

    private void Update()
    {
        if (heatEnergyThreshold <= 0) {
            sr.enabled = false;
            bc.enabled = false;
            cc.enabled = true;
            this.transform.localScale = new Vector3(5, 5, 1);
            this.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (Physics2D.OverlapCircle(seepingCheckpoint.transform.position, 0.1f, groundLayer))
        {
            if (rb.position.y > -2)
            {
                rb.velocity = new Vector2(rb.velocity.x, -0.5f);
            }
            seeped = true;
        }
        else if(seepingEnabled && seeped){
            cc.enabled = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "FreezeCollider") {
            this.transform.GetChild(0).gameObject.SetActive(false);
            sr.enabled = true;
            bc.enabled = true;
            cc.enabled = false;
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

}
