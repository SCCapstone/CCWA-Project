using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons
{
    public Button newGameBtn;
    public Button loadGameBtn;
    public Button settingsBtn;
    public Button creditsBtn;
    public Button quitBtn;
    // Tests if the new game button works
    [Test]
    public void NewGame()
    {
        string expectedName = "File Select";
        Button btn = newGameBtn.GetComponent<Button>();
        
        btn.onClick.Invoke();
        Assert.AreEqual(expectedName, SceneManager.GetActiveScene().name);
        
    }
}
