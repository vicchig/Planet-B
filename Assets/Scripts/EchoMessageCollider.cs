using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoMessageCollider : MonoBehaviour
{
    [Header("Message Attributes")]
    public string txt;
    public AudioClip clip;
    public int maxTxtShows;

    private EchoMessage message;
    private HelperCharacter echo;

    // Start is called before the first frame update
    void Start()
    {
        message = new EchoMessage(txt, clip, maxTxtShows);
        echo = GameObject.Find("HelperCharacter").GetComponent<HelperCharacter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EchoCollider" && !message.maxTextShowsReached() && !echo.isBusy()) {
            echo.addMessage(message);
        }
    }
}
