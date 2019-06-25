using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWeaponBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite weapon1;
    public Sprite weapon2;
    public Sprite weapon3;

    private Image image;


    private void Start()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Weapon 1"))
        {
            image.sprite = weapon1;
        }
        else if (Input.GetButtonDown("Weapon 2"))
        {
            image.sprite = weapon2;
        }
        else if (Input.GetButtonDown("Weapon 3"))
        {
            image.sprite = weapon3;
        }
    }
}
