using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloorNumTracker : MonoBehaviour
{
    private TextMeshProUGUI floorNumText;
    // Start is called before the first frame update
    void Start()
    {
        //Get the text object and set the floor number
        floorNumText = gameObject.GetComponent<TextMeshProUGUI>();
        floorNumText.text = "Floor: "+(Variables.floorNum+1);
    }

    // Update is called once per frame
    void Update()
    {
        //Update floor number
        floorNumText.text = "Floor: "+(Variables.floorNum+1);
    }
}
