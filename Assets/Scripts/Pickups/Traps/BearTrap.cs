using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    private AudioSource audioSource;


    void Awake() {
        audioSource =GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Player"))
        {
            audioSource.Play(0);
            collided.GetComponent<Character>().DamageHealth(1);
            collided.GetComponent<Character>().ensnaredStatus(2);
            StartCoroutine(bearTrapWaitCoroutine());
            
        }
    }

    public IEnumerator bearTrapWaitCoroutine(){
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
