using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Button for deleting a file
 * Instance Variables:
 *  int filenum: The file number corresponding to this button
 *
 * Methods:
 *  DeleteFile(): Deletes the associated file
 */
public class FileDeleteBtn : MonoBehaviour
{
    public int filenum;

    public void DeleteFile() //TODO add user check for deleting files
    {
        GameObject fileSelect = GameObject.Find("FileSelect");
        FileManager fileManager = GameObject.Find("FileManager").GetComponent<FileManager>();
        bool success = fileManager.DeleteFile(filenum);

        FileDisplayer fileDisplayer = fileSelect.GetComponent<FileDisplayer>();
        fileDisplayer.DisplayFiles();
    }

    
}
