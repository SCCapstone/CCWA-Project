using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    //make a variable to keep up with the last scnene useful for main back btn
    public void MainMenuButton()
    {   
        //Clearing any potential scenes
        Variables.menuNavStack.Clear();
        Variables.menuNavStack.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Title Screen");

        //Unpausing the game if quitting from gameplay scenes
        if (Time.timeScale == 0) {
            Time.timeScale = 1;
        }
    }

    //used to navigate one scene back. Typically used for menus and the like.
    public void BackButton() {
        //Throws you to the title screen if there aren't any more scenes in queue. This typically happens when editing
        if (Variables.menuNavStack.Count != 0) {
            SceneManager.LoadScene(Variables.menuNavStack.Pop());
        } else {
            SceneManager.LoadScene("Title Screen");
            Debug.Log("No more Scenes in button queue");
        }
    }

    public void FileSelectButton()
    {
        Variables.menuNavStack.Push(SceneManager.GetActiveScene().name);
        Debug.Log(Variables.menuNavStack.Peek());
        SceneManager.LoadScene("New Run");
    }

    public void TutorialButton()
    {
        Variables.menuNavStack.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Tutorial");
    }

    public void SettingsButton()
    {
        Variables.menuNavStack.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Settings");
    }

    public void AcheivementsButton()
    {
        Variables.menuNavStack.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Achievements");
    }

    //public void RetryButton(){}

    public void CreditsButton()
    {
        Variables.menuNavStack.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Credits");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void NewGame() {
        Variables.newGame = true;
        Variables.floorSeed = "";
        Variables.menuNavStack.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("File Select");
    }

    public void LoadGame() {
        Variables.newGame = false;
        Variables.floorSeed = "";
        Variables.menuNavStack.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("File Select");
    }

    //Not sure if needed thanks to Ian's prexisiting file buttons
    /*public void NewRunButton()
    {
        Variables.newGame = true;
        SceneManager.LoadScene("New Run");
    }

    public void LoadGameButton() {
        Variables.newGame = false;
        SceneManager.LoadScene("File Select");
    }*/

}
