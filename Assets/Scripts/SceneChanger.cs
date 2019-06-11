﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("Properties")]
    public string sceneName;
    public string sceneChangeTag;

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == sceneChangeTag) {
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
