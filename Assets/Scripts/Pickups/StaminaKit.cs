using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaKit : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Player"))
        {
            collided.GetComponent<Character>().StaminaRecover(100);
            Destroy(this.gameObject);
        }
    }

}
