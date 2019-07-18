using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenManager : MonoBehaviour
{
    private ProgressBar loadBar;
    private PauseMenu pMenu;
    private LoadBarRandomizer lBarRandomizer;
    private bool loaded;

    private void Start()
    {
        //set bar value and pause the game
        loadBar = this.transform.GetChild(transform.childCount - 1).transform.GetChild(1).GetComponent<ProgressBar>();
        lBarRandomizer = loadBar.GetComponent<LoadBarRandomizer>();
        loadBar.BarValue = 0;
        loadBar.maxValue = 100;

        loaded = false;

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
        }


        if (pMenu.isPaused())
        {
            AudioManager.pauseVoiceSource();
        }
        else {
            AudioManager.unpauseVoiceSource();
        }
    }
}
