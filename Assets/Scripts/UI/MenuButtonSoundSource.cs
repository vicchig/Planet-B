using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonSoundSource : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = new AudioSource();
    }
}
