using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Player"))
        {
            collided.GetComponent<Character>().increaseAttackDmg();
            Destroy(this.gameObject);
        }
    }
}
