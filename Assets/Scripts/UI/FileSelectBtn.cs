using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        bool inRun = fileManager.GetFileData(filenum).InRun;
        if(inRun)
        {
            //TODO load gameplay with stored run
        } else
        {
            if(Variables.newGame)
            {
                SceneManager.LoadScene("Tutorial");
            }
            SceneManager.LoadScene("New Run");
        }
    }

    
}
