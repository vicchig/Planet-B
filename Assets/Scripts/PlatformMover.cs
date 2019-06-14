using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Header("Transforms")]
    public Transform platform;
    public Transform StartTransform;
    public Transform EndTransform;

    [Header("Movement Attributes")]
    public float platformSpeedX;
    public float platformSpeedY;
    public bool moveVertically;//whether the platform is moving vertically or not, should be checked even if direction already specifies that 
    public bool active; //whether the platform should be moving or not

    private Rigidbody2D rBody;
    private Transform destination;

    [HideInInspector]
    public Vector3 direction;//direction that the platform is moving in at start

    private void Start() {
        rBody = GetComponent<Rigidbody2D>();
        SetDestination(EndTransform);
    }

    //draws the start and end positions
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(StartTransform.position, new Vector3(gameObject.transform.localScale.x * 3.8f, gameObject.transform.localScale.y * 1.2f, gameObject.transform.localScale.z));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(EndTransform.position, new Vector3(gameObject.transform.localScale.x * 3.8f, gameObject.transform.localScale.y * 1.2f, gameObject.transform.localScale.z));
    }


    private void FixedUpdate() {

        //deltaTime  returns around 0.02, multiplying by this value allows us to work in units/second otherwise it would be units/fixed time step (whatever that is)
        //use MovePosition steps a rigidbody through a number of positions before reaching the final transform position, this ensures that the object does not teleport and any collisions that occur on the way are registered
        //it should also carry the player with it
        if (active) {
            rBody.MovePosition(rBody.position + (new Vector2(direction.x * platformSpeedX, direction.y * platformSpeedY) * Time.fixedDeltaTime));



            if (moveVertically)
            {
                if (Vector3.Distance(platform.position, destination.position) < platformSpeedY * Time.fixedDeltaTime)//we compare it to the right side because that is how far the platform moves in one update
                {
                    SetDestination(destination == StartTransform ? EndTransform : StartTransform);// the ternary operator basically says
                                                                                                  /*
                                                                                                  * if(destination == StartTransform){
                                                                                                  *  destination = EndTransform;
                                                                                                  * }
                                                                                                  * else{
                                                                                                  *  destination = StartTransform;
                                                                                                  * }
                                                                                                  */
                }
            }
            else
            {
                if (Vector3.Distance(platform.position, destination.position) < platformSpeedX * Time.fixedDeltaTime)//we compare it to the right side because that is how far the platform moves in one update
                {
                    SetDestination(destination == StartTransform ? EndTransform : StartTransform);// the ternary operator basically says
                                                                                                  /*
                                                                                                  * if(destination == StartTransform){
                                                                                                  *  destination = EndTransform;
                                                                                                  * }
                                                                                                  * else{
                                                                                                  *  destination = StartTransform;
                                                                                                  * }
                                                                                                  */
                }
            }
        }
        



    }

    private void SetDestination(Transform dest)
    {
        destination = dest;
        direction = (destination.position - platform.position).normalized;
    }

}
