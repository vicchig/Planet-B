using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    <summary> A message that can be played by Echo in the game and have its text shown on screen.</summary> 
*/
public class EchoMessage
{
    /// <summary> Number of times this message has been played/shown on screen.</summary>
    private int textShows;

    /// <summary> The text of the message.</summary>
    private string text;

    /// <summary> Maximum number of times this message can be played/shown on screen.</summary>
    private int maxTextShows;

    /// <summary> Clip that is played when this message appears on the screen.</summary>
    private AudioClip clip;

    /**
        <summary> Create a new EchoMessage with a given text, audio clip to play and the maximum number of times this message can be shown.</summary> 

        <param name="text"> Text of the message.</param>
        <param name="clip"> Audio clip of the message's text. </param>
        <param name="maxTextShows"> Number of times this text can be shown.</param>
    */
    public EchoMessage(string text, AudioClip clip, int maxTextShows)
    {
        this.text = text;
        this.clip = clip;
        this.maxTextShows = maxTextShows;
        textShows = 0;
    }

    /**
        <summary>Plays this message's clip using the in-game AudioManager </summary>
    */
    public void playMessage() {
        AudioManager.playVoiceClip(this.clip);
    }

    /**
        <summary> Return whether this message has been shown the maximum allowed number of times </summary>
    
        <returns> False if this text can be shown at least one more time, true otherwise </returns>
    */
    public bool maxTextShowsReached() {
        return !(textShows < maxTextShows);
    }

    /**
        <summary> Return the amount of time this text will be played for and shown on screen.</summary> 

        <returns> Float representation of the time in seconds.</returns>
    */
    public float getDuration() {
        return clip.length;
    }

    /**
        <summary> Return the current number of times this message has been shown.</summary>
     
        <returns> An integer representing the number of times this message has been shown. </returns>
    */
    public int getTextShows()
    {
        return textShows;
    }

    /**
        <summary> Increment the number of times this message has been shown by a given integer amount </summary>
    
        <param name="incrementVal"> Amount to increment by.</param>
    */
    public void incrementTextShows(int incrementVal)
    {
        this.textShows = textShows + incrementVal;
    }

    /**
        <summary>Return the text of this message. </summary> 
     
        <returns> A string storing the text of this message.</returns>
    */
    public string getText()
    {
        return text;
    }

    /**
        <summary> Set the text of this message to a given string. </summary>
    
        <param name="text"> New message string to be used.</param>
    */
    public void setText(string text)
    {
        this.text = text;
    }
}
