using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathColliderLevel2Script : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerAttributes attributes = collision.gameObject.GetComponent<PlayerAttributes>();
        if (attributes != null)
        {
            attributes.SetCurrentHealth(0);
        }
    }
}
