using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    public float gravityUp = 0.6f;
    public float gravityDown = 0.5f;

    private PlayerMovement pMovement;
    private Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
        pMovement = this.GetComponent<PlayerMovement>();
        rBody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            if (!pMovement.isOnGround && rBody.velocity.y >= 0)
            {
                Debug.Log("UP");
                rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y - gravityUp);
            }

            else if (!pMovement.isOnGround && rBody.velocity.y < 0)
            {
                Debug.Log("DOWN");
                rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y - gravityDown);
            }
        
    }
}
