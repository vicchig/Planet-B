using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporParentScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vapor[] vaporObjects;
    public float darkenInterval = 50f;
    Color currentColor = new Color(255f, 255f, 255f);

    //public void enableRain()
    //{
    //    for (int i = 0; i < vaporObjects.Length; i++)
    //    {
    //        vaporObjects[i].setActiveRain(true);
    //    }
    //}
    //public void darkenClouds()
    //{
    //    currentColor = new Color(currentColor.r - darkenInterval, currentColor.b - darkenInterval, currentColor.g - darkenInterval);
    //    for (int i = 0; i < vaporObjects.Length; i++)
    //    {
    //        if (vaporObjects[i].isCloud)
    //        {
    //            SpriteRenderer sr = vaporObjects[i].gameObject.GetComponent<SpriteRenderer>();
    //            sr.color = currentColor;
    //        }
    //    }
    //}

    public void RefreshCloud (GameObject cloud)
    {
        SpriteRenderer sr = cloud.GetComponent<SpriteRenderer>();
        sr.color = currentColor;
    }
}
