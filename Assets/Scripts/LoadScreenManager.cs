using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenManager : MonoBehaviour
{
    private ProgressBar loadBar;
    private PauseMenu pMenu;
    private LoadBarRandomizer lBarRandomizer;
    private PlayerInput input;
    private bool loaded;

    private void Start()
    {
        //set bar value and pause the game
        loadBar = this.transform.GetChild(transform.childCount - 1).transform.GetChild(1).GetComponent<ProgressBar>();
        lBarRandomizer = loadBar.GetComponent<LoadBarRandomizer>();
        loadBar.maxValue = 100;

        loaded = false;

        input = GameObject.Find("Player3").GetComponent<PlayerInput>();

        pMenu = this.GetComponent<PauseMenu>();
        pMenu.Pause();
        
    }

    private void Update()
    {
        loadBar.BarValue = lBarRandomizer.getLoadBarVal();

        if (!loaded && loadBar.BarValue == loadBar.maxValue)
        {
            loaded = true;
            pMenu.Resume();
            this.transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
            this.enabled = false;
        }
        

        if (pMenu.isPaused())
        {
            AudioManager.pauseVoiceSource();
            input.setEscapeEnabled(false);

        }
        else {
            AudioManager.unpauseVoiceSource();
            input.setEscapeEnabled(true);
        }
    }

    public bool isLoaded() {
        return loaded;
    }
}
