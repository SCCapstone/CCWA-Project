using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class SceneTests {

    public void CleanScene()
    {
        foreach (GameObject o in UnityEngine.Object.FindObjectsOfType<GameObject>()) {
            GameObject.Destroy(o);
        }
    }

    [UnityTest]
    public IEnumerator MainMenuTest()
    {
        CleanScene();
        GameObject sm = GameObject.Instantiate(new GameObject("SceneManager"));
        sm.AddComponent<SceneController>();

        sm.GetComponent<SceneController>().MainMenuButton();

        yield return new WaitForSeconds(0f);

        Assert.AreEqual(SceneManager.GetActiveScene().name, "Title Screen");
    }

    [UnityTest]
    public IEnumerator FileSelectTest()
    {
        CleanScene();
        GameObject sm = GameObject.Instantiate(new GameObject("SceneManager"));
        sm.AddComponent<SceneController>();

        sm.GetComponent<SceneController>().FileSelectButton();

        yield return new WaitForSeconds(0f);

        Assert.AreEqual(SceneManager.GetActiveScene().name, "New Run");
    }

    [UnityTest]
    public IEnumerator TutorialTest()
    {
        CleanScene();
        GameObject sm = GameObject.Instantiate(new GameObject("SceneManager"));
        sm.AddComponent<SceneController>();

        sm.GetComponent<SceneController>().TutorialButton();

        yield return new WaitForSeconds(0f);

        Assert.AreEqual(SceneManager.GetActiveScene().name, "Tutorial");
    }

    [UnityTest]
    public IEnumerator SettingsTest()
    {
        CleanScene();
        GameObject sm = GameObject.Instantiate(new GameObject("SceneManager"));
        sm.AddComponent<SceneController>();

        sm.GetComponent<SceneController>().SettingsButton();

        yield return new WaitForSeconds(0f);

        Assert.AreEqual(SceneManager.GetActiveScene().name, "Settings");
    }

    [UnityTest]
    public IEnumerator AchievementsTest()
    {
        CleanScene();
        GameObject sm = GameObject.Instantiate(new GameObject("SceneManager"));
        sm.AddComponent<SceneController>();

        sm.GetComponent<SceneController>().AcheivementsButton();

        yield return new WaitForSeconds(0f);

        Assert.AreEqual(SceneManager.GetActiveScene().name, "Achievements");
    }

    [UnityTest]
    public IEnumerator CreditsTest()
    {
        CleanScene();
        GameObject sm = GameObject.Instantiate(new GameObject("SceneManager"));
        sm.AddComponent<SceneController>();

        sm.GetComponent<SceneController>().CreditsButton();

        yield return new WaitForSeconds(0f);

        Assert.AreEqual(SceneManager.GetActiveScene().name, "Credits");
    }
}