using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    protected Rigidbody2D rb;
    public float speed = 20f;
    public float despawnTime = 10.0f;
    protected Vector2 shootDirection;
    Vector2 currentPosition;
    Vector2 bulletVelocity;
    CircleCollider2D cc;

    public int playerDir;
    
    protected virtual void Start()
    {
        Camera cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();

        Destroy(gameObject, despawnTime); // despawner


        shootDirection = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z)) - transform.position;

        rb.velocity = shootDirection.normalized * speed;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Destructibles"))
        {
            Vector2 hitPosition = transform.position;
            //Debug.Log(hitPosition);
            Tilemap tilemap = collision.GetComponent<Tilemap>();
            Vector2 radius = new Vector2(0, 0);
            
            for (int x = 1; x > -2; x -= 2)
            {
                for (int y = 1; y > -2; y -= 2) 
                {
                    Vector2 temp = new Vector2(rb.velocity.x * x, rb.velocity.y * y); //rb.velocity; //(Vector2)collision.transform.position - hitPosition;
                    if (temp.x > 0)
                    {
                        radius.x = cc.radius;
                    }
                    else if (temp.x < 0)
                    {
                        radius.x = -cc.radius;
                    }
                    if (temp.y > 0)
                    {
                        radius.y = cc.radius;
                    }
                    else if (temp.y < 0)
                    {
                        radius.y = -cc.radius;
                    }
                    Vector3Int pos = tilemap.WorldToCell(hitPosition + radius);
                    if (tilemap.GetTile(pos) != null)
                    {
                        tilemap.SetTile(pos, null);
                        x -= 4;
                        break;
                    }
                }
            }

            Debug.Log(tilemap.WorldToCell(hitPosition));
            Destroy(gameObject);
        } else if (collision.CompareTag("Platforms"))
        {
            Destroy(gameObject);
        }

    }
}
