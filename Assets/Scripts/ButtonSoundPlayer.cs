using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    public void playClick() {
        AudioManager.playButtonClick();
    }

    public void playHover() {
        AudioManager.playButtonHover();
    }
    
}
