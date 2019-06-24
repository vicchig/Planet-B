using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCurrentController : MonoBehaviour
{
    public float force;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "VAPOUR") {
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, force), ForceMode2D.Impulse);
        }
    }
}
