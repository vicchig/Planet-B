using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectiveDisplayScript : MonoBehaviour
{
    [Header("Game Utilities")]
    public GameObject playerObj;
    public GameObject gameManagerObj;
    public GameObject hudObj;

    private PlayerAttributes playerAttributes;
    private GameManagerLevel1 manager;
    private HelperCharacter echo;
    private TextMeshProUGUI textMesh;
    private UIManager uiManager;

    private void Start()
    {
        playerAttributes = playerObj.GetComponent<PlayerAttributes>();
        manager = gameManagerObj.GetComponent<GameManagerLevel1>();
        echo = playerObj.GetComponent<HelperCharacter>();
        textMesh = this.GetComponent<TextMeshProUGUI>();
        uiManager = hudObj.GetComponent<UIManager>();
    }

}
