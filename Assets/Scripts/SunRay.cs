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
        if (collision.tag == "Player")
        {
            GameObject gb = GameObject.Find("test");
            GoTo((Vector2)gb.transform.position);
        }
    }

    private void GoTo(Vector2 pos)
    {
        Vector2 v1 = rb.velocity.normalized;
        Vector2 v2 = (pos - (Vector2)transform.position).normalized;
        //Debug.Log(Vector2.Dot(v1, v2));
        float angle = Mathf.Acos(Vector2.Dot(v1, v2));
        transform.Rotate(new Vector3(0,0,angle*180/Mathf.PI));
        //Debug.Log(angle);
        rb.velocity = (pos - (Vector2)transform.position).normalized * 5f;
    }
}
