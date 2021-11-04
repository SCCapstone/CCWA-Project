using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using UnityEngine;

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

public class FileData
{
    public int FileNum { get; private set; }
    public int FastestTime { get; private set; }
    public int NumRuns { get; private set; }
    public int NumWins { get; private set; }
    public string[] UnlockedAchievements { get; private set; }
    public bool InRun { get; private set; }
    public GameState CurrRun { get; private set; }

    public FileData(int filenum) {
        //TODO make default constructor
    }

    public FileData(int filenum, int fastesttime,
                    int numruns, int numwins,
                    string[] unlockedachievements,
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

public class GameState
{
    public PlayerState PlayerState { get; private set; }
    public int FloorNum { get; private set; }
    public int FloorSeed { get; private set; }
    public GameState(PlayerState playerstate, int floornum, int floorseed)
    {
        PlayerState = playerstate;
        FloorNum = floornum;
        FloorSeed = floorseed;
    }
    //TODO Add input validation
}

public class PlayerState
{
    public int Health { get; private set; }
    public int Stamina { get; private set; }
    public string PlayerClass { get; private set; }

    public PlayerState(int health, int stamina, string playerclass)
    {
        Health = health;
        Stamina = stamina;
        PlayerClass = playerclass;
    }
    //TODO Add input validation
}
