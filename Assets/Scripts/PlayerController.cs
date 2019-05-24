using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float speed = 10.0f;
    public float jumpForce = 1f;
    // Start is called before the first frame update
    void Start()
    {
        

        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /* bool upArrow = false;
        bool downArrow = false;
        bool rightArrow = false;
        bool leftArrow = false;

        Vector2 movement = new Vector2(0, 0);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement += new Vector2(1, 0);
            rightArrow = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement += new Vector2(-1, 0);
            leftArrow = true;
        }
        rigidBody2D.AddForce(movement * speed);

        if (!(upArrow || downArrow || leftArrow || rightArrow))
        {
            rigidBody2D.velocity = Vector2.zero;
            // rigidBody2D.angularVelocity = Vector2.zero;
        } */


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigidbody2d.AddForce(new Vector2(1, 0) * jumpForce);
        }

        rigidbody2d.MovePosition(position);

    }
}
