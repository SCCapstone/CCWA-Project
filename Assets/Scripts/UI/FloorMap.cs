using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorMap : MonoBehaviour
{
    private FloorGenerator floorGenerator;
    private Floor currFloor;
    public Tilemap miniMap;
    public TileBase roomTile;
    public TileBase currRoomTile;
    
    // Start is called before the first frame update
    void Start()
    {
        floorGenerator = GameObject.Find("Floor Map").GetComponent<FloorGenerator>();
        currFloor = floorGenerator.GetCurrFloor();
    }

    // Update is called once per frame
    void Update()
    {
        int yoffset = currFloor.floorLayout.GetLength(0);
        int xoffset = currFloor.floorLayout.GetLength(1);
        FloorGenerator.printFloor(currFloor.floorLayout);
        for(int i=0; i<yoffset; i++) {
            for(int j=0; j<xoffset; j++) {
                int room = currFloor.floorLayout[i,j];
                if(room > 0)
                {
                    if(room == floorGenerator.currRoomIdx+1)
                    {
                        miniMap.SetTile(new Vector3Int(j,yoffset-1-i,0), currRoomTile);
                    } else
                    {
                        miniMap.SetTile(new Vector3Int(j,yoffset-1-i,0), roomTile);
                    }
                }
            }
        }
    }
}
