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

    public static void playIntro1(AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
    public static void playIntro2(AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
    public static void playIntro3(AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
    public static void playWaterFound(AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
    public static void playAirSourceFound(AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
    public static void playDestructibleArea(AudioClip clip) {
        source.clip = clip;
        source.Play();
    }

    public static void stopPlaying() {
        source.Stop();
    }
}
