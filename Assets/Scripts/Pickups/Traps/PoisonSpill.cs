using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Poison Spill Trap
public class PoisonSpill : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Player"))
        {
            collided.GetComponent<Character>().poisonedStatus(3);
            StartCoroutine(poisonSpillWaitCoroutine());
        }
    }

    public IEnumerator poisonSpillWaitCoroutine(){
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
