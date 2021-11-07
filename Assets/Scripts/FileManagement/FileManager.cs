using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using UnityEngine;

public const int MAX_HEALTH = 100;
public const int MAX_STAMINA = 100;
public const int MAX_FLOOR_NUM = 10;
public const string[] PLAYER_CLASSES = {"warrior","mage","rogue"};

public class FileManager : MonoBehaviour
{
    // Declare instance variables
    private FileData[] Files = new FileData[3];
    public int CurrFile = 1;

    // Start is called before the first frame update
    void Start()
    {
        
        // StreamWriter streamWriter = new StreamWriter("Test.txt");
        // Serializer serializer = new Serializer();
        // serializer.Serialize(streamWriter, address);
        // streamWriter.Close();
    }

    // Loads the file associated with the filenum
    public bool LoadFile(int filenum) //TODO Add input validation
    {
        CurrFile = filenum;
        return true; //TODO return successful file load
    }

    // Saves data to the file associated with the filenum (doubles as overwrite function)
    public bool SaveFile(int filenum, FileData data) //TODO Add input validation
    {
        Files[filenum] = data;
        Files[filenum].ConvertToYAML();
        return true; //TODO return successful file save
    }

    // Deletes file associated with the filenum
    public bool DeleteFile(int filenum) //TODO Add input validation
    {
        return SaveFile(filenum, new FileData(filenum)); //Returns successful delete
    }

}

/*
 * Defines the file data for a save file
 * Instance Variables:
 *  int FileNum: The number associated with this file's data
 *  int FastestTime: The number of seconds of this file's fastest run completion
 *  int NumRuns: The total number of runs attempted on this file
 *  int NumWins: The total number of runs completed successfully on this file
 *  string[] UnlockedAchievements: List of titles of achievements unlocked on this file
 *  bool InRun: Whether or not the user is in the middle of a run on this file
 *  GameState CurrRun: The game state of the current run on this file (Null if InRun is false)
 *
 * Methods:
 *  FileData(int filenum): Default Constructor
 *  FileData(int filenum, int fastesttime, int numruns,
                    int numwins, string[] unlockedachievements,
                    bool inrun, GameState currrun): Full Constructor
 *  ConvertToYAML(): Converts the file's data to a YAML file
 *  LoadFromYAML(): Loads the file's data from a YAML file
 */
public class FileData
{
    public int FileNum;
    public int FastestTime;
    public int NumRuns;
    public int NumWins;
    public string[] UnlockedAchievements;
    public bool InRun;
    public GameState CurrRun;

    //Default Constructor
    public FileData(int filenum) {
        //TODO make default constructor
        FileNum = filenum;
        FastestTime = 9999999999;
        NumRuns = 0;
        NumWins = 0;
        UnlockedAchievements = new string[];
        InRun = false;
        CurrRun = null;
    }
    //Full Constructor
    public FileData(int filenum, int fastesttime, int numruns,
                    int numwins, string[] unlockedachievements,
                    bool inrun, GameState currrun)
    {
        FileNum = filenum;
        FastestTime = fastesttime;
        NumRuns = numruns;
        NumWins = numwins;
        UnlockedAchievements = unlockedachievements;
        InRun = inrun;
        CurrRun = currrun;
    }
    //TODO Add input validation

    public void ConvertToYAML()
    {
        //TODO save as YAML
    }

    public void LoadFromYAML()
    {
        //Loads based on FileNum
    }
}

/*
 * Defines a current game state
 * Fields:
 *  PlayerState PlayerState: The current state of the player in the game
 *  int FloorNum: The number of the current floor
 *  int FloorSeed: The seed of the current floor
 *
 * Methods:
 *  GameState(): Default Constructor
 *  GameState(int playerhealth, int playerstamina, string playerclass,
            string playerclass, int floornum, int floorseed): Full Constructor
 */
public struct GameState
{
    //Field Variables
    public PlayerState PlayerState;
    public int FloorNum;
    public int FloorSeed;

    //Default Constructor
    public GameState()
    {
        PlayerState = new PlayerState();
        FloorNum = 0;
        FloorSeed = 0;
    }
    //Full Constructor
    public GameState(int playerhealth, int playerstamina,
                    string playerclass, int floornum, int floorseed)
    {
        //Validation done in PlayerState class
        PlayerState = new PlayerState(playerhealth,playerstamina,playerclass);
        
        //Defaults to 0 if not valid
        if(floornum >= 0 && floornum <= MAX_FLOOR_NUM)
        {
            FloorNum = floornum;
        } else
        {
            FloorNum = 0;
        }

        //Any int seed should work so no validation needed
        FloorSeed = floorseed;
    }
}

/*
 * Defines a current player state
 * Fields:
 *  int Health: The player's current health
 *  int Stamina: The player's current stamina
 *  string PlayerClass: The player's class
 *
 * Methods:
 *  PlayerState(): Default Constructor
 *  PlayerState(int health, int stamina, string playerclass): Full Constructor
 */
public struct PlayerState
{
    //Field Variables
    public int Health;
    public int Stamina;
    public string PlayerClass;

    //Default Constructor
    public PlayerState()
    {
        Health = MAX_HEALTH;
        Stamina = MAX_STAMINA;
        PlayerClass = PLAYER_CLASSES[0];
    }
    //Full Constructor
    public PlayerState(int health, int stamina, string playerclass)
    {
        //Defaults to MAX_HEALTH if not valid
        if(health >= 0 && health <= MAX_HEALTH)
        {
            Health = health;
        } else
        {
            Health = MAX_HEALTH;
        }

        //Defaults to MAX_STAMINA if not valid
        if(stamina >= 0 && stamina <= MAX_STAMINA)
        {
            Stamina = stamina;
        } else
        {
            Stamina = MAX_STAMINA;
        }
        
        //Defaults to the first class in PLAYER_CLASSES if not valid
        classIndex = Array.IndexOf(PLAYER_CLASSES,playerclass);
        if(classIndex > -1)
        {
            PlayerClass = playerclass;
        } else
        {
            PlayerClass = PLAYER_CLASSES[0];
        }
        
    }
}
