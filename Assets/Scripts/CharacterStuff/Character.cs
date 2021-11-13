using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is the base class for all the characters that will be in the game
public class Character : MonoBehaviour
{
    //Variables for characters
    public int health, maxHealth = 6;
    public double stamina, maxStamina = 6.0;
    public float staminaRegenTime = .2f;
    public int attackDmg;
    public int defense;
    public string name;
    public bool isEnhanced = false;
    public float moveSpeed;
    public float baseMoveSpeed = 7f;
    public List<bool> statuses;

    

     
    void Start()
    {
        
    }

    
    void Update()
    {
    
    }

    //Is the base form of attack for the character. Ready to be overriden
    public virtual void Attack() {}

    //Recovers the health of the character by a amount of points
    //Virtual for the health bar
    public virtual void HealHealth (int a){
        health += a;
    }

    //Damages the health of the character by a amount of points
    //Virtual for the health bar
    public void DamageHealth (int a) {
        health -= a;
    }

    //Recovers the stamina by a amount of points
    //Virtual for the stamina bar
    public void StaminaRecover (double a) {
        stamina += a;
    }

    //Function to periodically regenerate stamina. Implement with StartCoroutine("RegenStamina")
    //Virtual for the stamina bar
    public IEnumerator RegenStamina() {
        while (stamina < maxStamina) {
            StaminaRecover(.25);

            //Delays the stamina Regeneration
            yield return new WaitForSeconds(staminaRegenTime);
        }
    }

    //Drains the stamina based on a amount of points
    //Virtual for the stamina bar
    public virtual void StaminaDrain (double a){}

    //Toggles the character's special state if they have one
    public void ToggleEnhanced() {
        isEnhanced = !isEnhanced;
    }

    //Deals with the character running out of health
    //Virtual because multiple things die differently
    public virtual void Die() {} 
    
    
}
