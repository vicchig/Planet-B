using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCurrentController : MonoBehaviour
{
    [Header("Properties")]
    [Tooltip("Force Applied to Player by the Air Current")]
    public float forceP;
    [Tooltip("Force applied to Water Vapours by the Air Current")]
    public float forceV;

    private bool active; 
    private float tempVeolictyY;
    private Rigidbody2D collidedBody;

    private float resetVelocityTimer;
    private bool resetVelocity;

    private void Start()
    {
        active = false;
        tempVeolictyY = 0;
        collidedBody = null;
    }

    private void Update()
    {
        if (active) {
            
            tempVeolictyY += forceP;
            collidedBody.velocity = new Vector2(collidedBody.velocity.x, tempVeolictyY);

        }
        else{
            active = false;
            collidedBody = null;
        }
        if (resetVelocityTimer >= 0.2) {
            tempVeolictyY = 0;
            resetVelocity = false;
            resetVelocityTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        if (resetVelocity) {
            resetVelocityTimer += Time.fixedDeltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WaterVapour") {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, collision.GetComponent<Rigidbody2D>().velocity.y);
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceV), ForceMode2D.Impulse);
            
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player") {
            active = true;
            collidedBody = other.GetComponent<Rigidbody2D>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        resetVelocity = true;
        active = false;
    }
}
