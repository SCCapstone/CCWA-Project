using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var address = new Address
        {
            street = "123 Tornado Alley\nSuite 16",
            city = "East Westville",
            state = "KS"
        };

        StreamWriter streamWriter = new StreamWriter("Test.txt");
        Serializer serializer = new Serializer();
        serializer.Serialize(streamWriter, address);
        streamWriter.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class FileData
{
    public int FileNum { get; private set; }
    public int FastestTime { get; private set; }
    public int NumRuns { get; private set; }
    public int NumWins { get; private set; }
    public string[] UnlockedAchievements { get; private set; }
    public boolean InRun { get; private set; }
    public GameState CurrRun { get; private set; }

    public FileData(int filenum, int fastesttime,
                    int numruns, int numwins,
                    string[] unlockedachievements,
                    boolean inrun, GameState currrun)
    {
        FileNum = filenum;
        FastestTime = fastesttime;
        NumRuns = numruns;
        NumWins = numwins;
        UnlockedAchievements = unlockedachievements;
        InRun = inrun;
        CurrRun = currrun;
    }
    // Add input validation
}

// Add GameState and PlayerState class

