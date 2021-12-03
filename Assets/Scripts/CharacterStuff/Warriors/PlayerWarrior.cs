using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*Class for the player character warrior
* ----INPUTS----- 
* w a s d = movement
* j = attack
* u = BERSERK MODE
* esc = pause
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
    //private SpriteRenderer sRenderer;

    //Audio Source for sound effects
    private AudioSource audioSource;

    //Heart counter
    public Image[] hearts;
    public GameObject redHeart;
    public GameObject blackHeart;

    //Stamina counter
    public Image[] staminaIcons;
    public GameObject staminaBottle;
    public GameObject emptystaminaBottle;

    //Counters for the attacking frames
    public float attackTime = .25f;
    public float maxAttackTime = .25f; 

    
    void Awake() {
        base.Awake();
        rigidB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        moveSpeed = baseMoveSpeed;
        berserkTimer = berserkMax;
    }
    
    void Update() {
        AssignWASD();

        //Enters the player character into berserk mode
        if (Input.GetKeyDown("u")) {
            ToggleEnhanced();
            Berserk();
            berserkTimer = berserkMax;
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

        //Allows the user to attack
        attack();

        //Stamina regeneration
        StartCoroutine("RegenStamina");

        //loading the amount of hearts a player has
        loadHearts();

        //loading the amount of stamina a player has
        loadStamina();
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

    //Lets the player character attack
    public void attack() {
        //only allows attack if stamina is above 0
        if (Input.GetKeyDown("j") && stamina >= 2) {
            animator.SetBool("attacking", true);
            audioSource.Play(0);
            attackTime = maxAttackTime;
            StaminaDrain(2);

        }

        //Ends the attack animation after a short period of time
        if (animator.GetBool("attacking")) {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0) {
                animator.SetBool("attacking", false);
            }
        } 
    }

    //Loads the health of the character re  of a character
    public void loadHearts() {
        //Getting the heart objects from the charUIcanvas
        GameObject[] heartHolder = GameObject.FindGameObjectsWithTag("Hearts");
        redHeart = GameObject.FindWithTag("redheart");
        blackHeart = GameObject.FindWithTag("blackheart");

        //sets the heart image array length
        hearts = new Image[maxHealth];
        
        //looping through to get the amount of hearts needed for the character
        for (int i = 0; i < hearts.Length; ++i) {
            hearts[i] = heartHolder[i].GetComponent<Image>();
        }

        //loading the hearts in relation to max health
        for (int i = 0; i < hearts.Length; ++i) {   

            //Enables the amount of hearts needed for max health
            if (i < maxHealth) {
                hearts[i].enabled = true;
            }

            //Enables the amount of hearts for current health
            if (i < health) {
                hearts[i].sprite = redHeart.GetComponent<SpriteRenderer>().sprite;
            } else {
                //Everything greater than health's value is an empty heart
                hearts[i].sprite = blackHeart.GetComponent<SpriteRenderer>().sprite;
            }
        }

        //clears any remaining hearts after the max
        for (int i = maxHealth; i < heartHolder.Length; ++i) {
            heartHolder[i].SetActive(false);
        }
    }

    public void loadStamina() {
        //Getting the stamina objects from the charUIcanvas
        GameObject[] staminaHolder = GameObject.FindGameObjectsWithTag("staminaicon");
        
        staminaBottle = GameObject.FindWithTag("staminapotion");
        emptystaminaBottle = GameObject.FindWithTag("emptypotion");

        //Sets the icon image array length
        staminaIcons = new Image[Convert.ToInt32(maxStamina)];
        
        //Looping through to get the amount of stamina icons needed for the character
        for (int i = 0; i < staminaIcons.Length; ++i) {
            staminaIcons[i] = staminaHolder[i].GetComponent<Image>();
        }

        //Loading the icons in relation to max stamina 
        for (int i = 0; i < staminaIcons.Length; ++i) {
             //Enables the amount of icons needed for max stamina
            if (i < maxStamina) {
                staminaIcons[i].enabled = true;
            }

            //Enables the amount of icons for current stamina
            if (i < stamina) {
                staminaIcons[i].sprite = staminaBottle.GetComponent<SpriteRenderer>().sprite;
            } else {
                //Everything greater than health's value is an empty heart
                staminaIcons[i].sprite = emptystaminaBottle.GetComponent<SpriteRenderer>().sprite;
            }
        }

        //Clears any remaining icons above the max
        for (int i = Convert.ToInt32(maxStamina); i < staminaHolder.Length; ++i) {
            staminaHolder[i].SetActive(false);
        }
    }

    public override void Die() {
        SceneManager.LoadScene("Game Over");
    }
}
