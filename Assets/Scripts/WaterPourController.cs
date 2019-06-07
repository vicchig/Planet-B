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
            generator.particleForce = new Vector3(150 * this.GetComponent<PlayerMovement>().dir, -20, 0);
            if (pourTime >= decrementTime) {
                attributes.SetCurrentWater(attributes.GetCurrentWater() - 1);
            }
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

        if (fPressed)
        {
            if (pourTime < decrementTime)
            {
                pourTime += Time.deltaTime;
            }
            else
            {
                pourTime = 0;
            }
        }
        else {
            pourTime = 0;
        }
    }

    public bool getFPressed() {
        return fPressed;
    }

}
