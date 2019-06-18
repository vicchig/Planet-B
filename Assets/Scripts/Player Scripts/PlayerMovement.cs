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

    public Transform wallGrabCheckPointTransform;
    public Transform platCollideCheckPoint;

    PlayerInput input; //stores current inputs for the player

    //store the respective component of the player object
    BoxCollider2D boxCollider;
    CapsuleCollider2D capsuleCollider;
    Rigidbody2D rBody;

    float jumpDelayTime; //holds the inspector jump delay
    float originalScaleX;

    public int dir; //1 for right -1 for left


    private Vector2 collidedPlatformVelocity;
    private Vector3 collidedPlatformDir;
    private Animator animator;

    private Vector2 defaultColliderSize;
    private Vector2 jumpColliderSize;

    private bool landingSoundPlayed;
    private bool initiateWallHang;

    private bool suppressAD = false;
    private float suppressADTimer = 0f;

    private bool playJumpingAnim;

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
    }

    private void Update()
    {


        //determine whether jumping animation should be palyed
        if (rBody.velocity.y != 0 && !isOnGround && !isOnPlatform && !isHanging)
        {
            playJumpingAnim = true;
        }
        else {
            playJumpingAnim = false;
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

        //playing the landing sound after a jump
        if (!isJumping && isOnGround && !landingSoundPlayed) {
            AudioManager.playFootstepSound();
            landingSoundPlayed = true;
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
            playJumpingAnim = false;
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
    }


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

        if (input.horizontalIn != 0 && (isOnGround || isOnPlatform) && !isHanging) {
            AudioManager.playFootstepSound();
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

        if (input.horizontalIn != 0 && !playJumpingAnim && !isHanging)
        {
            movingHorizontally = true;
            jumping = false;
            standing = false;
            hanging = false;
            shootingWhileStanding = false;
        }
        else if (playJumpingAnim)
        {
            jumping = true;
            standing = false;
            shootingWhileStanding = false;
            hanging = false;
        }
        else if (input.horizontalIn == 0 && !playJumpingAnim && !Input.GetButtonDown("Fire1") && !isHanging)
        {
            standing = true;
            movingHorizontally = false;
            jumping = false;
            shootingWhileStanding = false;
            hanging = false;
        }
        else if (Input.GetButtonDown("Fire1") && input.horizontalIn == 0 && !playJumpingAnim && !isHanging) {
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
