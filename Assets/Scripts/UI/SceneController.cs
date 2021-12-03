using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Title Screen");
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

    public void RetryButton(){}

    public void CreditsButton()
    {
        /*SceneManager.LoadScene("Credits")*/
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void NewRunButton()
    {
        SceneManager.LoadScene("New Run");
    }
}
