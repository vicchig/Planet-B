using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRay : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right.normalized * 3f;
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Platforms") {
            Destroy(gameObject);
        }
    }
}
