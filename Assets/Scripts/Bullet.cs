using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public float speed = 20f;
    //public GameObject tilemapGameObject;
    //Tilemap tilemap;
    
    void Start()
    {
        Destroy(gameObject, 5.0f);
        rb = GetComponent<Rigidbody2D>();
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            rb.velocity = -1 * speed * transform.up;
        } else if (Input.GetAxisRaw("Vertical") > 0)
        {
            rb.velocity = speed * transform.up;
        } else
        {
            rb.velocity = speed * transform.right;
        }
        // if (tilemapGameObject != null)
        //{
        //    tilemap = tilemapGameObject.GetComponent<Tilemap>();
        //}
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destructibles"))
        {


            Vector2 hitPosition = transform.position;
            //Debug.Log(hitPosition);
            Tilemap tilemap = collision.GetComponent<Tilemap>();
            tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            Destroy(gameObject);
        } else if (collision.CompareTag("Platforms"))
        {
            Destroy(gameObject);
        }
        
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //Detecting the Grid Position of Player
    //    if (collision.gameObject.name == "Player")
    //    {
    //        //Vector2 pPos = collision.gameObject.WorldToCell(collision.rigidbody.position);
    //      //  Debug.Log("pPos:" + pPos);
    //    //    tilemap.SetTile(pPos, null);
    //    }

    //}
}
