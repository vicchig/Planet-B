using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadScreenTipManager : MonoBehaviour
{
    public string level;
    public float tipDuration;
    public bool showTip;

    [Header("Level 1 Tips")]
    public List<string> tips1;

    [Header("Level 2 Tips")]
    public List<string> tips2;

    [Header("Level 2_1 Tips")]
    public List<string> tips2_1;

    [Header("Level 3 Tips")]
    public List<string> tips3;

    [Header("Level 4 Tips")]
    public List<string> tips4;

    [Header("Level 5 Tips")]
    public List<string> tips5;


    private TextMeshProUGUI tipMesh;
    private ProgressBar loadBar;
    private bool tipSet;
    private float tipTimer;

    void Start()
    {
        tipMesh = this.GetComponent<TextMeshProUGUI>();
        tipTimer = tipDuration;
    }

    void Update()
    {
        if (!tipSet && showTip)
        {
            if (level.Equals("1"))
            {
                tipMesh.text = tips1[(int)Random.Range(0, tips1.Count - 1)];
                tips1.Remove(tipMesh.text);
            }
            else if (level.Equals("2"))
            {
                tipMesh.text = tips2[(int)Random.Range(0, tips2.Count - 1)];
                tips2.Remove(tipMesh.text);
            }
            else if (level.Equals("2_1"))
            {
                tipMesh.text = tips2_1[(int)Random.Range(0, tips2_1.Count - 1)];
                tips2_1.Remove(tipMesh.text);
            }
            else if (level.Equals("3"))
            {
                tipMesh.text = tips3[(int)Random.Range(0, tips3.Count - 1)];
                tips3.Remove(tipMesh.text);
            }
            else if (level.Equals("4"))
            {
                tipMesh.text = tips4[(int)Random.Range(0, tips4.Count - 1)];
                tips4.Remove(tipMesh.text);
            }
            else if (level.Equals("5"))
            {
                tipMesh.text = tips5[(int)Random.Range(0, tips5.Count - 1)];
                tips5.Remove(tipMesh.text);
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
