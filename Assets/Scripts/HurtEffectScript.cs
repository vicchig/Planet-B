using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEffectScript : MonoBehaviour
{
    public Texture hurtTexture;


    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), hurtTexture, ScaleMode.StretchToFill);
    }
}
