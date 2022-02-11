using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    List<GameObject> lootList = new List<GameObject>();
    public GameObject Loot1;
    public GameObject Loot2;

    void Start(){
        lootList.Add(Loot1);
        lootList.Add(Loot2);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Player"))
        {
            if(collided.GetComponent<Character>().useKey()){
                int lootIndex = UnityEngine.Random.Range(0,lootList.Count);
                Transform trans = gameObject.GetComponent<Transform>();
                Instantiate(lootList[lootIndex], trans.position, transform.rotation);
                Destroy(this.gameObject);
            }
            
        }
    }
}
