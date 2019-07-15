using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatformMover : MonoBehaviour
{
    [Header("Transforms")]
    public Transform platform;
    public Transform StartTransform;
    public Transform EndTransform;

    [Header("Movement Attributes")]
    public float platformSpeedX;
    public float platformSpeedY;
    public bool active; //whether the platform should be moving or not

    private Rigidbody2D rBody;
    private Transform destination;
    private bool collidingWithPlayer;
    private Vector2 tempSpeed;

    [HideInInspector]
    public Vector3 direction;//direction that the platform is moving in at start

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        SetDestination(EndTransform);

        collidingWithPlayer = false;
        tempSpeed = new Vector2(platformSpeedX, platformSpeedY);
    }

    private void Update()
    {

        if (platform.position.y >= StartTransform.position.y && !collidingWithPlayer) {
            active = false;
        }
        else if (platform.position.y <= EndTransform.position.y && collidingWithPlayer) {
            platformSpeedY = 0;
        }

        if (!collidingWithPlayer)
        {
            SetDestination(StartTransform);
            platformSpeedY = tempSpeed.y;
        }

    }

    //draws the start and end positions
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(StartTransform.position, new Vector3(gameObject.transform.localScale.x * 3.8f, gameObject.transform.localScale.y * 1.2f, gameObject.transform.localScale.z));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(EndTransform.position, new Vector3(gameObject.transform.localScale.x * 3.8f, gameObject.transform.localScale.y * 1.2f, gameObject.transform.localScale.z));
    }


    private void FixedUpdate()
    {

        //deltaTime  returns around 0.02, multiplying by this value allows us to work in units/second otherwise it would be units/fixed time step (whatever that is)
        //use MovePosition steps a rigidbody through a number of positions before reaching the final transform position, this ensures that the object does not teleport and any collisions that occur on the way are registered
        //it should also carry the player with it
        if (active)
        {
            rBody.MovePosition(rBody.position + (new Vector2(direction.x * platformSpeedX, direction.y * platformSpeedY) * Time.fixedDeltaTime));



            
                if (Vector3.Distance(platform.position, destination.position) < platformSpeedY * Time.fixedDeltaTime)//we compare it to the right side because that is how far the platform moves in one update
                {
                // SetDestination(destination == StartTransform ? EndTransform : StartTransform);
                    
                }
            

        }




    }

    private void SetDestination(Transform dest)
    {
        destination = dest;
        direction = (destination.position - platform.position).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player") {
            active = true;
            collidingWithPlayer = true;
            SetDestination(EndTransform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collidingWithPlayer = false;
            SetDestination(StartTransform);
        }
    }

}
