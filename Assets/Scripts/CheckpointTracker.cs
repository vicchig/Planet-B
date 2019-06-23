using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTracker : MonoBehaviour
{
    public Transform[] checkpoints = new Transform[1];

    private int currentCPIndex;

    private void Start()
    {
        currentCPIndex = 0;
    }

    public Transform getCurrentCP() {
        return checkpoints[currentCPIndex];
    }

    public void incrementCPIndex(int increment) {
        currentCPIndex += increment;
    }
}
