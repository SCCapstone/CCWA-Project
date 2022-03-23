using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunTimeText : MonoBehaviour
{

    public Text timeUI;
    // Start is called before the first frame update
    void Awake()
    {
        timeUI.GetComponent<UnityEngine.UI.Text>().text ="Runtime: "+Variables.clock.ToString();
    }

  
    // Update is called once per frame
    void Update()
    {
        timeUI.GetComponent<UnityEngine.UI.Text>().text ="Runtime: "+Variables.clock.ToString();
    }
    
}
