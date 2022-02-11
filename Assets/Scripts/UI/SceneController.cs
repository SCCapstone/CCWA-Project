using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // make a variable to keep up with the last scnene useful for main back btn
    public void MainMenuButton()
    {
        SceneManager.LoadScene("Title Screen");
    }

    // used to navigate one scene back. Typically used for menus and the like.
    public void BackButton() {
        
    }

    public void FileSelectButton()
    {
        SceneManager.LoadScene("New Run");
    }

    public void TutorialButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("Settings");
    }

    public void AcheivementsButton()
    {
        SceneManager.LoadScene("Achievements");
    }

    //public void RetryButton(){}

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitButton()
    {
        Application.Quit();
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
