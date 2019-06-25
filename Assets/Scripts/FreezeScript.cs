using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeScript : MonoBehaviour
{
    public Sprite freezeImage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player") {
            this.GetComponent<SpriteRenderer>().sprite = freezeImage;
        }
    }
}
