using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEffectScript : MonoBehaviour
{
    public Texture hurtTexture;

    private PlayerAttributes pAttributes;

    private void Start()
    {
        pAttributes = this.GetComponent<PlayerAttributes>();

    }



    private void OnGUI()
    {
        if (pAttributes.isTakingDamage()) {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), hurtTexture, ScaleMode.StretchToFill);
        }
    }
}
