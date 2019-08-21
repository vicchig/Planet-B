using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TutorialMarker : MonoBehaviour
{
    // Start is called before the first frame update
    bool floatUp = true;
    public float floatTime = 0.5f;
    public float speed = 0.05f;
    float floatTimer;

    void Start()
    {
        floatTimer = floatTime;
    }

    // Update is called once per frame
    void Update()
    {
        floatTimer -= Time.deltaTime;
        if (floatTimer < 0)
        {
            floatTimer = floatTime;
            floatUp = !floatUp;
        }
        if (floatUp)
        {
            floatingUp();
        } else
        {
            floatingDown();
        }
    }

    private void floatingUp()
    {
        Vector2 pos1 = transform.position;
        pos1.y += speed;
        transform.position = pos1;
    }

    private void floatingDown()
    {
        Vector2 pos1 = transform.position;
        pos1.y -= speed;
        transform.position = pos1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(1);
        }
    }
}
