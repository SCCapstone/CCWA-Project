using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseUp : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Player"))
        {
            collided.GetComponent<Character>().increaseDefence();
            Destroy(this.gameObject);
        }
    }
}