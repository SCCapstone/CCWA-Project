using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.RepresentationModel;
using UnityEngine;


public class FileManager : MonoBehaviour
{
    string SAVE_DIRECTORY = Constants.SAVE_DIRECTORY;
    // Declare instance variables
    private FileData[] Files = new FileData[3];
    public int CurrFile = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Create saves directory if it doesnt already exist
        if(!System.IO.Directory.Exists(SAVE_DIRECTORY))
        {
            System.IO.Directory.CreateDirectory(SAVE_DIRECTORY);
        }        
    }

    // Loads the file associated with the filenum
    public bool LoadFile(int filenum) //TODO Add input validation
    {
        CurrFile = filenum;
        FileData fd = new FileData(CurrFile);
        FileData newfd = fd.LoadFromYAML();
        Files[CurrFile] = new FileData(CurrFile,newfd.FastestTime,newfd.NumRuns,
                                    newfd.NumWins,newfd.UnlockedAchievements,
                                    newfd.InRun,newfd.CurrRun);
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
 *  FileData(): Default Constructor
 *  FileData(int filenum): Load Constructor used in FileManager
 *  FileData(int filenum, int fastesttime, int numruns,
                    int numwins, string[] unlockedachievements,
                    bool inrun, GameState currrun): Full Constructor
 *  ConvertToYAML(): Converts the file's data to a YAML file
 *  LoadFromYAML(): Loads the file's data from a YAML file
 */
public class FileData
{
    //Load Global Constants
    string SAVE_DIRECTORY = Constants.SAVE_DIRECTORY;
    string SAVE_FILE_BASE_NAME = Constants.SAVE_FILE_BASE_NAME;

    public int FileNum;
    public int FastestTime;
    public int NumRuns;
    public int NumWins;
    public string[] UnlockedAchievements;
    public bool InRun;
    public GameState CurrRun;

    //Default Constructor for YAML Deserializing (used in FileData only)
    public FileData() {
        FileNum = 0;
        FastestTime = int.MaxValue;
        NumRuns = 0;
        NumWins = 0;
        UnlockedAchievements = new string[1];
        InRun = false;
        CurrRun = null;
    }
    //Load Constructor for loading from YAML (used in FileManager)
    public FileData(int filenum) {
        FileNum = filenum;
        FastestTime = int.MaxValue;
        NumRuns = 0;
        NumWins = 0;
        UnlockedAchievements = new string[1];
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
        //Saves File data as YAML
        StreamWriter streamWriter = new StreamWriter(SAVE_DIRECTORY+SAVE_FILE_BASE_NAME+FileNum+".yml");
        Serializer serializer = new Serializer();
        serializer.Serialize(streamWriter, this);
        streamWriter.Close();
    }

    public FileData LoadFromYAML()
    {
        //Loads based on FileNum and returns file data
        Deserializer deserializer = new Deserializer();
        return deserializer.Deserialize<FileData>(File.OpenText(SAVE_DIRECTORY+SAVE_FILE_BASE_NAME+FileNum+".yml"));
    }
}

/*
 * Defines a current game state
 * Instance Variables:
 *  PlayerState PlayerState: The current state of the player in the game
 *  int FloorNum: The number of the current floor
 *  int FloorSeed: The seed of the current floor
 *
 * Methods:
 *  GameState(): Default Constructor
 *  GameState(int playerhealth, int playerstamina, string playerclass,
            string playerclass, int floornum, int floorseed): Full Constructor
 */
public class GameState
{
    //Load Global Constants
    int MAX_FLOOR_NUM = Constants.MAX_FLOOR_NUM;

    //Instance Variables
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
 * Instance Variables:
 *  int Health: The player's current health
 *  int Stamina: The player's current stamina
 *  string PlayerClass: The player's class
 *
 * Methods:
 *  PlayerState(): Default Constructor
 *  PlayerState(int health, int stamina, string playerclass): Full Constructor
 */
public class PlayerState
{
    //Load Global Constants
    int MAX_HEALTH = Constants.MAX_HEALTH;
    int MAX_STAMINA = Constants.MAX_STAMINA;
    string[] PLAYER_CLASSES = Constants.PLAYER_CLASSES;

    //Instance Variables
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
        int classIndex = Array.IndexOf(PLAYER_CLASSES,playerclass);
        if(classIndex > -1)
        {
            PlayerClass = playerclass;
        } else
        {
            PlayerClass = PLAYER_CLASSES[0];
        }
        
    }
}
