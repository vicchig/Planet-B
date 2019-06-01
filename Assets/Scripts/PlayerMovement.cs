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
    public float wallGrabDistance = 0.4f; //distance between player and wall within which player can grab it
    public float wallGrabOffset = 0.5f;//vertical offset for where to draw raycasts for wall grab check
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


    //store the respective component of the player object
    private CapsuleCollider2D boxCollider;
    private Rigidbody2D rBody;
    private Animator animator;

    /*Jump Attributes*/
    private float jumpTime; //how long the jump lasts
    private float jumpDelay; //holds the inspector jump delay
    private float originalScaleX;
    private Vector2 jumpColliderSize;

    /*Attributes of */
    private Vector2 collidedPlatformVelocity;
    private Vector3 collidedPlatformDir;


    /*Misc*/
    private int dir; //1 for right -1 for left
    private Vector2 defaultColliderSize;
    private bool facingRight;
    public Transform wallCheckPoint; //point that determines if player can grab wall
    private PlayerInput input; //stores current inputs for the player

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        jumpColliderSize = new Vector2(defaultColliderSize.x, defaultColliderSize.y*0.5f);
        collidedPlatformVelocity = new Vector2(0, 0);

        originalScaleX = transform.localScale.x;
        defaultColliderSize = boxCollider.size;

        facingRight = false;
        isOnPlatform = false;
    }

    private void FixedUpdate() {
        physicsCheck();

        groundMovement();
        inAirMovement();
        onWallMovement();
        setAnimations();
    }

     private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }

    private void shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //bullet.tilemapGameObject = tilemapGameObject;
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
            isHanging = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, groundLayer);
        }
    }

    private void onWallMovement() {
        if (isHanging && !isOnPlatform && !isOnGround) {
            rBody.velocity = new Vector2(0,-1);            
        }
    
    }

    private void groundMovement() {
        float xVelocity = speedX * input.horizontalIn;

        //determining player sprite direction
        if (input.horizontalIn > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingRight = true;
        }
        else if (input.horizontalIn < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            facingRight = false;
        }

        
        //movement
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
        }
        else {
            boxCollider.size = defaultColliderSize;
        }

        if (input.jumpPressed && !isJumping && (isOnPlatform || isOnGround || jumpDelay > Time.time || (isHanging && !isOnGround && !isOnPlatform)))
        {

            //for some reason when on a platform and moving down, the jump was decreased, increasing jump force for that case fixed that
            if (!isHanging)
            {
                if (isOnPlatform && collidedPlatformVelocity.y * collidedPlatformDir.y < 0)
                {
                    rBody.AddForce(new Vector2(0f, jumpForce * 1.3f), ForceMode2D.Impulse);
                }
                else
                {
                    rBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                }
            }
            else {
                isHanging = false;
                if (facingRight)
                {
                    
                    rBody.position = new Vector2(rBody.position.x - 0.5f, rBody.position.y);
                }
                else {
                    rBody.position = new Vector2(rBody.position.x + 0.5f, rBody.position.y);
                }
                rBody.AddForce(new Vector2(0f, jumpForce * 1.3f), ForceMode2D.Impulse);
            }




            isOnGround = false;
            isJumping = true;
            isOnPlatform = false;
            jumpTime = Time.time + 0.5f;
        }
        else if (rBody.velocity.y != 0 && !isOnPlatform && !isHanging && !isOnGround)
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
        if (input.horizontalIn != 0 && !isJumping && !isHanging)
        {
            animator.SetBool("movingHorizontally", true);
            animator.SetBool("standing", false);
            animator.SetBool("jumping", false);
            animator.SetBool("hanging", false);
        }
        else if (isJumping)
        {
            animator.SetBool("jumping", true);
            animator.SetBool("movingHorizontally", false);
            animator.SetBool("hanging", false);
        }
        else if (input.horizontalIn == 0 && !isJumping && !Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("standing", true);
            animator.SetBool("movingHorizontally", false);
            animator.SetBool("jumping", false);
            animator.SetBool("shooting", false);
            animator.SetBool("hanging", false);
        }
        else if (Input.GetButtonDown("Fire1") && input.horizontalIn == 0 && !isJumping) {
            animator.SetBool("standing", false);
            animator.SetBool("shooting", true);
            animator.SetBool("hanging", false);
        }
        else if (isHanging && !isJumping && !isOnGround && !isOnPlatform) {
            animator.SetBool("hanging", true);
            animator.SetBool("jumping", false);
            animator.SetBool("standing", false);
            animator.SetBool("shooting", false);
            animator.SetBool("movingHorizontally", false);

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
