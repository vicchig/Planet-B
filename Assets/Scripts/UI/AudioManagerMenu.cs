using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerMenu : MonoBehaviour
{
    static AudioManagerMenu current;

    [Header("Object Sounds")]
    public AudioClip buttonClick;
    public AudioClip buttonHover;

    [Header("Mixer Groups")]
    public AudioMixerGroup ambientGroup;//The ambient mixer group
    public AudioMixerGroup musicGroup;  //The music mixer group
    public AudioMixerGroup stingGroup;  //The sting mixer group
    public AudioMixerGroup playerGroup; //The player mixer group
    public AudioMixerGroup voiceGroup;  //The voice mixer group


    AudioSource stingSource;            //Reference to the generated sting Audio Source


    void Awake()
    {
        //If an AudioManager exists and it is not this...
        if (current != null && current != this)
        {
            //...destroy this. There can be only one AudioManager
            Destroy(gameObject);
            return;
        }

        //This is the current AudioManager and it should persist between scene loads
        current = this;
        DontDestroyOnLoad(gameObject);

        //Generate the Audio Source "channels" for our game's audio

        stingSource = gameObject.AddComponent<AudioSource>() as AudioSource;


        //Assign each audio source to its respective mixer group so that it is
        //routed and controlled by the audio mixer

        stingSource.outputAudioMixerGroup = stingGroup;


    }


    private void Start()
    {

    }




    public static void playButtonClick()
    {
        if (current == null)
        {
            return;
        }
        current.stingSource.clip = current.buttonClick;
        current.stingSource.Play();
    }

    public static void playButtonHover()
    {
        if (current == null)
        {
            return;
        }
        current.stingSource.clip = current.buttonHover;
        current.stingSource.Play();
    }

  
}
