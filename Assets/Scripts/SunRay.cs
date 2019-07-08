using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SunRay : MonoBehaviour
{
    Rigidbody2D rb;
    public List<GameObject> iceTargets = new List<GameObject>();
    public GameObject waterTarget;
    float speed = 5f;
    public bool targetWater;
    public bool targetIce;

    private ILevelManagerWater waterManager;
    private Transform reflectTarget;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right.normalized * 3f;

        waterManager = GameObject.Find("GameManager").GetComponent<ILevelManagerWater>();

        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < iceTargets.Count; i++) {
            if (iceTargets[i].GetComponent<Level3DynamicParticleScript>().evapLeft <= 0) {
                iceTargets.RemoveAt(i);
            }
        }
    }

    private void chooseTarget() {
        if (targetIce)
        {
            int randomIceIndex = (int)(Random.Range(0, iceTargets.Count));
            reflectTarget = iceTargets[randomIceIndex].transform;
            
        }
        else if(targetWater){
            reflectTarget = waterTarget.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Platforms") {
            Destroy(gameObject);
        }
        else if (collision.tag == "Player")
        {
            AudioManager.playSunrayReflect();
            chooseTarget();
            GoTo(reflectTarget.position);
        }
        else if (collision.tag == "WaterPoolCollider") {
            waterManager.SetEvaporatedWater(waterManager.GetEvaporatedWater() + 1);
            Destroy(gameObject, 0.1f);
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
