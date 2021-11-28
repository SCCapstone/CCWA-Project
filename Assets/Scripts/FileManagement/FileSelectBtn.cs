using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO Documenation/comments for this code
public class FileSelectBtn : MonoBehaviour
{
    public int filenum;

    void Start()
    {
        GameObject fileSelect = GameObject.Find("FileSelect");
        FileManager fileManager = fileSelect.GetComponent<FileManager>();
        bool success = fileManager.LoadFile(filenum);
        displayData(fileManager.GetFileData(filenum));
    }

    public void loadFile()
    {

    }

    void displayData(FileData fd)
    {
        GameObject totalTimeDisplay =  GameObject.Find("File"+filenum+"Btn/TotalTime");
        string totalTimeStamp = getTimeStamp(fd.TotalTime);
        totalTimeDisplay.GetComponent<UnityEngine.UI.Text>().text = "Total Time "+totalTimeStamp;
    }

    string getTimeStamp(int timeInSeconds)
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
