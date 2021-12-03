using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{

    public GameObject victory;
    public GameObject failure;
    // Start is called before the first frame update
    void Start()
    {
        loadScreen(Variables.wonGame);
    }

    // Update is called once per frame
    void Update() {}
    public void loadScreen(bool wonGame)
    {
        victory.SetActive(wonGame);
        failure.SetActive(!wonGame);
    }
}
