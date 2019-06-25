using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3DynamicParticleScript : MonoBehaviour
{

    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "FreezeCollider") {
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = true;
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
