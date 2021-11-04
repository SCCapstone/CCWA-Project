using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame() {
        Debug.Log("this loads the new game scene");
        /*SceneManager.LoadScene("NewCharacter")*/
    }

    public void LoadGame() {
        /*SceneManager.LoadScene("FileSelect")*/
    }

    public void Settings() {
        /*SceneManager.LoadScene("Settings")*/
    }

    public void Credits() {
        /*SceneManager.LoadScene("Credits")*/
    }

    public void Quit() {
        Application.Quit();
    }

}
