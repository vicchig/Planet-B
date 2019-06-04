using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPourController : MonoBehaviour
{

    public GameObject particleSource;

    private PlayerAttributes attributes;
    private bool fPressed;
    private ParticleGenerator generator;
    private void Start()
    {
        attributes = GetComponent<PlayerAttributes>();
        generator = particleSource.GetComponent<ParticleGenerator>();
        generator.enabled = false;
    }

    private void Update()
    {
        if (fPressed && attributes.GetCurrentWater() > 0)
        {
            generator.enabled = true;
            generator.particleForce = new Vector3(150 * this.GetComponent<PlayerMovement>().dir, -20, 0);
            
        }
        else{
            generator.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Flood")) {
            fPressed = true;
        }
        else if (Input.GetButtonUp("Flood")) {
            fPressed = false;
        }
    }

}
