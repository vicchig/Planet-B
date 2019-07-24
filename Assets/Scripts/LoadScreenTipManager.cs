using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadScreenTipManager : MonoBehaviour
{
    public string level;
    public float tipDuration;
    public bool showTip;

    private TextMeshProUGUI tipMesh;
    private ProgressBar loadBar;
    private bool tipSet;
    private float tipTimer;

    private List<string> tips1;
    private List<string> tips2;
    private List<string> tips3;
    private List<string> tips4;
    private List<string> tips5;

    void Start()
    {
        tipMesh = this.GetComponent<TextMeshProUGUI>();
        tipTimer = tipDuration;

        tips1 = new List<string>();
        tips2 = new List<string>();
        tips3 = new List<string>();
        tips4 = new List<string>();

        tips1.Add("Evaporation occurs when sunlight heats water in surface bodies of water and it changes state into water vapour.");
        tips1.Add("Water vapour rises in the atmosphere since it is hotter than the surrounding air.");
        tips1.Add("Some water in surface bodies of water comes from groundwater (underground water streams).");

        tips2.Add("Evaporated water vapour rises in the atmosphere and cools down as it does so.");
        tips2.Add("Evaporated water returns to the surface of the planet through precipitation.");
        tips2.Add("Precipiation is the process of water (in any state) falling back down to the surface of the planet.");
        tips2.Add("Precipitation has many forms in addition to rain. It includes any water that falls down to the ground such as rain, snow and hail.");
        tips2.Add("Water vapour rises in the atmosphere and cools down, transitioning back into liquid water. If temperatures are low enough, it can turn into snow or hail.");

        tips3.Add("Collection is an essential part of the water cycle. It is the process of groundwater and water from mountain glaciers (surface runoff) returning back to larger bodies of water such as lake, rivers and oceans.");
        tips3.Add("Collection refers to water going back to surface bodies of water through precipitation and different kinds of runoff such as surface runoff and groundwater seepage.");
        tips3.Add("Collection ensures that the cycle keeps going. If the collection stage of the water cycle did not exist, water would be forever evaporated from surface bodies of water and the surface of plants during the evaporation stage.");

        tips4.Add("Evaporation has two parts, surface water evaporation and transpiration from trees.");
        tips4.Add("Transpiration is not its own individual stage of the cycle. Think of it as a branch of evaporation that has a special name.");
        tips4.Add("Transpiration is the process of water evaporating from the surface of plants.");
    }

    void Update()
    {
        if (!tipSet && showTip)
        {
            if (level.Equals("1") || level.Equals("1_1") || level.Equals("4"))
            {
                tipMesh.text = tips1[(int)Random.Range(0, tips1.Count - 1)];
                tips1.Remove(tipMesh.text);
            }
            else if (level.Equals("2_1") || level.Equals("3"))
            {
                tipMesh.text = tips2[(int)Random.Range(0, tips2.Count - 1)];
                tips2.Remove(tipMesh.text);
            }
            else if (level.Equals("3_1") || level.Equals("5"))
            {
                tipMesh.text = tips3[(int)Random.Range(0, tips3.Count - 1)];
                tips3.Remove(tipMesh.text);
            }
            else if (level.Equals("4_1") || level.Equals("2"))
            {
                tipMesh.text = tips4[(int)Random.Range(0, tips4.Count - 1)];
                tips4.Remove(tipMesh.text);
            }
            tipSet = true;
        }
        else {
            tipTimer -= Time.unscaledDeltaTime;

            if (tipTimer <= 0) {
                tipTimer = tipDuration;
                tipSet = false;
            }
        }
    }
}
