using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This class handles the inputs for the file select screen
public class NewRun : MonoBehaviour
{
     

    //Takes you back to the file select screen
    public void mainBackBtn() {
        SceneManager.LoadScene("TestingMovement");
    }

    public void selectMage() {

    }

    public void selectWarrior() {

    }

    public void selectRogue() {
        
    }
}
