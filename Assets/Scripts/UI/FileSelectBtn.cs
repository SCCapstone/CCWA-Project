using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/*
 * Button for loading a file
 * Instance Variables:
 *  int filenum: The file number corresponding to this button
 *
 * Methods:
 *  LoadFile(): Loads the associated file and progresses to next screen
 */
public class FileSelectBtn : MonoBehaviour
{
    public int filenum;

    public void LoadFile()
    {
        GameObject fileSelect = GameObject.Find("FileSelect");
        FileManager fileManager = GameObject.Find("FileManager").GetComponent<FileManager>();
        bool success = fileManager.LoadFile(filenum);
        if(success)
        {
            Debug.Log("Successfully loaded file "+filenum);
        }

        FileData fd = fileManager.GetFileData(filenum);
        if(fd.DateCreated == "00/00/0000")
        {
            fd.DateCreated = DateTime.Now.ToString("d");
        }
        bool inRun = fd.InRun;
        Debug.Log(fd.DateCreated + " " + fd.InRun);
        if(inRun)
        {
            //TODO load gameplay with stored run
            GameState gs = fd.CurrRun;
            Variables.inRun = true;
            Variables.playerHealth = gs.PlayerState.Health;
            Variables.playerStamina = gs.PlayerState.Stamina;
            Variables.floorNum = gs.FloorNum;
            Variables.floorSeed = gs.FloorSeed;
            SceneManager.LoadScene("Gameplay");
        } else
        {
            SceneManager.LoadScene("New Run");
            Variables.menuNavStack.Push(SceneManager.GetActiveScene().name);
        }
    }

    
}
