using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public GameObject manager;
    public bool incrementOnCollision; //whether the current checkpoint index should be incremented upon collision with this checkpoint
    private CheckpointTracker cpTracker;

    private void Start()
    {
        cpTracker = manager.GetComponent<CheckpointTracker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            if (incrementOnCollision) {
                cpTracker.incrementCPIndex(1);
                incrementOnCollision = false;
            }
        }
        
    }
}
