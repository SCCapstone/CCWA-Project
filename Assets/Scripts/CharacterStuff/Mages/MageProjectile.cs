using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageProjectile : MonoBehaviour
{
   
    private Vector3 shootDir;
    private int attack;
    private GameObject wallMap;
    // Start is called before the first frame update
    public void Setup(Vector2 shootDir, int attackDmg)
    {
        this.attack=attackDmg;
        this.shootDir = shootDir;
        wallMap = GameObject.Find("Wall Map");
        Destroy(gameObject, 5f);
    }

   
   
    private void Update()
    {
        float moveSpeed = 10f;
        transform.position += shootDir * moveSpeed* Time.deltaTime ;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //GameObject collided = collision.gameObject;
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss") {
            //Gets the instance of the enemy or boss 
            var enemy = collision.gameObject.GetComponent<WarriorEnemy>();
            if (enemy == null) {
                 enemy = collision.gameObject.GetComponent<WarriorBoss>();
            }
            
            //calculating the damage done to the enemy
            int damage = attack - enemy.defense;

            //Damages the enemy's health via the player's attackDmg value
            if (damage <= 0) {
                //mage spells slow enemies
                enemy.slowedStatus(1f);
                //Always do at least one damage to an enemy
                enemy.DamageHealth(1);
            } else {
                //mage spells slow enemies
                enemy.slowedStatus(1f);
                enemy.DamageHealth(damage);
            }
            
            //Kills the enemy if their health is less 0
            if (enemy.health <= 0) {
                enemy.Die();
            }
            
            //updates enemy health bar
            enemy.healthBar.transform.localScale = new Vector3((float)enemy.health / 10, enemy.healthBar.transform.localScale.y, enemy.healthBar.transform.localScale.x);
            Destroy(gameObject);
        }

        // hit a wall
        if(collision.gameObject==wallMap){
            Destroy(gameObject);
        }
    }
    
}
