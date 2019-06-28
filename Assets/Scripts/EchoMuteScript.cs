using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoMuteScript : MonoBehaviour
{

    public void muteEcho() {
        AudioManager.muteVoiceSource();
    }
}
