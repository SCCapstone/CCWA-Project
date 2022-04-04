using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public GameObject scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + Variables.score.ToString();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
