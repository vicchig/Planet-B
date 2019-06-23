using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager : MonoBehaviour
{ 
    [Header("Inherited")]
    public GameObject player;
    public GameObject helperChar;

    protected List<Collider2D> echoColliders;
    protected PlayerAttributes attributes;
    protected HelperCharacter echo;
    protected CheckpointTracker checkpointTracker;

    protected void Start()
    {
        echoColliders = new List<Collider2D>();
        attributes = player.GetComponent<PlayerAttributes>();
        echo = helperChar.GetComponent<HelperCharacter>();
        checkpointTracker = this.GetComponent<CheckpointTracker>();

        resetLevel();
    }

    protected void Update()
    {
        respawnAtCheckPoint();
    }

    protected void FixedUpdate()
    {
        findEchoColliders();
    }

    protected void findEchoColliders() {
        BoxCollider2D echoCollider = player.GetComponent<BoxCollider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;

        echoCollider.OverlapCollider(filter, echoColliders);
    }

    protected void respawnAtCheckPoint()
    {
        if (attributes.GetCurrentHealth() <= 0) {
            player.transform.position = checkpointTracker.getCurrentCPTransform().position;
            attributes.SetCurrentHealth(attributes.GetMaxHealth());
        }

    }

    protected void resetLevel() {
        player.transform.position = checkpointTracker.checkpoints[0].position;
        attributes.SetCurrentHealth(attributes.GetMaxHealth());
        
    }

    protected abstract void levelObjectiveChecks();
    protected abstract void checkEchoCollisions();
}
