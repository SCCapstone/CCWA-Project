using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RunTimeText : MonoBehaviour
{

    public Text timeUI;
    // Start is called before the first frame update
    void Awake()
    {
        timeUI.GetComponent<UnityEngine.UI.Text>().text ="Runtime: "+Math.Round(Variables.clock,2);
    }

  
    // Update is called once per frame
    void Update()
    {
        timeUI.GetComponent<UnityEngine.UI.Text>().text ="Runtime: "+Math.Round(Variables.clock,2);
    }
    
}
