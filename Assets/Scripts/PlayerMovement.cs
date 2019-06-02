using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Properties")]
    public float speedX = 8f;
    public float jumpDelayDuration = 0.05f; //amount of time you have to jump up once you have walked off an edge of the platform you were on
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
    public bool isHanging;

    [Header("Weapon Components")]
    public Transform firePoint;
    public Bullet bulletPrefab;

    PlayerInput input; //stores current inputs for the player

    //store the respective component of the player object
    BoxCollider2D boxCollider;
    Rigidbody2D rBody;

    float jumpTime; //how long the jump lasts
    float jumpDelayTime; //holds the inspector jump delay
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

        dir = 1;
    }

    private void FixedUpdate() {
        physicsCheck();

        horizontalMovement();
        
        inAirMovement();
        setAnimations();
    }

     void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //bullet.tilemapGameObject = tilemapGameObject;
    }


    private void physicsCheck() {
        if (isJumping)
        {
            boxCollider.size = jumpColliderSize;
        }
        else
        {
            boxCollider.size = defaultColliderSize;
        }


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

    private void horizontalMovement() {
        float xVelocity = speedX * input.horizontalIn;
        float yVelocity = rBody.velocity.y;

        if (xVelocity * dir < 0)
        {
            flipPlayerDir();
        }



        if (isOnGround && !input.jumpPressed)
        {
            jumpDelayTime = Time.time + jumpDelayDuration;
            isJumping = false;
        }

        if (isOnPlatform)
        {
            if (input.horizontalIn == 0)
            {
                rBody.velocity = new Vector2(collidedPlatformVelocity.x * collidedPlatformDir.x, collidedPlatformVelocity.y * collidedPlatformDir.y);
            }
            else {
                rBody.velocity = new Vector2(xVelocity + collidedPlatformVelocity.x * collidedPlatformDir.x, collidedPlatformVelocity.y * collidedPlatformDir.y);
            }
        }
        else {
            rBody.velocity = new Vector2(xVelocity, rBody.velocity.y);
        }
    }

    private void inAirMovement() {
        if (input.jumpPressed && !isJumping && (isOnPlatform || isOnGround || jumpDelayTime > Time.time))
        {

            //jump is severely decreased on a platform that is moving down and increased for one that is moving up, so to balance that out, give a little boost when jumping on a platform that is moving down
            if (isOnPlatform)
            {
                if (collidedPlatformDir.y < 0)
                {
                    rBody.AddForce(new Vector2(0f, jumpForce * 1.2f), ForceMode2D.Impulse);
                }
                else if (collidedPlatformDir.y > 0)
                {
                    rBody.AddForce(new Vector2(0f, jumpForce * 0.9f), ForceMode2D.Impulse);
                }
            }
            else {
                rBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
            isOnGround = false;
            isJumping = true;
            isOnPlatform = false;
            //jumpTime = Time.time + 0.5f;
        }
        else if (rBody.velocity.y != 0 && !isOnPlatform)
        {
            isJumping = true;
        }
        else{
            isJumping = false;
        }

        //limit maximum falling speed
        if (rBody.velocity.y < maxFallSpeed) {
            rBody.velocity = new Vector2(rBody.velocity.x, maxFallSpeed);
        }
    }

    private void setAnimations()
    {
        bool movingHorizontally = false, jumping = false, standing = false, shootingWhileStanding = false;

        if (input.horizontalIn != 0 && !isJumping)
        {
            movingHorizontally = true;
            jumping = false;
            standing = false;
            shootingWhileStanding = false;
        }
        else if (isJumping)
        {
            jumping = true;
            standing = false;
            shootingWhileStanding = false;
        }
        else if (input.horizontalIn == 0 && !isJumping && !Input.GetButtonDown("Fire1"))
        {
            standing = true;
            movingHorizontally = false;
            jumping = false;
            shootingWhileStanding = false;
        }
        else if (Input.GetButtonDown("Fire1") && input.horizontalIn == 0 && !isJumping) {
            shootingWhileStanding = true;
            movingHorizontally = false;
            jumping = false;
            standing = false;
        }

        animator.SetBool("standing", standing);
        animator.SetBool("movingHorizontally", movingHorizontally);
        animator.SetBool("jumping", jumping);
        animator.SetBool("shooting", shootingWhileStanding);

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

        if (col.transform.CompareTag("MovingPlatform") && (leftFoot || rightFoot)) {
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
