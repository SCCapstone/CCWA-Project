using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*This script manages the main menu transitions with each function name
* being assigned to a button on the main screen.
*/
public class MainMenu : MonoBehaviour
{
    public void NewGame() {
        Variables.newGame = true;
        SceneManager.LoadScene("File Select");
    }

    public void LoadGame() {
        Variables.newGame = false;
        SceneManager.LoadScene("File Select");
    }

    public void Settings() {
        SceneManager.LoadScene("Settings");
    }

    public void Credits() {
        /*SceneManager.LoadScene("Credits")*/
    }

    public void Quit() {
        Application.Quit();
    }

}
