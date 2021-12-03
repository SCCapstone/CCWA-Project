using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemy : Warrior
{
    public GameObject healthBar;
    
    void Awake() {
        base.Awake();
        defense = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Kills the enemy
    public override void Die()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collidedWith) {
        Debug.Log("we caressed under the wonder truck");

        var collision = collidedWith.collider;
        
        if (collision.tag == "Player") {
            //Gets the instance of player
            var playerCharacter = collision.GetComponent<RogueCharacter>();
            
            /*
            Debug.Log(playerCharacter);
            if (playerCharacter == null) {
                
              //  playerCharacter = collision.GetComponent<RogueCharacter>();
            }
            
            
            //Debug.Log(playerCharacter);
            if (Variables.characterType == "warrior") 
            {
                var playerCharacter = collision.GetComponent<PlayerWarrior>();
            }
            */
            //calculating the damage done to player
            int damage = attackDmg - playerCharacter.defense;

            //Damages the player's health via the enemy's attackDmg value
            if (damage <= 0) {
                //Always do at least one damage to a player
                playerCharacter.DamageHealth(1);
            } else {
                playerCharacter.DamageHealth(damage);
            }
            
            //Kills the player if their health is less 0
            if (playerCharacter.health <= 0) {
                playerCharacter.Die();
            }
        }
    }
}