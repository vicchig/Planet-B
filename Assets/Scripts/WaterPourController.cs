using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPourController : MonoBehaviour
{
    [Header("Attributes")]
    public GameObject particleSource;
    public float decrementTime;

    private PlayerAttributes attributes;
    private bool fPressed;
    private ParticleGenerator generator;
    private float pourTime;
    private void Start()
    {
        attributes = GetComponent<PlayerAttributes>();
        generator = particleSource.GetComponent<ParticleGenerator>();
        generator.enabled = false;
        pourTime = 0;
    }

    private void Update()
    {
        if (fPressed && attributes.GetCurrentWater() > 0)
        {
            generator.enabled = true;
            generator.particleForce = new Vector3(50 * this.GetComponent<PlayerMovement>().dir, -20, 0);
            if (pourTime >= decrementTime) {
                attributes.SetCurrentWater(attributes.GetCurrentWater() - 1);
                pourTime = 0;
            }
        }
        else{
            generator.enabled = false;
        }

        if (Input.GetButtonDown("Flood"))
        {
            fPressed = true;
        }
        else if (Input.GetButtonUp("Flood"))
        {
            fPressed = false;
        }
    }

    private void FixedUpdate()
    {
        

        if (fPressed)
        {
            if (pourTime < decrementTime)
            {
                pourTime += Time.deltaTime;
            }
        }
    }

    public bool getFPressed() {
        return fPressed;
    }

}
