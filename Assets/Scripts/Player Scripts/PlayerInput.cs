using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)] //makes sure this script runs a frame before the others so that the game does not look laggy at times
public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public float horizontalIn; //horizontal input
    [HideInInspector] public bool jumpPressed;

    bool readyToClear; //controls whether current inputs should be cleared


    private bool escapeEnabled;
    private bool escapePressed;

    // Update is called once per frame DO NOT USE THIS FOR PHYSICS
    //Update accumulates all inputs
    void Update()
    {
        clearInput();//clear existing inputs if necessary

        /* THIS IS FOR LATER SO THAT IF THE PLAYER LOSES ALL MOVEMENT STOPS
        if (GameManager.gameOver) {
            return;
        }*/

        processInput();
        horizontalIn = Mathf.Clamp(horizontalIn, -1f, 1f); //horizontalIn will always be between -1 and 1
    }

    //Used for physics updates
    void FixedUpdate() {
        readyToClear = true;
    }



    private void processInput() {
        horizontalIn += Input.GetAxis("horizontal");
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        escapePressed = Input.GetButtonDown("Cancel");
        
    }

    private void clearInput() {
        if (readyToClear) {
            horizontalIn = 0f;
            jumpPressed = false;
            readyToClear = false;
        }

    }

    public bool isEscapePressed() {
        return escapePressed;
    }

    public bool isEscapeEnabled() {
        return escapeEnabled;
    }

    public void setEscapeEnabled(bool enabled)
    {
        escapeEnabled = enabled;

    }
}
