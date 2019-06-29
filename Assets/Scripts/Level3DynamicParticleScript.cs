using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    Tilemap destructMap;
    BoundsInt bounds;
    int[] destroyed;
    private bool stopMoving;
    GameManagerLevel3 manager;
    private bool frozen;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        seeped = false;

        destructMap = GameObject.Find("Grid").transform.GetChild(3).GetComponent<Tilemap>();
        bounds = destructMap.cellBounds;
        destroyed = new int[3];
        stopMoving = false;

        manager = GameObject.Find("GameManager").GetComponent<GameManagerLevel3>();
    }

    private void Update()
    {
        if (heatEnergyThreshold <= 0) {
            sr.enabled = false;
            bc.enabled = false;
            cc.enabled = true;
            this.transform.localScale = new Vector3(5, 5, 1);
            this.transform.GetChild(0).gameObject.SetActive(true);
            frozen = false;
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
            if (!stopMoving) {
                checkBlockingWallDestruction();
            }
        }
    }

    private void checkBlockingWallDestruction()
    {
        TileBase[] tiles = destructMap.GetTilesBlock(bounds);

        for (int i = 0; i < destructMap.size.x; i++)
        {
            for (int j = 0; j < destructMap.size.y; j++)
            {
                TileBase tile = tiles[i + j * bounds.size.x];
                if (tile == null && i == 6)
                {
                    if (j == 19 && destroyed[0] == 0)
                    {
                        destroyed[0] = 1;
                    }
                    else if (j == 20 && destroyed[1] == 0)
                    {
                        destroyed[1] = 1;
                    }
                    else if (j == 21 && destroyed[2] == 0)
                    {
                        destroyed[2] = 1;
                    }
                }
            }
        }

        int count = 0;
        for (int i = 0; i < destroyed.Length; i++)
        {
            if (destroyed[i] == 1)
            {
                count++;
            }
        }

        if (count >= 3)
        {
            rb.velocity = new Vector2(2, rb.velocity.y);
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (seepingEnabled)
        {
            if (collision.gameObject.name == "SeepingCollider")
            {
                cc.enabled = false;
                bc.enabled = false;
            }
            else if (collision.gameObject.name == "RightPoolBoundary")
            {
                stopMoving = true;
                rb.velocity = new Vector2(0, 0);
            }
            else if (collision.gameObject.name == "WaterPoolColliderRight")
            {
                manager.setWaterInPool1(manager.getWaterInPool1() + 1);
            }
        }
        else {
            if (collision.transform.tag == "bullet2")
            {
                heatEnergyThreshold -= heatEnergyIncrement;
                if (frozen)
                {
                    Destroy(collision.gameObject);
                }
            }
            else if (collision.gameObject.name == "WaterPoolColliderLeft" && heatEnergyThreshold <= 0) {
                manager.setWaterInPool2(manager.getWaterInPool2() + 1);
            }
            else if (collision.transform.tag == "FreezeCollider")
            {
                this.transform.GetChild(0).gameObject.SetActive(false);
                sr.enabled = true;
                bc.enabled = true;
                cc.enabled = false;
                this.transform.localScale = new Vector3(1, 1, 1);
                frozen = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (seepingEnabled) {
            if (collision.gameObject.name == "SeepingCollider")
            {
                cc.enabled = true;
                bc.enabled = false;
            }
        }
    }
}
