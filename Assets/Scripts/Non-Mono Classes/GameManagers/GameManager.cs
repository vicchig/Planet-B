using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public abstract class GameManager : MonoBehaviour
{ 
    [Header("Inherited")]
    public GameObject player;
    public GameObject helperChar;
    public GameObject textMesh;
    public GameObject nextLevelMarker;

    protected List<Collider2D> echoColliders;
    protected PlayerAttributes attributes;
    protected HelperCharacter echo;
    protected CheckpointTracker checkpointTracker;
    protected TextMeshProUGUI objectiveDisplay;


    protected virtual void Start()
    {
        echoColliders = new List<Collider2D>();
        attributes = player.GetComponent<PlayerAttributes>();
        echo = helperChar.GetComponent<HelperCharacter>();
        checkpointTracker = this.GetComponent<CheckpointTracker>();
        objectiveDisplay = textMesh.GetComponent<TextMeshProUGUI>();

        resetLevel();
    }

    protected virtual void Update()
    {
        respawnAtCheckPoint();
        levelEchoMsgChecks();
        checkEchoCollisions();
        changeObjectives();
    }

    protected virtual void FixedUpdate()
    {
        findEchoColliders();
    }

    protected void findEchoColliders() {
        BoxCollider2D echoCollider = player.GetComponent<BoxCollider2D>();
        ContactFilter2D filter = new ContactFilter2D
        {
            useTriggers = true
        };

        echoCollider.OverlapCollider(filter, echoColliders);
    }

    protected void respawnAtCheckPoint()
    {
        //if (attributes.GetCurrentHealth() <= 0) {
            //player.transform.position = checkpointTracker.getCurrentCPTransform().position;
            //attributes.SetCurrentHealth(attributes.GetMaxHealth());
        //}

    }

    protected void resetLevel() {
        player.transform.position = checkpointTracker.checkpoints[0].position;
        //attributes.SetCurrentHealth(attributes.GetMaxHealth());
    }

    protected void playerInstantDeath() {
        List<Collider2D> colliders = new List<Collider2D>();
        BoxCollider2D col = player.GetComponent<BoxCollider2D>();
        ContactFilter2D filter = new ContactFilter2D
        {
            useTriggers = true
        };

        col.OverlapCollider(filter, colliders);
        /*
        for (int i = 0; i < colliders.Count; i++) {
            if (colliders[i].tag == "DeathOnTouch") {
                attributes.SetCurrentHealth(0);
            }
        }*/
    }

    protected abstract void levelEchoMsgChecks();
    protected abstract void checkEchoCollisions();
    protected abstract void changeObjectives();
}
