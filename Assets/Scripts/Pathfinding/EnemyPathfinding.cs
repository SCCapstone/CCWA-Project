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
    private List<Vector2> pathVectorList;
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
        float step = speed * Time.deltaTime;
        // Debug.Log("target pos "+target.position.x + " " + target.position.y + " " + target.position.z);
        if (!(pathVectorList == null) && pathVectorList.Count > 0)
        {
            Vector2 moveVector = Vector2.MoveTowards(transform.position, pathVectorList[0], step);
            Vector3 newTransform = new Vector3(moveVector.x, moveVector.y, -1);
            transform.position = newTransform;
            // Debug.Log("moved towards "+pathVectorList[0].x + " " + pathVectorList[0].y + " " + pathVectorList[0].z);
            if (transform.position == (Vector3)pathVectorList[0])
            {
                // Debug.Log("Made it"+pathVectorList[0].x + " " + pathVectorList[0].y + " " + pathVectorList[0].z);
                pathVectorList.RemoveAt(0);
            }
        }
        else
        {
            transform.position=Vector2.MoveTowards(transform.position, target.position, step);
        }

    }


    private void StopMoving()
    {
        pathVectorList = null;
    }

    public Vector2 GetTargetPosition()
    {
        Debug.Log("Position: " + target.position.x + " " + target.position.y);
        return target.position;
    }
    public Vector2 GetPostion()
    {
        return transform.position;
    }
}