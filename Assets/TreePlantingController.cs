﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlantingController : MonoBehaviour
{
    public GameObject treePrefab;
    
    private bool inTreePlantArea;
    private bool fPressed;
    private bool planting;

    void Start()
    {
        inTreePlantArea = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Flood"))
        {
            fPressed = true;
        }
        if (Input.GetButtonUp("Flood")) {
            fPressed = false;
            planting = false;
        }

        if (inTreePlantArea && fPressed && !planting) {
            GameObject tree = Instantiate(treePrefab, transform.position, Quaternion.identity);
            tree.transform.SetParent(GameObject.Find("TreeParent").transform);
            tree.GetComponent<TranspiratorScript>().managerObj = GameObject.Find("GameManager");
            planting = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TreePlantArea") {
            inTreePlantArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TreePlantArea")
        {
            inTreePlantArea = false;
        }
    }
}
