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
    private bool activated;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right.normalized * 3f;

        waterManager = GameObject.Find("GameManager").GetComponent<ILevelManagerWater>();
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < iceTargets.Count; i++) {
            if (iceTargets[i] == null || !iceTargets[i].GetComponent<Level3DynamicParticleScript>().isFrozen()) {
                iceTargets.RemoveAt(i);
            }
        }
    }

    private void chooseTarget() {
        
            if (targetIce)
            {
                if (iceTargets.Count > 0)
                {
                    int randomIceIndex = (int)(Random.Range(0, iceTargets.Count));
                    reflectTarget = iceTargets[randomIceIndex].transform;
                }
            }
            else if (targetWater)
            {
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
            

            if ((!(targetIce && iceTargets.Count <= 0) || targetWater) && (targetIce || targetWater)) {
                activated = true;
            }


            if (activated) {
                chooseTarget();
                GoTo(reflectTarget.position);
            }
        }

        if (activated) {
            if (collision.tag == "WaterPoolCollider")
            {
                if (SceneManager.GetActiveScene().name != "Level 3") {
                    waterManager.SetEvaporatedWater(waterManager.GetEvaporatedWater() + 1);

                }
                Destroy(gameObject, 0.1f);
            }
            else if (collision.tag == "DynamicParticleL3" && collision.gameObject.GetComponent<Level3DynamicParticleScript>().isFrozen()) {
                collision.gameObject.GetComponent<Level3DynamicParticleScript>().heatEnergyThreshold -= collision.gameObject.GetComponent<Level3DynamicParticleScript>().heatEnergyIncrement;
                Destroy(gameObject, 0.1f);
            }
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
