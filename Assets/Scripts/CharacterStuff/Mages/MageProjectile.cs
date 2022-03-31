using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageProjectile : MonoBehaviour
{
   
    private Vector3 shootDir;
    // Start is called before the first frame update
    public void Setup(Vector2 shootDir)
    {
        this.shootDir = shootDir;
        Destroy(gameObject, 5f);
    }

   
   
    private void Update()
    {
        float moveSpeed = 50f;
        transform.position += shootDir * moveSpeed* Time.deltaTime ;
    }
    
}
