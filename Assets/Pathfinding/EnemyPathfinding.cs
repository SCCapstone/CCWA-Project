using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    private const float speed = 40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void StopMoving()
    {
        pathVectorList = null;
    }

    public Vector3 GetPostion()
    {
        return transform.position;
    }
}
