using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public GameObject manager;

    private CheckpointTracker cpTracker;

    private void Start()
    {
        cpTracker = manager.GetComponent<CheckpointTracker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            cpTracker.incrementCPIndex(1);
            Destroy(gameObject);
        }
        
    }
}
