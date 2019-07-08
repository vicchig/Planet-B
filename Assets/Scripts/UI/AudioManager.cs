using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;

    [Header("Player Sounds")]
    public AudioClip[] footstepsClips; //selection of footstep sounds

    [Header("Object Sounds")]
    public AudioClip airSourcePop; //played when air source is picked up
    public AudioClip waterDropPop; //played when you pick up a water droplet
    public AudioClip buttonClick;
    public AudioClip buttonHover;
    public AudioClip shoot;
    public AudioClip rain;
    public AudioClip waterSplash;
    public AudioClip bubbles;
    public AudioClip sunrayReflect;

    [Header("Mixer Groups")]
    public AudioMixerGroup ambientGroup;//The ambient mixer group
    public AudioMixerGroup musicGroup;  //The music mixer group
    public AudioMixerGroup stingGroup;  //The sting mixer group
    public AudioMixerGroup playerGroup; //The player mixer group
    public AudioMixerGroup voiceGroup;  //The voice mixer group
    public AudioMixerGroup effectGroup;

    AudioSource ambientSource;			//Reference to the generated ambient Audio Source
    AudioSource musicSource;            //Reference to the generated music Audio Source
    AudioSource stingSource;            //Reference to the generated sting Audio Source
    AudioSource playerSource;           //Reference to the generated player Audio Source
    AudioSource effectSource;
    public static AudioSource voiceSource;            //Reference to the generated voice Audio Source




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
        
        //Generate the Audio Source "channels" for our game's audio
        ambientSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        musicSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        stingSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        voiceSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        effectSource = gameObject.AddComponent<AudioSource>() as AudioSource;

        //Assign each audio source to its respective mixer group so that it is
        //routed and controlled by the audio mixer
        ambientSource.outputAudioMixerGroup = ambientGroup;
        musicSource.outputAudioMixerGroup = musicGroup;
        stingSource.outputAudioMixerGroup = stingGroup;
        playerSource.outputAudioMixerGroup = playerGroup;
        voiceSource.outputAudioMixerGroup = voiceGroup;
        effectSource.outputAudioMixerGroup = effectGroup;

    }


    private void Start()
    {
        
    }

    public static void playVoiceClip(AudioClip clip) {
        if (current == null || voiceSource.isPlaying) {
            return;
        }
        voiceSource.clip = clip;
        voiceSource.Play();
    }

    public static void playFootstepSound() {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }

        int rand = (int)Random.Range(0, 2);

        current.playerSource.clip = current.footstepsClips[rand];
        current.playerSource.Play();
    }


    public static void playAirSourcePop()
    {
        if (current == null || current.stingSource.isPlaying)
        {
            return;
        }

        current.stingSource.clip = current.airSourcePop;
        current.stingSource.Play();
    }

    public static void playWaterDropPop()
    {
        if (current == null)
        {
            return;
        }
        current.stingSource.clip = current.waterDropPop;
        current.stingSource.Play();
    }

    public static void muteVoiceSource() {
        voiceSource.mute = !voiceSource.mute;
    }

    public static void pauseVoiceSource() {
        voiceSource.Pause();
    }

    public static void unpauseVoiceSource() {
        voiceSource.UnPause();
    }

    public static void playButtonClick() {
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

    public static void playShoot() {
        if (current == null)
        {
            return;
        }
        current.stingSource.clip = current.shoot;
        current.stingSource.Play();
    }

    public static void playRain()
    {
        if (current == null || current.ambientSource.isPlaying)
        {
            return;
        }
        current.ambientSource.clip = current.rain;
        current.ambientSource.Play();
        current.ambientSource.loop = true;
    }

    public static void playSplash()
    {
        if (current == null)
        {
            return;
        }
        current.stingSource.clip = current.waterSplash;
        current.stingSource.Play();
    }

    public static void playBubbles()
    {
        if (current == null || current.ambientSource.isPlaying)
        {
            return;
        }
        current.ambientSource.clip = current.bubbles;
        current.ambientSource.Play();
        current.ambientSource.loop = true;
    }

    public static void playSunrayReflect()
    {
        if (current == null)
        {
            return;
        }
        current.effectSource.clip = current.sunrayReflect;
        current.effectSource.Play();
    }
}
