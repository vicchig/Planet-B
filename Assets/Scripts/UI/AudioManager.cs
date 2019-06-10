using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;

    [Header("Player Sounds")]
    public AudioClip footstepsClip;

    [Header("Mixer Groups")]
    public AudioMixerGroup ambientGroup;//The ambient mixer group
    public AudioMixerGroup musicGroup;  //The music mixer group
    public AudioMixerGroup stingGroup;  //The sting mixer group
    public AudioMixerGroup playerGroup; //The player mixer group
    public AudioMixerGroup voiceGroup;  //The voice mixer group

    AudioSource ambientSource;			//Reference to the generated ambient Audio Source
    AudioSource musicSource;            //Reference to the generated music Audio Source
    AudioSource stingSource;            //Reference to the generated sting Audio Source
    AudioSource playerSource;           //Reference to the generated player Audio Source
    AudioSource voiceSource;            //Reference to the generated voice Audio Source

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
        ambientSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        musicSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        stingSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        voiceSource = gameObject.AddComponent<AudioSource>() as AudioSource;

        //Assign each audio source to its respective mixer group so that it is
        //routed and controlled by the audio mixer
        ambientSource.outputAudioMixerGroup = ambientGroup;
        musicSource.outputAudioMixerGroup = musicGroup;
        stingSource.outputAudioMixerGroup = stingGroup;
        playerSource.outputAudioMixerGroup = playerGroup;
        voiceSource.outputAudioMixerGroup = voiceGroup;

    }


    private void Start()
    {
        
    }

    public static void playVoiceClip(AudioClip clip) {
        if (current == null || current.voiceSource.isPlaying) {
            return;
        }
        current.voiceSource.clip = clip;
        current.voiceSource.Play();
    }

    public static void playFootstepSound() {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.footstepsClip;
        current.playerSource.Play();
    }

}
