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
    public TileBase noRoomTile;
    
    // Start is called before the first frame update
    void Start()
    {
        floorGenerator = GameObject.Find("Floor Map").GetComponent<FloorGenerator>();
        currFloor = floorGenerator.GetCurrFloor();
    }

    // Update is called once per frame
    void Update()
    {
        currFloor = floorGenerator.GetCurrFloor();
        FloorGenerator.printFloor(currFloor.floorLayout);
        int yoffset = currFloor.floorLayout.GetLength(0);
        int xoffset = currFloor.floorLayout.GetLength(1);
        int minimapTop = 296;
        int minimapRight = 552;
        Room[] rooms = currFloor.rooms;
        int roomHeight = rooms[0].height;
        int roomWidth = rooms[0].width;

        // Figures out the floor layout
        for(int i=0; i<yoffset; i++) {
            for(int j=0; j<xoffset; j++) {
                // Figures out the room layout
                int roomNum = currFloor.floorLayout[i,j];
                if(roomNum > 0)
                {
                    for(int l=0; l<roomHeight; l++) {
                        for(int m=0; m<roomWidth; m++) {
                            int xval = (minimapRight-((xoffset)*roomWidth))+j*roomWidth+l;
                            int yval = (minimapTop-(yoffset)*roomHeight)-(i*roomHeight)+m;
                            if(rooms[roomNum-1].map[l,m] == 0) {
                                miniMap.SetTile(new Vector3Int(xval,yval,0), noRoomTile);
                            } else
                            {
                                if(roomNum-1==floorGenerator.currRoomIdx)
                                {
                                    miniMap.SetTile(new Vector3Int(xval,yval,0), currRoomTile);
                                } else {
                                    miniMap.SetTile(new Vector3Int(xval,yval,0), roomTile);
                                }
                            }
                        }
                    }
                    // if(roomNum == floorGenerator.currRoomIdx+1)
                    // {
                    //     miniMap.SetTile(new Vector3Int((minimapRight-(xoffset-1))+j,minimapTop-i,0), currRoomTile);
                    // } else
                    // {
                    //     miniMap.SetTile(new Vector3Int((minimapRight-(xoffset-1))+j,minimapTop-i,0), roomTile);
                    // }
                }
            }
        }
        // for(int i=0;i<floorGenerator.numRooms;i++)
        // {
        //     for(int l=0; l<roomHeight; l++) {
        //         for(int m=0; m<roomWidth; m++) {
        //             int xval = i*roomWidth+l;
        //             int yval = m;
        //             if(rooms[i].map[l,m] == 0) {
        //                 miniMap.SetTile(new Vector3Int(xval,yval,0), noRoomTile);
        //             } else
        //             {
        //                 if(i==floorGenerator.currRoomIdx)
        //                 {
        //                     miniMap.SetTile(new Vector3Int(xval,yval,0), currRoomTile);
        //                 } else {
        //                     miniMap.SetTile(new Vector3Int(xval,yval,0), roomTile);
        //                 }
        //             }
        //         }
        //     }
        // }
    }
}
