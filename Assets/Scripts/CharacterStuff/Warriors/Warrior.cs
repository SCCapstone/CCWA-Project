using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base warrior class
public class Warrior : Character
{
    
   

    // Start is called before the first frame update
    void Start()
    {
    //Setting base warrior stats
    health = 8;
    stamina = 3.0;
    attackDmg = 8;
    defense = 6;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
