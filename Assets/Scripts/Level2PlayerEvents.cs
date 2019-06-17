using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2PlayerEvents : MonoBehaviour
{
    Vector3 startingPos;
    private void Start()
    {
        startingPos = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "DeathCollider")
        {
            transform.position = startingPos;
        }
    }
}
