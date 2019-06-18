using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Class that controls player movement.</summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    public float speedX = 8f; //player's vertical speed
    public float jumpDelayDuration = 0.05f; //amount of time you have to jump up once you have walked off an edge of the platform you were on
    public float maxFallSpeed = -10.0f; //limit for how fast the player can fall down

    [Header("Jump Properties")]
    public float jumpForce = 6.3f; //force of the regular player jump

    [Header("Enviro Check Properties")]
    public float groundDistance = 0.2f; //distance at which the player is considered to be on the ground at
    public LayerMask groundLayer; //layer that contains static platforms
    public LayerMask movingPlatLayer; //layer that contains moving platforms

    [Header("Status Flags")]
    public bool isOnGround; //whether the player is on graound or not
    public bool isJumping; //whether the player has just attempted to jump using the jump key
    public bool isOnPlatform; //whether the player is on a moving platform or not
    public bool isHanging; //whether the player is hanging off a wall or moving platform

    [Header("Weapon Components")]
    public Transform firePoint; //the point where player bullets are spawned

    [Header("Collision Checkpoints")]
    public Transform wallGrabCheckPointTransform; //point that checks whether the player can grab a wall or not
    public Transform platCollideCheckPoint; //point that checks whether the player's feet are on the ground or not

    private PlayerInput input; //stores current inputs for the player
    private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;
    private Rigidbody2D rBody;

    private Vector2 defaultColliderSize; //the starting size of the player's collider
    private Vector2 jumpColliderSize; //the size of the player's collider when they are in the air
    private Vector2 collidedPlatformVelocity; //velocity of the moving platform that the player is colliding with
    private Vector3 collidedPlatformDir; //direction of the moving platform that the player is colliding with
    private Animator animator; 

    private bool landingSoundPlayed; //whether the landing sound has been played or not when the player touches the ground after a jump
    private bool initiateWallHang; //whether the player can wall grab or not (ie. is the wall grab key being pressed)
    private bool isInAir; //wether the player is in the air or not
    private bool suppressAD = false; //whether A and D key presses should be suppressed

    private float suppressADTimer = 0f; //current amount of time that A and D key presses have been suppresed for
    private float jumpDelayTime; //holds the inspector jump delay
    private float originalScaleX; //the transform's x scale that the player starts with

    public int dir; //1 for right -1 for left

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        originalScaleX = transform.localScale.x;
        isOnPlatform = false;

        collidedPlatformVelocity = new Vector2(0, 0);

        animator = GetComponent<Animator>();

        defaultColliderSize = boxCollider.size;
        jumpColliderSize = new Vector2(defaultColliderSize.x, defaultColliderSize.y * 0.5f);

        dir = 1;

        landingSoundPlayed = true;
        initiateWallHang = false;
    }

    private void FixedUpdate() {
        physicsCheck();
        inAirMovement();

        //timer for suppressing A and D presses after a wall hang jump
        if (suppressAD)
        {
            if (suppressADTimer >= 0.20f)
            {
                suppressADTimer = 0;
                suppressAD = false;
            }
            else {
                suppressADTimer += Time.fixedDeltaTime;
            }
        }

        //changing collider size during a jump

    }

    private void Update()
    {
        //determine whether jumping animation should be palyed
        if (rBody.velocity.y != 0 && !isOnGround && !isOnPlatform && !isHanging)
        {
            isInAir = true;
        }
        else {
            isInAir = false;
        }

        //change firepoint location when hanging on a wall and shooting
        if (isHanging)
        {
            firePoint.transform.localPosition = new Vector3(-0.454f, -0.133f, 0f);

            if (input.jumpPressed) {
                suppressAD = true;
                suppressADTimer = 0;
            }

        }
        else
        {
            firePoint.transform.localPosition = new Vector3(0.454f, -0.133f, 0f);
        }        

        //wall climb setting when appropriate input is pressed
        if (Input.GetButtonDown("WallClimb")) {
            initiateWallHang = true;
        }
        if (Input.GetButtonUp("WallClimb")) {
            initiateWallHang = false;
            isHanging = false;
        }


        //setting isHanging to true or false
        if (initiateWallHang)
        {
            if (!isOnGround && Physics2D.OverlapCircle(wallGrabCheckPointTransform.position, 0.1f, groundLayer) || Physics2D.OverlapCircle(wallGrabCheckPointTransform.position, 0.1f, movingPlatLayer)) //!Physics2D.OverlapCircle(wallGrabCheckPointTransform.position, 0.1f, movingPlatLayer))
            {
                isHanging = true;
            }
            else
            {
                isHanging = false;
            }
        }

        //suppressing A and D key presses after a wall hang jump
        if (suppressAD) {
            input.horizontalIn = 0;
        }

        /*Determine whether the player is on the ground or not*/
        if (Physics2D.OverlapCircle(platCollideCheckPoint.position, 0.1f, groundLayer))
        {
            isOnGround = true;
            isOnPlatform = false;
            isInAir = false;
            isHanging = false;
            isJumping = false;
        }
        else
        {
            isOnGround = false;
        }

        horizontalMovement();
        wallHangMovement();

        setAnimations();
        playSounds();
    }

    //performs various physics related checks and calculations, should be put in FixedUpdate
    private void physicsCheck() {
        //changing colliders during wall grab
        if (isHanging && !Physics2D.OverlapCircle(wallGrabCheckPointTransform.position, 0.1f, movingPlatLayer))
        {
            boxCollider.enabled = false;
            capsuleCollider.enabled = true;
        }
        else {
            capsuleCollider.enabled = false;
            boxCollider.enabled = true;
        }

    }

    private void horizontalMovement() {
        float xVelocity = speedX * input.horizontalIn;

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
            platformMovement(xVelocity);
        }
        else {
            rBody.velocity = new Vector2(xVelocity, rBody.velocity.y);
        }
    }

    private void inAirMovement() {
        if (input.jumpPressed)
        {

            if (!isJumping && (isOnPlatform || isOnGround || jumpDelayTime > Time.time || (isHanging && !isOnGround && !isOnPlatform) || (isHanging && isOnPlatform))) {
                //wall hang jump off platform
                if (isHanging && isOnPlatform)
                {
                    transform.SetParent(null);
                    isOnPlatform = false;
                }

                //jump is severely decreased on a platform that is moving down and increased for one that is moving up, so to balance that out, give a little boost when jumping on a platform that is moving down
                if (isOnPlatform)
                {
                    platformJump();
                }
                else
                {
                    if (isHanging)
                    {
                        wallJump();
                    }
                    else
                    {
                        rBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                    }
                }
                isOnGround = false;
                isHanging = false;
                isJumping = true;
                isOnPlatform = false;
                landingSoundPlayed = false;
            }
        }
        else {
            isJumping = false;
        }

        //limit maximum falling speed
        if (rBody.velocity.y < maxFallSpeed) {
            rBody.velocity = new Vector2(rBody.velocity.x, maxFallSpeed);
        }
    }

    //movement of the palyer on a moving platform
    private void platformMovement(float xVelocity) {
        if (input.horizontalIn == 0)
        {
            rBody.velocity = new Vector2(collidedPlatformVelocity.x * collidedPlatformDir.x, collidedPlatformVelocity.y * collidedPlatformDir.y);
        }
        else
        {
            rBody.velocity = new Vector2(xVelocity + collidedPlatformVelocity.x * collidedPlatformDir.x, collidedPlatformVelocity.y * collidedPlatformDir.y);
        }
    }

    //jump of the player on a moving platform
    private void platformJump() {
        if (collidedPlatformDir.y < 0 && collidedPlatformVelocity.y == 0)
        {
            rBody.AddForce(new Vector2(0f, jumpForce * 1.2f), ForceMode2D.Impulse);
        }
        else if (collidedPlatformDir.y > 0 && collidedPlatformVelocity.y == 0)
        {
            rBody.AddForce(new Vector2(0f, jumpForce * 0.9f), ForceMode2D.Impulse);
        }
        else {
            rBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void wallJump() {
        rBody.transform.position = new Vector2(rBody.transform.position.x - 1f * dir, rBody.transform.position.y);
        rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y);
        rBody.AddForce(new Vector2(5f * dir, jumpForce * 1.3f), ForceMode2D.Impulse);
    }

    private void setAnimations()
    {
        bool movingHorizontally = false, jumping = false, standing = false, shootingWhileStanding = false, hanging = false;

        if (input.horizontalIn != 0 && !isInAir && !isHanging)
        {
            movingHorizontally = true;
            jumping = false;
            standing = false;
            hanging = false;
            shootingWhileStanding = false;
        }
        else if (isInAir)
        {
            jumping = true;
            standing = false;
            shootingWhileStanding = false;
            hanging = false;
        }
        else if (input.horizontalIn == 0 && !isInAir && !Input.GetButtonDown("Fire1") && !isHanging)
        {
            standing = true;
            movingHorizontally = false;
            jumping = false;
            shootingWhileStanding = false;
            hanging = false;
        }
        else if (Input.GetButtonDown("Fire1") && input.horizontalIn == 0 && !isInAir && !isHanging) {
            shootingWhileStanding = true;
            movingHorizontally = false;
            jumping = false;
            standing = false;
            hanging = false;
        }
        else if (isHanging) {
            hanging = true;
            shootingWhileStanding = false;
            standing = false;
            jumping = false;
            movingHorizontally = false;
        }

        animator.SetBool("standing", standing);
        animator.SetBool("movingHorizontally", movingHorizontally);
        animator.SetBool("jumping", jumping);
        animator.SetBool("shooting", shootingWhileStanding);
        animator.SetBool("hanging", hanging);
    }

    private void wallHangMovement() {
        if (isHanging)
        {
            rBody.velocity = new Vector2(rBody.velocity.x, -1);

        }
        else
        {
            rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y);
        }

        if (Physics2D.OverlapCircle(wallGrabCheckPointTransform.position, 0.1f, movingPlatLayer) || !Physics2D.OverlapCircle(platCollideCheckPoint.position, 0.12f, movingPlatLayer))
        {
            isOnPlatform = false;
        }
    }


    //changes the direction of the player on the X axis
    private void flipPlayerDir() {
        dir *= -1;

        Vector3 scale = transform.localScale;

        scale.x = originalScaleX * dir;
        transform.localScale = scale;
    }


    private void playSounds() {
        //footsteps
        if (input.horizontalIn != 0 && (isOnGround || isOnPlatform) && !isHanging)
        {
            AudioManager.playFootstepSound();
        }

        //landing
        if (!isJumping && isOnGround && !landingSoundPlayed)
        {
            AudioManager.playFootstepSound();
            landingSoundPlayed = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {

        if ((col.transform.CompareTag("MovingPlatform") || col.transform.CompareTag("RandomlyMovingPlatform")) && Physics2D.OverlapCircle(platCollideCheckPoint.position, 0.14f, movingPlatLayer))
        {
            transform.parent = col.transform;
            isOnPlatform = true;
            isJumping = false;
            if (col.transform.CompareTag("MovingPlatform")) {
                col.gameObject.GetComponent<PlatformMover>().active = true;
            }
        }

    }

    private void OnCollisionStay2D(Collision2D col) {
        //getting onto moving platforms or randomly moving platforms
        if (col.transform.tag == "RandomlyMovingPlatform") {
            if (col.gameObject.GetComponent<RandomPlatformMover>().isMoving())
            {
                collidedPlatformVelocity.x = col.gameObject.GetComponent<RandomPlatformMover>().platformSpeedX;
                collidedPlatformVelocity.y = col.gameObject.GetComponent<RandomPlatformMover>().platformSpeedY;
                collidedPlatformDir.x = col.gameObject.GetComponent<RandomPlatformMover>().direction.x;
                collidedPlatformDir.y = col.gameObject.GetComponent<RandomPlatformMover>().direction.y;
            }
            else {
                collidedPlatformVelocity.x = 0;
                collidedPlatformVelocity.y = 0;
                collidedPlatformDir.x = 0;
                collidedPlatformDir.y = 0;
            }

            if (Physics2D.OverlapCircle(wallGrabCheckPointTransform.position, 0.1f, movingPlatLayer) && initiateWallHang)
            {
                transform.SetParent(col.transform);
                isOnPlatform = true;
            }
        }
        else if (col.transform.tag == "MovingPlatform") {
            collidedPlatformVelocity.x = col.gameObject.GetComponent<PlatformMover>().platformSpeedX;
            collidedPlatformVelocity.y = col.gameObject.GetComponent<PlatformMover>().platformSpeedY;
            collidedPlatformDir.x = col.gameObject.GetComponent<PlatformMover>().direction.x;
            collidedPlatformDir.y = col.gameObject.GetComponent<PlatformMover>().direction.y;

            if (Physics2D.OverlapCircle(wallGrabCheckPointTransform.position, 0.1f, movingPlatLayer) && initiateWallHang)
            {
                transform.SetParent(col.transform);
                isOnPlatform = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col) {
        //leaving moving platforms
        if (col.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
            isOnPlatform = false;
        }
        else if (col.transform.tag == "RandomlyMovingPlatform")
        {
            transform.parent = null;
            isOnPlatform = false;
        }
    }
}
