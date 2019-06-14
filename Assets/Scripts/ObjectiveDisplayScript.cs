using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveDisplayScript : MonoBehaviour
{
    [Header("Game Utilities")]
    public GameObject playerObj;
    public GameObject gameManagerObj;
    public GameObject hudObj;

    private PlayerAttributes playerAttributes;
    private GameManagerScript manager;
    private HelperCharacter echo;
    private TextMeshProUGUI textMesh;
    private UIManager uiManager;

    private void Start()
    {
        playerAttributes = playerObj.GetComponent<PlayerAttributes>();
        manager = gameManagerObj.GetComponent<GameManagerScript>();
        echo = playerObj.GetComponent<HelperCharacter>();
        textMesh = this.GetComponent<TextMeshProUGUI>();
        uiManager = hudObj.GetComponent<UIManager>();
    }


    private void Update()
    {
        if (playerAttributes.GetCurrentWater() * 4 < manager.waterNeededInPool && manager.getAmountOfWaterInPool() <= 0) {
            textMesh.text = "Current Objective: Collect 14 groundwater droplets.";
        }
        else if (playerAttributes.GetCurrentWater() * 4 >= manager.waterNeededInPool) {
            textMesh.text = "Current Objective: Find an area on the surface to create an artificial lake in and fill it with the collected water.";
        }
        else if (manager.getAmountOfWaterInPool() >= manager.waterNeededInPool)
        {
            textMesh.text = "Current Objective: Evaporate the water in the lake using heat energy from the Sun.";
        }
    }
}
