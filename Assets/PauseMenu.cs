using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;


    private PlayerInput input;

    private void Start()
    {
        input = GameObject.Find("Player3").GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.isEscapePressed() && input.isEscapeEnabled())
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 3f;
        gameIsPaused = false;
        AudioManager.unpauseVoiceSource();
    }
    public void Pause()
    {
        AudioManager.pauseVoiceSource();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public bool isPaused() {
        return gameIsPaused;
    }
}
