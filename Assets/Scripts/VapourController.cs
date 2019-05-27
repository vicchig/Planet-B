using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VapourController : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<ParticleSystem>().Stop();
        }
    }
}
    
