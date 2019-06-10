using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    [Header("Audio Source")]
    public AudioSource audioSource;

    public static AudioSource source;

    private void Start()
    {
        source = audioSource;
    }

    public static void playClip(AudioClip clip) {
	if(!source.isPlaying){
		source.PlayOneShot(clip);
	} 
    }
}
