using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text scoreUI;
    public GameObject scoreText;
    // Start is called before the first frame update
    void Awake()
    {
        scoreUI.GetComponent<UnityEngine.UI.Text>().text ="Score: "+Variables.score;
    }

  
    // Update is called once per frame
    void Update()
    {
        scoreUI.GetComponent<UnityEngine.UI.Text>().text ="Score: "+Variables.score;
    }
}
