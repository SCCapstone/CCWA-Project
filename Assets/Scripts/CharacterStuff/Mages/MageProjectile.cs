using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageProjectile : MonoBehaviour
{
    private Vector3 shootDir;
    // Start is called before the first frame update
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        Destroy(gameObject, 5f);
    }

   
   
    private void Update()
    {
        float moveSpeed = 100f;
        transform.position += shootDir * moveSpeed* Time.deltaTime ;
    }
    
}
