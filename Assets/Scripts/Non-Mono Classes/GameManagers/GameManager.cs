using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager : MonoBehaviour
{

    [Header("Inherited")]
    public GameObject player;
    public Vector3 respawnPosition;

    protected List<Collider2D> echoColliders;
    protected PlayerAttributes attributes;

    protected void Start()
    {
        echoColliders = new List<Collider2D>();
        attributes = player.GetComponent<PlayerAttributes>();
    }

    protected void Update()
    {
        findEchoColliders();
        resetLevel();
    }

    protected void findEchoColliders() {
        BoxCollider2D echoCollider = player.GetComponent<BoxCollider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;

        echoCollider.OverlapCollider(filter, echoColliders);
    }

    protected abstract void levelObjectiveChecks();
    protected abstract void checkEchoCollisions();

    protected void resetLevel() {
        if (attributes.GetCurrentHealth() <= 0)
        {
            player.transform.position = respawnPosition;
        }
    }
    
}
