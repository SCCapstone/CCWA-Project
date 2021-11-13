using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Class for the player character warrior
* ----INPUTS----- 
* u = BERSERK MODE
* w a s d = movement
* 
*
*----------------
*/
public class PlayerWarrior : Warrior
{
    //Timer for beserk mode and it's maximum time
    public float berserkMax = 10f;
    public float berserkTimer;

    // Start is called before the first frame update
    void Awake()
    {
        moveSpeed = baseMoveSpeed;
        berserkTimer = berserkMax;
    }

    // Update is called once per frame
    void Update()
    {
        //Enters the player character into berserk mode
        if (Input.GetKeyDown("u")) {
            ToggleEnhanced();
        }
        if(isEnhanced) {
            Berserk();
            if (berserkTimer > 0) {
                berserkTimer -= Time.deltaTime;
            } else {
                //Set back to false
                ToggleEnhanced();
                
                //Set back to base stats
                Berserk();

                //reset the timer
                berserkTimer = berserkMax;
            }
        }
        
    }

    
}
