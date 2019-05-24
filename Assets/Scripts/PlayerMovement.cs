using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Properties")]
    public float speedX = 10.0f;
    public float jumpDelayInsp = 0.05f; //amount of time you have to jump up once you have walked off an edge of the platform you were on
    public float maxFallSpeed = -10.0f;

    [Header("Jump Properties")]
    public float jumpForce = 6.3f;

    [Header("Enviro Check Properties")]
    public float footOffset = 0.4f;
    public float groundDistance = 0.2f; //distance at which the player is considered to be on the ground at
    public LayerMask groundLayer;

    [Header("Status Flags")]
    public bool isOnGround;
    public bool isJumping;

    PlayerInput input; //stores current inputs for the player

    //store the respective component of the player object
    BoxCollider2D boxCollider;
    Rigidbody2D rBody;

    float jumpTime; //how long the jump lasts
    float jumpDelay; //holds the inspector jump delay
    float originalScaleX;

    int dir; //1 for right -1 for left




    void Start()
    {
        input = GetComponent<PlayerInput>();
        rBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        originalScaleX = transform.localScale.x;

    }

    void FixedUpdate() {
        physicsCheck();

        groundMovement();
        inAirMovement();
    }

    void Update()
    {

    }


    private void physicsCheck() {

        /*Determine whether the player is on the ground or not*/

        //raycasts to check if the feet are touchingthe ground
        RaycastHit2D leftFoot = raycast(new Vector2(-footOffset, 0f), Vector2.down, 1.0f, groundLayer);
        RaycastHit2D rightFoot = raycast(new Vector2(footOffset, 0f), Vector2.down, 1.0f, groundLayer);

        if (leftFoot || rightFoot)
        {
            isOnGround = true;
        }
        else {
            isOnGround = false;
        }





    }

    private void groundMovement() {
        float xVelocity = speedX * input.horizontalIn;

        if (xVelocity * dir < 0) {
            flipPlayerDir();
        }

        rBody.velocity = new Vector2(xVelocity, rBody.velocity.y);

        if (isOnGround) {
            jumpDelay = Time.time + jumpDelay;
        }
    }

    private void inAirMovement() {
        //beginning of jump
        if (input.jumpPressed && !isJumping && (isOnGround || jumpDelay > Time.time))
        {
            isOnGround = false;
            isJumping = true;

            rBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            jumpTime = Time.time + 0.5f;
        }
        else if (rBody.velocity.y != 0)
        {
            isJumping = true;
        }
        else {
            isJumping = false;
        }

        //limit maximum falling speed
        if (rBody.velocity.y < maxFallSpeed) {
            rBody.velocity = new Vector2(rBody.velocity.x, maxFallSpeed);
        }
    }

    private void flipPlayerDir() {
        dir *= -1;

        Vector3 scale = transform.localScale;

        scale.x = originalScaleX * dir;
        transform.localScale = scale;
    }

    //wraps up the raycasting 
    /*
        offset: where to draw the ray from relative to the bottom center of the player sprite
        rayDir: direction of the ray
        length: length of ray
        mask: the layer that you want to check the raycast hit with
    */
    private RaycastHit2D raycast(Vector2 offset, Vector2 rayDir, float length, LayerMask mask) {
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDir, length, mask);
        Color color;
        if (hit.collider != null)
        {
            color = Color.green;
        }
        else {
            color = Color.red;
        }
        Debug.DrawRay(pos+offset, rayDir * length, color);

        return hit;
    }
}
