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
        InvokeRepeating("Chasing", float.PositiveInfinity, 0.3f);
    }

    void Chasing()
    {

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        pathVectorList = enemyAi.FindPath(transform.position, target.position);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (!(pathVectorList == null) && pathVectorList.Count > 0)
        {
            Vector2 moveVector = Vector2.MoveTowards(transform.position, pathVectorList[0], step);
            Vector3 newTransform = new Vector3(moveVector.x, moveVector.y, -1);
            transform.position = newTransform;
            if (transform.position == (Vector3)pathVectorList[0])
            {
                pathVectorList.RemoveAt(0);
            }
        }
        else
        {
            Vector2 moveVector = Vector2.MoveTowards(transform.position, target.position, step);
            Vector3 newTransform = new Vector3(moveVector.x, moveVector.y, -1);
            transform.position = newTransform;
        }
    }


    private void StopMoving()
    {
        pathVectorList = null;
    }
}