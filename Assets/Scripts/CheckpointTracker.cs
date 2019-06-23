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

    public void Update()
    {
        Debug.Log(currentCPIndex);
    }

    public Transform getCurrentCPTransform() {
        return checkpoints[currentCPIndex];
    }

    public int getCurrentCPIndex() {
        return currentCPIndex;
    }

    public void setCheckpoint(GameObject cp) {
        currentCPIndex = cp.GetComponent<CheckpointController>().getCPIndex();
    }
   
}
