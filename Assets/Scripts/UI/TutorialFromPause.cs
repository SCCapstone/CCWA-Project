using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFromPause : MonoBehaviour
{
    public GameObject PauseOverlay;
    public GameObject TutorialOverlay;

    public void loadTutorial() {
        PauseOverlay.SetActive(false);
        TutorialOverlay.SetActive(true);
        
   }

    public void backToPause() {
        PauseOverlay.SetActive(true);
        TutorialOverlay.SetActive(false);
    } 
}
