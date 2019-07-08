using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    public void playClick() {
        AudioManagerMenu.playButtonClick();
    }

    public void playHover() {
        AudioManagerMenu.playButtonHover();
    }
    
}
