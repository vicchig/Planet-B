using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [Header("Attributes")]
    public float bounceDistance;
    public float floatSpeed;

    private Vector2 originalPos;
    private Rigidbody2D rb;
    private int dir;

    private void Start()
    {
        originalPos = new Vector2(transform.position.x, transform.position.y);
        rb = GetComponent<Rigidbody2D>();
        dir = 1;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (new Vector2(0, floatSpeed * dir)) * Time.deltaTime);
        changeDir();
    }


    private void changeDir()
    {
        if (Vector2.Distance(originalPos, new Vector2(rb.position.x, rb.position.y)) >= bounceDistance)
        {
            dir *= -1;
            originalPos = new Vector2(rb.position.x, rb.position.y);
        }
    }

}
