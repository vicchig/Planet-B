using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    public GameObject nextLevelMenu;
    // Start is called before the first frame update
    void Start()
    {
        nextLevelMenu.SetActive(false);
    }
    public void LaunchMenu()
    {
        nextLevelMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoToLevel1()
    {
        SceneManager.LoadScene(1);
    }
}
