using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public int roomWidth = 25;
    public int roomHeight = 25;

    public int numRooms = 10;
    public string seed = "";
    private RoomGenerator roomGenerator;

    public FloorGenerator(int roomWidth, int roomHeight, int numRooms, string seed) //TODO add input validation
    {
        this.roomWidth = roomWidth;
        this.roomHeight = roomHeight;
        this.numRooms = numRooms;
        roomGenerator = new RoomGenerator(roomWidth,roomHeight);
        if(!string.IsNullOrEmpty(seed))
        {
            this.seed = seed;
            roomGenerator.setUseSeed(true);
            roomGenerator.setSeed(this.seed);
        }
    }

    private int[,] GenerateFloorLayout(string seed)
    {
        int[,] floorLayout = new int[this.numRooms, this.numRooms];

        //Starting room set randomly
        int[,] roomCoords = new int[this.numRooms, 2];
        System.Random rand = new System.Random(this.seed.GetHashCode());
        int startCoord = rand.Next(0, this.numRooms * this.numRooms);
        floorLayout[startCoord / this.numRooms, startCoord % this.numRooms] = 1;
        roomCoords[0, 0] = startCoord / this.numRooms;
        roomCoords[0, 1] = startCoord % this.numRooms;

        int roomsLeft = numRooms - 1;
        while (roomsLeft != 0)//Get remaining rooms such that they are always adjacent to a previous room
        {
            List<int[]> possiblePos = new List<int[]>();
            for (int c = 0; c < this.numRooms - roomsLeft; c++) //Find all adjacent rooms that arent occupied
            {
                int row = roomCoords[c, 0];
                int col = roomCoords[c, 1];
                for (int i = row - 1; i <= row + 1; i++)
                {
                    for (int j = col - 1; j <= col + 1; j++)
                    {
                        if (i < 0 || i >= this.numRooms || j < 0 || j >= this.numRooms 
                            || (i != row && j != col) || floorLayout[i, j] != 0)
                        {
                            continue;
                        }
                        int[] coords = { i, j };
                        possiblePos.Add(coords);
                    }
                }
            }
            //Choose a valid new room at random
            int newRoomIdx = rand.Next(0, possiblePos.Count);
            int[] newRoomCoords = (int[])possiblePos[newRoomIdx];
            roomCoords[this.numRooms - roomsLeft, 0] = newRoomCoords[0];
            roomCoords[this.numRooms - roomsLeft, 1] = newRoomCoords[1];
            floorLayout[newRoomCoords[0], newRoomCoords[1]] = this.numRooms - roomsLeft + 1;
            roomsLeft--;
        }
        
        //Find the bounds of the floor in order to minimize the size of the floor layout
        int[] bounds = { this.numRooms, this.numRooms, 0, 0 };//Default max bounds order of left, top, right, bottom
        for (int i = 0; i < this.numRooms; i++)
        {
            for (int j = 0; j < numRooms; j++)
            {
                if (floorLayout[i, j] != 0)
                {
                    if (j < bounds[0]) //Is it more left
                    {
                        bounds[0] = j;
                    }
                    if (j > bounds[2]) //Is it more right
                    {
                        bounds[2] = j;
                    }
                    if (i < bounds[1]) //Is it higher
                    {
                        bounds[1] = i;
                    }
                    if (i > bounds[3]) //Is it lower
                    {
                        bounds[3] = i;
                    }
                }
            }
        }

        int[,] newFloorLayout = new int[bounds[3] + 1 - bounds[1], bounds[2] + 1 - bounds[0]]; //Make new map with minimal spaces
        for (int i = 0; i < bounds[3] + 1; i++)
        {
            for (int j = 0; j < bounds[2] + 1; j++)
            {
                if (floorLayout[i, j] != 0)
                {
                    newFloorLayout[i - bounds[1], j - bounds[0]] = floorLayout[i, j]; //Adjust position by bounds
                }
            }
        }

        return newFloorLayout;
    }

    public void GenerateFloor(string seed)
    {
        int[,] floorLayout = GenerateFloorLayout(seed);
        //TODO get needed room types (# of and location of exits)
        //TODO generate rooms based on floor layout and room types
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}