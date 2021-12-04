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
        InvokeRepeating("Chasing", 2.0f, 0.3f);

    }

    void Chasing()
    {

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        pathVectorList = enemyAi.FindPath(GetPostion(), GetTargetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("target pos "+target.position.x + " " + target.position.y + " " + target.position.z);
        if (!(pathVectorList == null) && !(pathVectorList.Count == 0))
        {

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pathVectorList[0], step);
            // Debug.Log("moved towards "+pathVectorList[0].x + " " + pathVectorList[0].y + " " + pathVectorList[0].z);
            if (transform.position == pathVectorList[0])
            {
                // Debug.Log("Made it"+pathVectorList[0].x + " " + pathVectorList[0].y + " " + pathVectorList[0].z);
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