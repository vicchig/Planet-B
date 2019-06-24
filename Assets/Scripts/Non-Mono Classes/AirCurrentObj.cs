using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCurrentObj
{
    private bool inAirCurrent;
    private float tempVelocityY;
    private Rigidbody2D body;
    private Collider2D collider;
    private float resetVelocityTimer;
    private bool resetVelocity;

    public AirCurrentObj(Rigidbody2D body, Collider2D collider, bool resetVelocityX) {
        this.body = body;
        inAirCurrent = false;
        tempVelocityY = body.velocity.y;
        resetVelocity = false;
        resetVelocityTimer = 0f;
        this.collider = collider;

        if (resetVelocityX) {
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }

    public void setInAirCurrent(bool tf) {
        inAirCurrent = tf;
    }

    public void setBody(Rigidbody2D body) {
        this.body = body;
    }

    public void setTempVelocityY(float velocity) {
        tempVelocityY = velocity;
    }

    public void setResetVelocity(bool tf) {
        resetVelocity = tf;
    }

    public void setResetVelocityTimer(float time) {
        resetVelocityTimer = time;
    }

    public Collider2D getCollider() {
        return collider;
    }

    public bool isInAirCurrent() {
        return inAirCurrent;
    }

    public float getTempVelocityY() {
        return tempVelocityY;
    }

    public void setVelocity() {
        body.velocity = new Vector2(body.velocity.x, tempVelocityY); ;
    }

    public float getResetVelocityTimer() {
        return resetVelocityTimer;
    }

    public bool isResetVelocity() {
        return resetVelocity;
    }

    public Rigidbody2D getBody() {
        return body;
    }
}
