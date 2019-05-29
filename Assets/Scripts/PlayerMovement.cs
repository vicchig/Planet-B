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
    public LayerMask movingPlatLayer;

    [Header("Status Flags")]
    public bool isOnGround;
    public bool isJumping;
    public bool isOnPlatform;

    PlayerInput input; //stores current inputs for the player

    //store the respective component of the player object
    BoxCollider2D boxCollider;
    Rigidbody2D rBody;

    float jumpTime; //how long the jump lasts
    float jumpDelay; //holds the inspector jump delay
    float originalScaleX;

    int dir; //1 for right -1 for left


    private Vector2 collidedPlatformVelocity;
    private Vector3 collidedPlatformDir;
    private Animator animator;

    private Vector2 defaultColliderSize;
    private Vector2 jumpColliderSize;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        originalScaleX = transform.localScale.x;
        isOnPlatform = false;

        collidedPlatformVelocity = new Vector2(0,0);

        animator = GetComponent<Animator>();

        defaultColliderSize = boxCollider.size;
        jumpColliderSize = new Vector2(defaultColliderSize.x, defaultColliderSize.y*0.5f);
    }

    private void FixedUpdate() {
        physicsCheck();

        groundMovement();
        inAirMovement();
        setAnimations();
    }

    private void Update()
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

        if (input.horizontalIn > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (input.horizontalIn < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        

        if (xVelocity * dir < 0)
        {
            flipPlayerDir();
        }

        if (!isOnPlatform)
        {
            rBody.velocity = new Vector2(xVelocity, rBody.velocity.y);
        }
        //the player kept bouncing on the platform when going up until the cases for going up and down were not separated
        else if (isOnPlatform && collidedPlatformVelocity.y* collidedPlatformDir.y > 0 ) {
            rBody.velocity = new Vector2(xVelocity + collidedPlatformVelocity.x * collidedPlatformDir.x, collidedPlatformVelocity.y * collidedPlatformDir.y);
        }
        else {
            rBody.velocity = new Vector2(xVelocity + collidedPlatformVelocity.x * collidedPlatformDir.x, rBody.velocity.y + collidedPlatformVelocity.y * collidedPlatformDir.y);

        }

        if (isOnGround)
        {
            jumpDelay = Time.time + jumpDelay;
        } 
    }

    private void inAirMovement() {

        if (isJumping)
        {
            boxCollider.size = jumpColliderSize;
            Debug.Log("Collider Size Changed");
        }
        else {
            boxCollider.size = defaultColliderSize;
        }

        if (input.jumpPressed && !isJumping && (isOnPlatform || isOnGround || jumpDelay > Time.time))
        {
            
            //for some reason when on a platform and moving down, the jump was decreased, increasing jump force for that case fixed that
            if (isOnPlatform && collidedPlatformVelocity.y * collidedPlatformDir.y < 0)
            {
                rBody.AddForce(new Vector2(0f, jumpForce * 1.3f), ForceMode2D.Impulse);
            }
            else {
                rBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }

            isOnGround = false;
            isJumping = true;
            isOnPlatform = false;
            jumpTime = Time.time + 0.5f;
        }
        else if (rBody.velocity.y != 0 && !isOnPlatform)
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

    private void setAnimations()
    {
        if (input.horizontalIn != 0 && !isJumping)
        {
            animator.SetBool("movingHorizontally", true);
            animator.SetBool("standing", false);
            animator.SetBool("jumping", false);
        }
        else if (isJumping)
        {
            animator.SetBool("jumping", true);
            animator.SetBool("movingHorizontally", false);
        }
        else if (input.horizontalIn == 0 && !isJumping && !Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("standing", true);
            animator.SetBool("movingHorizontally", false);
            animator.SetBool("jumping", false);
            animator.SetBool("shooting", false);
        }
        else if (Input.GetButtonDown("Fire1") && input.horizontalIn == 0 && !isJumping) {
            animator.SetBool("standing", false);
            animator.SetBool("shooting", true);
        }
    }

    //changes the direction of the player on the X axis
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
        
        return hit;
    }


    private void OnCollisionEnter2D(Collision2D col) {
        RaycastHit2D leftFoot = raycast(new Vector2(-footOffset, 0f), Vector2.down, 1.0f, movingPlatLayer);
        RaycastHit2D rightFoot = raycast(new Vector2(footOffset, 0f), Vector2.down, 1.0f, movingPlatLayer);

        if (col.transform.tag == "MovingPlatform" && (leftFoot || rightFoot)) {
            transform.parent = col.transform;
            isOnPlatform = true;
        }
        else if (col.transform.tag == "Spikes") {
            transform.position = new Vector3(-2,1,0);
        }
        
    }

    //called while the object is colliding with something
    private void OnCollisionStay2D(Collision2D col) {
        if (col.transform.tag == "MovingPlatform") {
            collidedPlatformVelocity.x = col.gameObject.GetComponent<PlatformMover>().platformSpeedX;
            collidedPlatformVelocity.y = col.gameObject.GetComponent<PlatformMover>().platformSpeedY;
            collidedPlatformDir.x = col.gameObject.GetComponent<PlatformMover>().direction.x;
            collidedPlatformDir.y = col.gameObject.GetComponent<PlatformMover>().direction.y;
        }
        
    }

    private void OnCollisionExit2D(Collision2D col) {
        if (col.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
            isOnPlatform = false;
        }
    }

    
}
