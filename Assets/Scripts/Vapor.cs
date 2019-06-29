using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vapor : MonoBehaviour
{
    public ParticleSystem steam;
    public ParticleSystem rain;

    public GameObject platform;

    private Rigidbody2D rb;
    Vector3 startPosition;
    private ILevelManagerCondensation manager;
    private void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();

        manager = GameObject.Find("GameManager").GetComponent<ILevelManagerCondensation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "VaporDeathCollider")
        {

            if (platform != null)
            {
                transform.position = platform.transform.position + new Vector3(0, 1f, 0);
            }
            else
            {
                transform.position = startPosition;
            }
            rb.velocity = new Vector2(0, 0);
        }

        if (collision.tag == "CondensationArea")
        {
            manager.SetCondensedVapour(manager.GetCondensedVapour() + 1);

            manager.ChangeCloudColour();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
