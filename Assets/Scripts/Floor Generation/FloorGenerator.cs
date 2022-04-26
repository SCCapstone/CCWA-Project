using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO Add documentation
public class FloorGenerator : MonoBehaviour
{
    public int numRooms = 10;
    public int currRoomIdx = 0;
    public Floor currFloor;
    public string seed = "";
    public System.Random rand;
    private RoomGenerator roomGenerator;

    public FloorGenerator(int numRooms, string seed) //TODO add input validation
    {
        this.numRooms = numRooms;
        if(!string.IsNullOrEmpty(seed))
        {
            this.seed = seed;
            roomGenerator = new RoomGenerator(true);
        }
        rand = new System.Random(this.seed.GetHashCode());
    }
    private int[,] GenerateFloorLayout(string seed)
    {
        int[,] floorLayout = new int[this.numRooms, this.numRooms];

        //Starting room set randomly
        int[,] roomCoords = new int[this.numRooms, 2];
        int startCoord = this.rand.Next(0, this.numRooms * this.numRooms);
        floorLayout[startCoord / this.numRooms, startCoord % this.numRooms] = 1;
        roomCoords[0, 0] = startCoord / this.numRooms;
        roomCoords[0, 1] = startCoord % this.numRooms;

        int roomsLeft = numRooms - 1;
        while(roomsLeft != 0)//Get remaining rooms such that they are always adjacent to a previous room
        {
            List<int[]> possiblePos = new List<int[]>();
            for(int c = 0; c < this.numRooms - roomsLeft; c++) //Find all adjacent rooms that arent occupied
            {
                int row = roomCoords[c, 0];
                int col = roomCoords[c, 1];
                for(int i = row - 1; i <= row + 1; i++)
                {
                    for(int j = col - 1; j <= col + 1; j++)
                    {
                        if(i < 0 || i >= this.numRooms || j < 0 || j >= this.numRooms 
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
        for(int i = 0; i < this.numRooms; i++)
        {
            for(int j = 0; j < numRooms; j++)
            {
                if(floorLayout[i, j] != 0)
                {
                    if(j < bounds[0]) //Is it more left
                    {
                        bounds[0] = j;
                    }
                    if(j > bounds[2]) //Is it more right
                    {
                        bounds[2] = j;
                    }
                    if(i < bounds[1]) //Is it higher
                    {
                        bounds[1] = i;
                    }
                    if(i > bounds[3]) //Is it lower
                    {
                        bounds[3] = i;
                    }
                }
            }
        }

        int[,] newFloorLayout = new int[bounds[3] + 1 - bounds[1], bounds[2] + 1 - bounds[0]]; //Make new map with minimal spaces
        for(int i = 0; i < bounds[3] + 1; i++)
        {
            for(int j = 0; j < bounds[2] + 1; j++)
            {
                if(floorLayout[i, j] != 0)
                {
                    newFloorLayout[i - bounds[1], j - bounds[0]] = floorLayout[i, j]; //Adjust position by bounds
                }
            }
        }

        return newFloorLayout;
    }

    
    public string[,] GetRoomExits(int[,] floorLayout)
    {
        string[] directionVals = {"n","s","e","w"};
        string[,] exitFloorLayout = new string[floorLayout.GetLength(0),floorLayout.GetLength(1)];
        for(int i = 0; i < floorLayout.GetLength(0); i++)
        {
            for(int j = 0; j < floorLayout.GetLength(1); j++)
            {
                if(floorLayout[i,j] != 0) //If this cell is a room
                {
                    string exitDirs = ""; //find all adjacent rooms
                    for(int k = i - 1; k <= i + 1; k++)
                    {
                        for(int l = j - 1; l <= j + 1; l++)
                        {
                            //Make sure we are checking within the bounds of floor layout and only checking cardinal directions
                            if(k < 0 || k >= floorLayout.GetLength(0) || l < 0 || l >= floorLayout.GetLength(1) 
                                || (k != i && l != j) || (k == i && l == j) || floorLayout[k, l] == 0)
                            {
                                continue;
                            } else //Current cell is an adjacent room
                            {
                                if(k == i-1) //Current cell is below the room we are checking
                                {
                                    exitDirs += directionVals[0];
                                } else if(k == i+1)//Current cell is above the room we are checking
                                {
                                    exitDirs += directionVals[1];
                                } else if(l == j-1)//Current cell is left of the room we are checking
                                {
                                    exitDirs += directionVals[3];
                                }
                                else//Current cell is right of the room we are checking
                                {
                                    exitDirs += directionVals[2];
                                }
                            }
                        }
                    }
                    exitFloorLayout[i,j] = exitDirs;
                } else
                {
                    exitFloorLayout[i,j] = "0";
                }
            }
        }
        return exitFloorLayout;
    }

    public string[] GetDirectionsFromExitString(string exitString)
    {
        string[] directions = new string[exitString.Length];
        for(int k = 0; k < exitString.Length; k++)
        {
            if(exitString[k].Equals('n'))
            {
                directions[k] = "north";
            }else if(exitString[k].Equals('s'))
            {
                directions[k] = "south";
            }else if(exitString[k].Equals('e'))
            {
                directions[k] = "east";
            }else if(exitString[k].Equals('w'))
            {
                directions[k] = "west";
            }
        }
        return directions;
    }
    
    public Floor GenerateFloor(string seed)
    {
        // We need to ensure that the seed is at least 5 characters long in 
        // a consistent way. So, if the length is less than 5, make the seed
        // a repeated string of itself.
        if(seed.Length < 5) {
            seed = seed+seed+seed;
        }
        int[,] floorLayout = GenerateFloorLayout(seed);
        string[,] exitsLayout = GetRoomExits(floorLayout);
        
        Room[] rooms = new Room[numRooms]; //Generates rooms
        for(int i = 0; i < floorLayout.GetLength(0); i++)
        {
            for(int j = 0; j < floorLayout.GetLength(1); j++)
            {
                if(floorLayout[i,j] != 0)
                {
                    var bossRoom = false;
                    int numEnemies = rand.Next(2,6); //Between 2 and 5 enemies
                    int numItems = rand.Next(1,5); //Between 1 and 4 items
                    string dirs = exitsLayout[i,j];
                    string[] directions = GetDirectionsFromExitString(dirs);
                    if(floorLayout[i,j]-1 == rooms.Length-1)
                    {
                        bossRoom = true;
                    }
                    // If we pass the same seed to each call of GenerateRoom
                    // we will end up with 5-6 of the same room. So,
                    // we generate a new seed substring based on the number of enemies
                    string newSeed = "";
                    if(j > seed.Length) {
                        newSeed = seed.Substring(0);
                    } else {
                        newSeed = seed.Substring(numEnemies);
                    }
                    rooms[floorLayout[i,j]-1] = roomGenerator.GenerateRoom(newSeed,numEnemies,numItems,bossRoom,directions);
                }
            }
        }

        // printExitFloor(exitsLayout);
        
        //Makes the floor with the room and layout
        Floor floor = new Floor(seed, floorLayout, rooms);
        Variables.currFloor = floor;
        return floor;
    }

    public static void printExitFloor(string[,] floorLayout)
	{
        string lineString = "";
		for(int i=0; i < floorLayout.GetLength(0); i++)
		{
			for(int j=0; j < floorLayout.GetLength(1); j++)
			{
                lineString+=floorLayout[i,j]+" ";
			}
            lineString += "\n";
		}
        Debug.Log(lineString);
	}

    public static void printFloor(int[,] floorLayout)
	{
        string lineString = "";
		for(int i=0; i < floorLayout.GetLength(0); i++)
		{
			for(int j=0; j < floorLayout.GetLength(1); j++)
			{
                lineString += floorLayout[i,j]+" ";
			}
			lineString+="\n";
		}
        Debug.Log(lineString);
	}

    public Floor GetCurrFloor()
    {
        return this.currFloor;
    }

    public Room GetCurrRoom()
    {
        return this.GetCurrFloor().rooms[this.currRoomIdx];
    }

    public void NewFloor(int floorNum)
    {
        this.currFloor = GenerateFloor(floorNum + "" + this.seed);
        RoomRenderer renderer = this.gameObject.GetComponent<RoomRenderer>();
        renderer.setCurrentRoom(this.GetCurrRoom());
        renderer.RenderRoom(this.GetCurrRoom());
    }

    // Start is called before the first frame update
    void Awake()
    {   
        if(this.numRooms == 0)
        {
            this.numRooms = 10;
        }
        roomGenerator = new RoomGenerator(true);
        int floorNum = 1;
        if (!string.IsNullOrEmpty(Variables.floorSeed))
        {
            this.seed = Variables.floorSeed;
        } else
        {
            roomGenerator = new RoomGenerator(true);
            // If there is no seed, pick some entirely random number
            this.seed = Random.Range(1, 100000000).ToString();
            FileManager fileManager = GameObject.Find("FileManager").GetComponent<FileManager>();
            if(!Variables.newGame && fileManager.GetFileData(fileManager.CurrFile+1).CurrRun != null)
            {
                this.seed = Variables.floorSeed;
                var player = GameObject.FindWithTag("Player");
                Character character = player.GetComponent<Character>();
                character.health = Variables.playerHealth;
                character.stamina = Variables.playerStamina;
                floorNum = Variables.floorNum;
            }
            
        }
        Variables.floorSeed = this.seed;
        rand = new System.Random(this.seed.GetHashCode());
        this.currFloor = GenerateFloor(floorNum + "" + this.seed);

        if(SceneManager.GetActiveScene().name == "Gameplay")
        {
            RoomRenderer renderer = this.gameObject.GetComponent<RoomRenderer>();            
            renderer.setCurrentRoom(this.GetCurrRoom());
            renderer.RenderRoom(this.GetCurrRoom());
        }
    }
}