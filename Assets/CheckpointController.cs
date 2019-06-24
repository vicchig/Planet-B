using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public GameObject manager;
    public bool active; //whether this checkpoint should change the player's current checkpoint on collision
    private CheckpointTracker cpTracker;

    private int cpIndex;

    private void Start()
    {
        cpTracker = manager.GetComponent<CheckpointTracker>();
        cpIndex = findCPIndex(this.GetComponent<CheckpointController>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            if (active && cpIndex > cpTracker.getCurrentCPIndex()) {
                cpTracker.setCheckpoint(this.gameObject);
                active = false;
            }
        }
    }

    private int findCPIndex(CheckpointController cp) {
        for (int i = 0; i < cpTracker.checkpoints.Length; i++) {
            if (cp == cpTracker.checkpoints[i].GetComponent<CheckpointController>()) {
                return i;
            }
        }
        return -1;
    }

    public int getCPIndex() {
        return cpIndex;
    }
}
