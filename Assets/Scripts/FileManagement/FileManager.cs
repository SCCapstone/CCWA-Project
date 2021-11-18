using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using UnityEngine;

//TODO Make Tests for FileManager
/*
 * Defines the file manager for the game
 * Instance Variables:
 *  FileData[] Files: All the current save files for the game
 *  int CurrFile: The file number of the currently playing file
 *
 * Methods:
 *  Start(): Startup operations (Creates save directory/files if needed)
 *  LoadFile(int filenum): Loads the file associated with filenum; Returns load success
 *  SaveFile(int filenum, FileData data): Saves data to file associated with file number; Returns save success
 *  DeleteFile(int filenum): Deletes file data associated with file number; Returns delete success
 */
public class FileManager : MonoBehaviour
{
    //Load Global Constants
    string SAVE_DIRECTORY = Constants.SAVE_DIRECTORY;
    string SAVE_FILE_BASE_NAME = Constants.SAVE_FILE_BASE_NAME;
    int[] VALID_FILE_NUMS = Constants.VALID_FILE_NUMS;
    string valid_nums = "["; //Local constant based on VALID_FILE_NUMS

    //Declare instance variables
    int files_length;
    private FileData[] Files;
    public int CurrFile;

    //Start is called before the first frame update
    void Start()
    {
        //Initialize instance variables
        int files_length = VALID_FILE_NUMS.Length;
        FileData[] Files = new FileData[VALID_FILE_NUMS.Length];
        int CurrFile = VALID_FILE_NUMS[0];

        //Create saves directory if it doesnt already exist
        if(!System.IO.Directory.Exists(SAVE_DIRECTORY))
        {
            System.IO.Directory.CreateDirectory(SAVE_DIRECTORY);
        }

        //Get string with valid file numbers for error messages
        for(int i = 0; i < VALID_FILE_NUMS.Length; i++) {
            valid_nums += i+",";
        }
        valid_nums.TrimEnd(',');
        valid_nums += "]";

        //Gets all existing saves and creates default files for empty files
        for(int i = 0; i < VALID_FILE_NUMS.Length; i++)
        {
            FileData file = new FileData(VALID_FILE_NUMS[i]);
            if(!System.IO.File.Exists(SAVE_DIRECTORY+SAVE_FILE_BASE_NAME+VALID_FILE_NUMS[i]+".yml"))
            {
                Files[i] = file;
                Files[i].ConvertToYAML();
            } else
            {
                Files[i] = file.LoadFromYAML();
            }
        }
    }

    //Loads the file associated with the filenum
    public bool LoadFile(int filenum)
    {
        //Checks for valid file number
        if(Array.IndexOf(VALID_FILE_NUMS,filenum) > -1)
        {
            CurrFile = filenum;
            FileData fd = new FileData(CurrFile);
            FileData newfd = fd.LoadFromYAML();
            Files[CurrFile] = new FileData(CurrFile,newfd.FastestTime,newfd.NumRuns,
                                        newfd.NumWins,newfd.UnlockedAchievements,
                                        newfd.InRun,newfd.CurrRun);
            return true; //TODO return successful file load
        } else
        {
            Debug.Log("Invalid file number: " + filenum + 
                ". File not loaded. Valid file numbers are: "+valid_nums);
            return false;
        }
    }

    //Saves data to the file associated with the filenum (doubles as overwrite function)
    public bool SaveFile(int filenum, FileData data)
    {
        //Checks for valid file number
        if(Array.IndexOf(VALID_FILE_NUMS,filenum) > -1)
        {
            Files[filenum] = data;
            return Files[filenum].ConvertToYAML();
        } else
        {
            Debug.Log("Invalid file number: " + filenum + 
                ". File not saved. Valid file numbers are: "+valid_nums);
            return false;
        }
    }

    //Deletes file associated with the filenum
    public bool DeleteFile(int filenum)
    {
        //Checks for valid file number
        if(Array.IndexOf(VALID_FILE_NUMS,filenum) > -1)
        {
            return SaveFile(filenum, new FileData(filenum)); //Returns successful delete
        } else
        {
            Debug.Log("Invalid file number: " + filenum + 
                ". File not deleted. Valid file numbers are: "+valid_nums);
            return false;
        }
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
 *  ConvertToYAML(): Converts the file's data to a YAML file; Returns success of saving file
 *  LoadFromYAML(): Loads the file's data from a YAML file; Returns loaded data as FileData
 */
public class FileData
{
    //Load Global Constants
    string[] ALL_ACHIEVEMENTS = Constants.ALL_ACHIEVEMENTS;
    string SAVE_DIRECTORY = Constants.SAVE_DIRECTORY;
    string SAVE_FILE_BASE_NAME = Constants.SAVE_FILE_BASE_NAME;
    int[] VALID_FILE_NUMS = Constants.VALID_FILE_NUMS;

    //Declare instance Variables
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
        //Checks for valid file number
        if(Array.IndexOf(VALID_FILE_NUMS,filenum) > -1)
        {
            FileNum = filenum;
        } else
        {
            FileNum = 1;
        }
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
        //Checks for valid file number
        if(Array.IndexOf(VALID_FILE_NUMS,filenum) > -1)
        {
            FileNum = filenum;
        } else
        {
            FileNum = 1;
        }

        //Checks for valid fastest time
        if(fastesttime < 0)
        {
            FastestTime = int.MaxValue;
        } else
        {
            FastestTime = fastesttime;
        }

        //Checks for valid number of runs
        if(numruns < 0)
        {
            NumRuns = 0;
        } else
        {
            NumRuns = numruns;
        }
        //Checks for valid number of wins
        if(numwins < 0)
        {
            NumWins = 0;
        } else if(numwins > numruns)
        {
            NumWins = numruns;
        } else
        {
            NumWins = numwins;
        }
        
        //Checks for valid unlocked achievements
        List<int> valid_achievement_idx = new List<int>();
        for(int i = 0; i < unlockedachievements.Length; i++)
        {
            if(Array.IndexOf(ALL_ACHIEVEMENTS,unlockedachievements[i]) > -1)
            {
                valid_achievement_idx.Add(i);
            }
        }
        UnlockedAchievements = new string[valid_achievement_idx.Count];
        for(int i = 0; i < valid_achievement_idx.Count; i++)
        {
            UnlockedAchievements[i] = unlockedachievements[valid_achievement_idx[i]];
        }

        if(inrun) {
            CurrRun = currrun; //Validation done in GameState constructor
        } else
        {
            CurrRun = null;
        }
    }

    public bool ConvertToYAML()
    {
        //Saves File data as YAML
        StreamWriter streamWriter = new StreamWriter(SAVE_DIRECTORY+SAVE_FILE_BASE_NAME+FileNum+".yml");
        Serializer serializer = new Serializer();
        serializer.Serialize(streamWriter, this);
        streamWriter.Close();
        return true;
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

    //Declare instance Variables
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

    //Declare instance Variables
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
