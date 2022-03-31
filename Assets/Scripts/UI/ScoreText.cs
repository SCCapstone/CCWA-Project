using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public GameObject score;
    // Start is called before the first frame update
    void Start()
    {
        score.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
