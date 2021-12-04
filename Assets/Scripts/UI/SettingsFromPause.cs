using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsFromPause : MonoBehaviour
{
    public GameObject PauseOverlay;
    public GameObject SettingsOverlay;

    public void loadSettings()
    {
        PauseOverlay.SetActive(false);
        SettingsOverlay.SetActive(true);
    }

    public void backToPause()
    {
        PauseOverlay.SetActive(true);
        SettingsOverlay.SetActive(false);
    }
}
