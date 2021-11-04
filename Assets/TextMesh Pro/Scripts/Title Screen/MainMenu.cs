using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame() {
        Debug.Log("this loads the new game scen");
        /*SceneManager.LoadScene()*/
    }

    public void LoadGame() {
        /*SceneManager.LoadScene()*/
    }

    public void Settings() {
        /*SceneManager.LoadScene()*/
    }

    public void Credits() {
        /*SceneManager.LoadScene()*/
    }

    public void Quit() {
        Application.Quit();
    }

}
