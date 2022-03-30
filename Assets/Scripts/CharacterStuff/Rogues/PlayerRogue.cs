using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/*Class for the player character warrior
* ----INPUTS----- 
* w a s d = movement
* j = attack
* u = SHADOW MODE
* esc = Pause
*----------------
*/
public class PlayerRogue : Rogue
{
    //Timer for shadow mode and it's maximum time
    public float shadowMax = 10f;
    public float shadowTimer;

    //Rigidbody for the player character and vector for movement directions
    private Rigidbody2D rigidB;
    private Vector2 moveDirection;

    //Animator for sprite animations
    private Animator animator;

    //Audio Source for sound effects
    private AudioSource audioSource;

    //Heart counter
    public Image[] hearts;
    public GameObject redHeart;
    public GameObject blackHeart;

    //Stamina counter
    public Image[] staminaIcons;
    public GameObject staminaBottle;
    public GameObject emptyStaminaBottle;

    //Counters for the attacking frames
    public float attackTime = .35f;
    public float maxAttackTime = .35f;

    public GameObject pauseScreen;
    public GameObject manaBar;

    void Awake() {
        base.Awake();
        rigidB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        pauseScreen = GameObject.FindWithTag("paused");
        pauseScreen.SetActive(false);
        manaBar = GameObject.FindWithTag("manabar");
        manaBar.SetActive(false);
        moveSpeed = baseMoveSpeed;
        shadowTimer = shadowMax;

        name = "rogue";
    }

    void Update() {
        AssignWASD();

     if (Input.GetKeyDown(KeyCode.Escape))
        {
            Variables.isPaused = !Variables.isPaused;
            if (Variables.isPaused) {
                Pause();
            } else {
                Resume();
            }
        }

        //Enters the player character into berserk mode
        if (Input.GetKeyDown("u")) {
            ToggleEnhanced();
            shadowMode();
            shadowTimer = shadowMax;
        }

        if(isEnhanced) {
            if (shadowTimer > 0) {
                shadowTimer -= Time.deltaTime;
            } else {
                //Set back to false
                ToggleEnhanced();
                //Re-enable enemy pathfinding
                shadowMode();
                //reset the timer
                shadowTimer = shadowMax;
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

        if(health <= 0) {
            base.Die();
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

    //Lets the player character attack
    public override void attack() {
        //only allows attack if there are at least two stamina jars
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

    //Loads the visual element of the health
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

    //Loads the visual element of the stamina
    public void loadStamina() {
        //Getting the stamina objects from the charUIcanvas
        GameObject[] staminaHolder = GameObject.FindGameObjectsWithTag("staminaicon");
        
        staminaBottle = GameObject.FindWithTag("staminapotion");
        emptyStaminaBottle = GameObject.FindWithTag("emptypotion");

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
                staminaIcons[i].sprite = emptyStaminaBottle.GetComponent<SpriteRenderer>().sprite;
            }
        }

        //Clears any remaining icons above the max
        for (int i = Convert.ToInt32(maxStamina); i < staminaHolder.Length; ++i) {
            staminaHolder[i].SetActive(false);
        }
    }

    //Pauses the game
    public void Pause() {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    //Resumes the game
    public void Resume() {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
}

