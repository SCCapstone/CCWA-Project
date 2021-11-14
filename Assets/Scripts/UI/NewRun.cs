using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This class handles the inputs for the file select screen
public class NewRun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Takes you back to the file select screen
    public void mainBackBtn() {
        SceneManager.LoadScene("FileSelect");
    }

    public void selectMage() {

    }

    public void selectWarrior() {

    }

    public void selectRogue() {
        
    }
}
