using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplayDetectorController : MonoBehaviour
{
    public string textToDisplay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            if (transform.parent.tag == "AirSource") {
                GameManagerScript.displayTextAirSource();
            }
        }
    }
}
