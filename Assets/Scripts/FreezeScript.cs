using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeScript : MonoBehaviour
{
    [Header("Attributes")]
    [Tooltip("Sprite to change to when object freezes")]
    public Sprite freezeImage;
    [Tooltip("Tag of the collider which will freeze this object on collision")]
    public string freezeColliderTag;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == freezeColliderTag) {
            this.GetComponent<SpriteRenderer>().sprite = freezeImage;
        }
    }
}
