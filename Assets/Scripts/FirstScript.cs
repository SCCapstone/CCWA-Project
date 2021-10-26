using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position);

        Vector3 testVector = new Vector3(10, 2, -5);

        transform.position += testVector;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
