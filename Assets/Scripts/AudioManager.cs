using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Sounds")]
    public AudioClip intro1Sound;
    public AudioClip intro2Sound;
    public AudioClip intro3Sound;
    public AudioClip waterFoundSound;
    public AudioClip airSourceFoundSound;
    public AudioClip destructibleAreaFoundSound;


    public static AudioSource source;

    public static AudioClip intro1;
    public static AudioClip intro2;
    public static AudioClip intro3;
    public static AudioClip waterFound;
    public static AudioClip airSourceFound;
    public static AudioClip destructibleAreaFound;


    private void Start()
    {
        source = audioSource;
        intro1 = intro1Sound;
        intro2 = intro2Sound;
        intro3 = intro3Sound;
        waterFound = waterFoundSound;
        airSourceFound = airSourceFoundSound;
        destructibleAreaFound = destructibleAreaFoundSound;
    }

    public static void playIntro1() {
        source.clip = intro1;
        source.Play();
    }
    public static void playIntro2() {
        source.clip = intro2;
        source.Play();
    }
    public static void playIntro3() {
        source.clip = intro3;
        source.Play();
    }
    public static void playWaterFound() {
        source.clip = waterFound;
        source.Play();
    }
    public static void playAirSourceFound() {
        source.clip = airSourceFound;
        source.Play();
    }
    public static void playDestructibleArea() {
        source.clip = destructibleAreaFound;
        source.Play();
    }

    public static void stopPlaying() {
        source.Stop();
    }
}
