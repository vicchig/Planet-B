using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranspiratorScript : MonoBehaviour
{
    public float transpirationDelay;
    public GameObject managerObj;
    public int transpirationAmnt;

    private TreeController tController;
    private float transpirationTime;
    private GameManagerLevel4 manager;

    private void Start()
    {
        tController = this.GetComponent<TreeController>();
        manager = managerObj.GetComponent<GameManagerLevel4>();
    }

    private void Update()
    {
        if (transpirationTime >= transpirationDelay) {
            transpirationTime = 0f;
            manager.setTranspirationAmnt(manager.getTranspirationAmnt() + transpirationAmnt);
        }
    }

    private void FixedUpdate()
    {
        if (tController.getStates()[1] == 1) {
            transpirationTime += Time.fixedDeltaTime;
        }
    }
}
