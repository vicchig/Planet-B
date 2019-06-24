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
    public float forceUp;
    public float forceDown = 0.4f;

    private List<AirCurrentObj> objects;


    private void Start()
    {
        objects = new List<AirCurrentObj>();
    }


    private void Update()
    {
        for (int i = 0; i < objects.Count; i++) {
            if (objects[i].getBody() == null || !objects[i].isInAirCurrent())
            {
                Debug.Log("Hello there");
                objects.RemoveAt(i);
            }
        }

        for (int  i = 0; i < objects.Count; i++) {
            if (objects[i].isInAirCurrent())
            {
                if (objects[i].getBody().velocity.y >= 0)
                {
                    objects[i].setTempVelocityY(objects[i].getTempVelocityY() + forceUp);

                }
                else {
                    objects[i].setTempVelocityY(objects[i].getTempVelocityY() + forceDown);
                }
                objects[i].setVelocity();
            }
        }

        for (int i = 0; i < objects.Count; i++) {
            if (objects[i].getResetVelocityTimer() >= 0.2) {
                objects[i].setTempVelocityY(0);
                objects[i].setResetVelocity(false);
                objects[i].setResetVelocityTimer(0);
                objects[i].setInAirCurrent(false);
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].isResetVelocity()) {
                objects[i].setResetVelocityTimer(objects[i].getResetVelocityTimer() + Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].getCollider() == collision)
            {
                return;
            }
        }
        if (collision.tag == "Player")
        {
            objects.Add(new AirCurrentObj(collision.GetComponent<Rigidbody2D>(), collision, false));
            objects[objects.Count - 1].setInAirCurrent(true);
        }
        else if (collision.tag == "WaterVapour")
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, collision.GetComponent<Rigidbody2D>().velocity.y);
            objects.Add(new AirCurrentObj(collision.GetComponent<Rigidbody2D>(), collision, true));
            objects[objects.Count - 1].setInAirCurrent(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < objects.Count; i++) {
            if (collision == objects[i].getCollider()) {
                objects[i].setResetVelocity(true);
            }
        }
    }
    /*
    private bool inAirCurrent; 
    private float tempVeolictyY;
    private Rigidbody2D collidedBody;

    private float resetVelocityTimer;
    private bool resetVelocity;

    private void Start()
    {
        inAirCurrent = false;
        tempVeolictyY = 0;
        collidedBody = null;
    }

    private void Update()
    {
        if (inAirCurrent) {
            
            tempVeolictyY += forceP;
            collidedBody.velocity = new Vector2(collidedBody.velocity.x, tempVeolictyY);

        }
        else{
            inAirCurrent = false;
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
            inAirCurrent = true;
            collidedBody = other.GetComponent<Rigidbody2D>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        resetVelocity = true;
        inAirCurrent = false;
    }*/
}
