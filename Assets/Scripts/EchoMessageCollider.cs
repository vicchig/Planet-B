using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoMessageCollider : MonoBehaviour
{
    [Header("Message Attributes")]
    public string txt;
    public AudioClip clip;
    public int maxTxtShows;
    /// <summary>
    /// Whether this collider should wait for Echo to finish all of her current dialogues.
    /// </summary>
    public bool waitUntilFree = true; 

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
        if (waitUntilFree)
        {
            if (collision.tag == "EchoCollider" && !message.maxTextShowsReached() && !echo.isBusy())
            {
                echo.addMessage(message);
            }
        }
        else {
            if (collision.tag == "EchoCollider" && !message.maxTextShowsReached())
            {
                echo.addMessage(message);
            }
        }
        
    }
}
