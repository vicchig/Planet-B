using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    
    ParticleSystem ps;
    bool isFiring = false;
    // Start is called before the first frame update
    void Start()
    {
        ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("1");
            isFiring = true;
            ps.Play();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("2");
            ps.Stop();
        }
    }
}
    
