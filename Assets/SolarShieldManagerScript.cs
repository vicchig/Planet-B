using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarShieldManagerScript : MonoBehaviour
{
    Transform solarShield;
    public float timer = 0.4f;
    public float rotationAngle = 90f;

    private float timeRemaining = 0;
    private float degreesPerSecond;
    // Start is called before the first frame update
    void Start()
    {
        solarShield = transform.GetChild(4);
        degreesPerSecond = rotationAngle / timer;
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            solarShield.gameObject.SetActive(true);
            solarShield.Rotate(0f, 0f, Time.deltaTime * degreesPerSecond);
            timeRemaining -= Time.deltaTime;
        } else
        {
            solarShield.gameObject.SetActive(false);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sun Ray") && timeRemaining <= 0)
        {
            solarShield.gameObject.SetActive(true);
            timeRemaining = timer;
        }
    }

}
