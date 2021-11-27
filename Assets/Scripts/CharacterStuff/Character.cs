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
    public virtual void Attack(Collider2D collision) {}

    //Recovers the health of the character by a amount of points
    //Virtual for the health bar
    public virtual void HealHealth (int a){
        health += a;
        //check that health doesn't go over the max
        if (health > maxHealth) {
            health = maxHealth;
        }
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
        //check that stamina recover doesn't go over max
        if (stamina > maxStamina) {
            stamina  = maxStamina;
        }
    }

    //Function to periodically regenerate stamina. Implement with StartCoroutine("RegenStamina")
    public IEnumerator RegenStamina() {
        while (stamina < maxStamina) {
            StaminaRecover(.005f);

            //Delays the stamina Regeneration
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    //Drains the stamina based on a amount of points
    public void StaminaDrain (double a){
        stamina -= a;
    }

    //Toggles the character's special state if they have one
    public void ToggleEnhanced() {
        isEnhanced = !isEnhanced;
    }

    //Deals with the character running out of health
    //Virtual because multiple things die differently
    public virtual void Die() {} 
    
    
}
