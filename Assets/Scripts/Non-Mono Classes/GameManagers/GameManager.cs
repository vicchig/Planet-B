using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager : MonoBehaviour
{ 
    [Header("Inherited")]
    public GameObject player;
    public Vector3 respawnPosition;
    public GameObject helperChar;

    protected List<Collider2D> echoColliders;
    protected PlayerAttributes attributes;
    protected HelperCharacter echo;

    protected void Start()
    {
        echoColliders = new List<Collider2D>();
        attributes = player.GetComponent<PlayerAttributes>();
        echo = helperChar.GetComponent<HelperCharacter>();
    }

    protected void Update()
    {
        resetLevel();
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

    protected void resetLevel() {
        if (attributes.GetCurrentHealth() <= 0)
        {
            player.transform.position = respawnPosition;
            attributes.SetCurrentHealth(attributes.GetMaxHealth());
        }
    }

    protected abstract void levelObjectiveChecks();
    protected abstract void checkEchoCollisions();
}
