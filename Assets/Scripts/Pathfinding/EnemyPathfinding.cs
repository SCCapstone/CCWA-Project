using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    private Transform target;
    private int WidthOfGrid = Constants.mapWidth;
    private int HeightOfGrid = Constants.mapHeight;
    

    public int speed;
    
    private Pathfinding enemyAi;
    private List<Vector3> pathVectorList;
    // Start is called before the first frame update
    void Start()
    {
        enemyAi = new Pathfinding(WidthOfGrid, HeightOfGrid);
        pathVectorList = null;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        pathVectorList = enemyAi.FindPath(GetPostion(), GetTargetPosition());
        InvokeRepeating("Chasing", 2.0f, 0.3f);

    }

    void Chasing()
    {
        pathVectorList = enemyAi.FindPath(GetPostion(), GetTargetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if(!(pathVectorList.Count == 0) || (pathVectorList==null))
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pathVectorList[0], step);
            if(transform.position == pathVectorList[0])
            {
                pathVectorList.RemoveAt(0);
            }
        }
            
    }


    private void StopMoving()
    {
        pathVectorList = null;
    }

    public Vector3 GetTargetPosition()
    {
        return target.position;
    }
    public Vector3 GetPostion()
    {
        return transform.position;
    }
}
