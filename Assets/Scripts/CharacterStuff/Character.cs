using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is the base class for all the characters that will be in the game
public class Character : MonoBehaviour
{
    //Variables for characters
    public int health, maxHealth = 6;
    public double stamina, maxStamina = 6.0;
    public int attackDmg;
    public int defense;
    public string name;
    public bool isEnhanced = false;
    public List<bool> statuses;

    //Timers for enhanced mode and stamina regen
    
     
    void Start()
    {
        
    }

    
    void Update()
    {
    
    }

    //Is the base form of attack for the character. Ready to be overriden
    public virtual void Attack() {}

    //Recovers the health of the character by a amount of points
    public void HealHealth (int a){}

    //Recovers the stamina periodically by a amount of points
    public void StaminaRecover (double a){}

    //Drains the stamina based on a amount of points
    public void StaminaDrain (double a){}

    //Toggles the character's special state if they have one
    public void ToggleEnhanced() {
        isEnhanced = !isEnhanced;
    }

    //Deals with the character running out of health
    public void Die() {} 
    
    
}
