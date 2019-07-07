using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SunRay : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform reflectTarget;
    float speed = 5f;
    GameObject iceParent;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right.normalized * 3f;
        Destroy(gameObject, 5f);

        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            iceParent = GameObject.Find("IceParent");
        }
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
            if (SceneManager.GetActiveScene().name == "Level 3")
            {
                reflectTarget = iceParent.transform.GetChild(0);
            }
            GoTo(reflectTarget.position);
        }
    }

    private void GoTo(Vector2 pos)
    {
        Vector2 newVelocity = (pos - (Vector2)transform.position).normalized;

        rb.velocity = newVelocity * speed;

        float angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x);
        transform.eulerAngles = new Vector3(0f,0f, angle * Mathf.Rad2Deg);

    }
}
