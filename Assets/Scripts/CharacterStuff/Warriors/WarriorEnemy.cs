using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemy : Warrior
{
    public GameObject healthBar;
    
    void Awake() {
        base.Awake();
        defense = 2;
        DifficutyAdjust();
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
        Debug.Log(collision + " this is the collision name");
        string name = collision.name;
        Debug.Log(name);
        
        switch(name) {
            case "Rogue(Clone)":
                var rogue = collision.GetComponent<RogueCharacter>();
                //calculating the damage done to player
                int rDamage = attackDmg - rogue.defense;
                //Damages the player's health via the enemy's attackDmg value
                if (rDamage <= 0) {
                    //Always do at least one damage to a player
                    rogue.DamageHealth(1);
                } else {
                    rogue.DamageHealth(rDamage);
                }
                //Kills the player if their health is less 0
                if (rogue.health <= 0) {
                    rogue.Die();
                }
            break;

            case "WarriorPrefab(Clone)":
                var warrior = collision.GetComponent<PlayerWarrior>();
                //calculating the damage done to player
                int wDamage = attackDmg - warrior.defense;
                
                //Damages the player's health via the enemy's attackDmg value
                if (wDamage <= 0) {
                    //Always do at least one damage to a player
                    warrior.DamageHealth(1);
                } else {
                    warrior.DamageHealth(wDamage);
                }
                //Kills the player if their health is less 0
                if (warrior.health <= 0) {
                    warrior.Die();
                }
            break;

            case "Mage(Clone)":
                var mage = collision.GetComponent<PlayerMage>();
                //calculating the damage done to player
                int mDamage = attackDmg - mage.defense;
                
                //Damages the player's health via the enemy's attackDmg value
                if (mDamage <= 0) {
                    //Always do at least one damage to a player
                    mage.DamageHealth(1);
                } else {
                    mage.DamageHealth(mDamage);
                }
                //Kills the player if their health is less 0
                if (mage.health <= 0) {
                    mage.Die();
                }
            break;
            }
        }
}

   