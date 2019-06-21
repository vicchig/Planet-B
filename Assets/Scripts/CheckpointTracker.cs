using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTracker : MonoBehaviour
{
    public Vector3[] checkpoints = new Vector3[0];

    private int currentCPIndex;

    private void Start()
    {
        currentCPIndex = 0;
    }

    public Vector3 getCurrentCP() {
        return checkpoints[currentCPIndex];
    }

    public void incrementCPIndex(int increment) {
        currentCPIndex += increment;
    }
}
