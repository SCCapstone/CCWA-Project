using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Defines the file displayer for the game
 * Methods:
 *  DisplayFiles(): Displays data of all files onto the file select screen
 *  DisplayData(FileData data): Displays the data associated with filenum
 *  GetTimeStamp(int timeInSeconds): Converts a time in seconds to a display timestamp; Returns string timestamp
 */
public class FileDisplayer : MonoBehaviour
{
    void Awake()
    {
        DisplayFiles();
    }

    //Gets the file data for each file and displays it on the file select screen
    public void DisplayFiles()
    {
        int[] VALID_FILE_NUMS = Constants.VALID_FILE_NUMS;
        GameObject fileSelect = GameObject.Find("FileSelect");
        FileManager fileManager = GameObject.Find("FileManager").GetComponent<FileManager>();

        for(int i = 0; i < VALID_FILE_NUMS.Length; i++)
        {
            int filenum = VALID_FILE_NUMS[i];
            DisplayData(fileManager.GetFileData(filenum));
        }
    }

    //Displays the data for a given file
    async void DisplayData(FileData data) //TODO display achievements
    {
        int filenum = data.FileNum;
        GameObject totalTimeDisplay =  GameObject.Find("File"+filenum+"Btn/TotalTime");
        string totalTimeStamp = GetTimeStamp(data.TotalTime);
        totalTimeDisplay.GetComponent<UnityEngine.UI.Text>().text = "Total Time "+totalTimeStamp;

        GameObject fastestRunDisplay =  GameObject.Find("File"+filenum+"Btn/FastestRun");
        string fastestRunStamp = GetTimeStamp(Mathf.FloorToInt(data.FastestTime));
        fastestRunDisplay.GetComponent<UnityEngine.UI.Text>().text = "Fastest Run\t\t"+fastestRunStamp;

        GameObject.Find("File"+filenum+"Btn/DateCreated").GetComponent<UnityEngine.UI.Text>().text = data.DateCreated;

        for(int i = 0; i < data.UnlockedAchievements.Length; ++i)
        {
            if(data.UnlockedAchievements[i] != null)
            {
                Debug.Log("File "+filenum+" has unlocked achievement "+data.UnlockedAchievements[i]);

                //change color of the appropriate game object
                GameObject box = GameObject.Find("File"+filenum+"Btn/Achievements/Achievement"+(i+1)+"/BrownBox");
                Debug.Log(box);
                box.GetComponent<Image>().color = Constants.unlockedAchievementColor;
            }
        }
    }
    //Converts time in seconds to a string timestamp
    string GetTimeStamp(int timeInSeconds)
    {
        //Break up into hours, minutes, seconds
        int[] timeBlocks = new int[3];
        if(timeInSeconds > 3600)
        {
            timeBlocks[0] = timeInSeconds / 3600;
            timeInSeconds -= timeBlocks[0]*3600;
        }
        if(timeInSeconds > 60)
        {
            timeBlocks[1] = timeInSeconds / 60;
            timeInSeconds -= timeBlocks[1]*60;
        }
        timeBlocks[2] = timeInSeconds;

        //Get string representation
        string timeStamp = "";
        for(int i = 0; i < timeBlocks.Length; i++)
        {
            if(timeBlocks[i] < 10)
            {
                timeStamp += "0";
            }
            timeStamp += timeBlocks[i];
            if(i != timeBlocks.Length-1)
            {
                timeStamp +=":";
            }
        }

        return timeStamp;
    }
}
