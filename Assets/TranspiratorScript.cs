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
    private ILevelManagerTranspiration manager;

    private void Start()
    {
        tController = this.GetComponent<TreeController>();
        manager = managerObj.GetComponent<ILevelManagerTranspiration>();
    }

    private void Update()
    {
        if (transpirationTime >= transpirationDelay) {
            transpirationTime = 0f;
            manager.SetTranspirationAmnt(manager.GetTranspirationAmount() + transpirationAmnt);
        }
    }

    private void FixedUpdate()
    {
        if (tController.getStates()[1] == 1) {
            transpirationTime += Time.fixedDeltaTime;
        }
    }
}
