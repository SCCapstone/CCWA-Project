using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Class for the player character warrior
* ----INPUTS----- 
* w a s d = movement
* j = attack
* u = BERSERK MODE
*
*----------------
*/
public class PlayerWarrior : Warrior
{
    //Timer for beserk mode and it's maximum time
    public float berserkMax = 10f;
    public float berserkTimer;

    //Rigidbody for the player character and vector for movement directions
    private Rigidbody2D rigidB;
    private Vector2 moveDirection;

    //Animator and sprite renderer for sprite animations
    private Animator animator;
    private SpriteRenderer sRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        rigidB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        moveSpeed = baseMoveSpeed;
        berserkTimer = berserkMax;
    }

    // Update is called once per frame
    void Update()
    {
        AssignWASD();

        //Enters the player character into berserk mode
        if (Input.GetKeyDown("u")) {
            ToggleEnhanced();
            Berserk();
        }

        if(isEnhanced) {

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

        //Let's the player character attack
        if (Input.GetKeyDown("j")) {
            
        }
    }

    void FixedUpdate() {
        Move();
       
    }

    //Getting and setting the user inputs for movement
    void AssignWASD() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX,moveY).normalized;

        //Setting the sprite aniamtions
        //Controls the parameters in the blend tree based on the values entered from the moveDirection vector
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
    }

    //Function that allows the player character to move with WASD
    void Move() {
        rigidB.velocity = new Vector2 (moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        //updating the sprite to face the correct way when not moving by setting the idle blend tree values to last input value
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }
    }

    //puts out a damaging hitbox in front of the player
    public override void Attack(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Boss") {
            //Gets the instance of the enemy or boss 
            var enemy = collision.GetComponent<Character>();
            
            //calculating the damage done to the enemy
            int damage = attackDmg - enemy.defense;

            //Damages the enemy's health via the player's attackDmg value
            if (damage <= 0) {
                //Always do at least one damage to an enemy
                enemy.DamageHealth(1);
            } else {
                enemy.DamageHealth(damage);
            }
            
            //Kills the enemy if their health is less 0
            if (enemy.health < 0) {
                enemy.Die();
            }
        }

        //assign the hitboxes to animations or something
    }

    public override void Die() {
        SceneManager.LoadScene("Game Over");
    }
}
