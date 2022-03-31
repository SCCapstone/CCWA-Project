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
* u = JUICED
* esc = Pause
*----------------
*/

public class PlayerMage : Mage
{
    //Timer for shadow mode and it's maximum time
    public float juicedMax = 5f;
    public float juiceTimer;

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
    public GameObject staminabar;
    public GameObject MageProjectile;

    //Mana counter
    public Image [] manaIcons;
    public GameObject manaBottle;
    public GameObject emptyManaBottle;

    //Counters for the attacking frames
    public float attackTime = .35f;
    public float maxAttackTime = .35f;

    public GameObject pauseScreen;
    public GameObject manaBar;
    void Awake () {
        base.Awake();
        rigidB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        pauseScreen = GameObject.FindWithTag("paused");
        pauseScreen.SetActive(false);
        staminabar = GameObject.FindWithTag("staminabar");
        staminabar.SetActive(false);
        manaBar = GameObject.FindWithTag("manabar");
        moveSpeed = baseMoveSpeed;
        juiceTimer = juicedMax;
        name = "mage";
    }

    // Update is called once per frame
    void Update()
    {
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

        //Enters the player character into juiced mode
        if (Input.GetKeyDown("u")) {
            ToggleEnhanced();
            Juiced();
            juiceTimer = juicedMax;
        }

        if(isEnhanced) {
            if (juiceTimer > 0) {
                juiceTimer -= Time.deltaTime;
            } else {
                //Set back to false
                ToggleEnhanced();
                //Set back to base mana
                Juiced();
                //reset the timer
                juiceTimer = juicedMax;
            }
        }

        //Allows the user to attack
        attack();

        //Stamina regeneration
        StartCoroutine("RegenStamina");

        //Mana regeneration
        StartCoroutine("RegenMana");

        //loading the amount of hearts a player has
        loadHearts();

        //loading the amount of stamina a player has
        //loadStamina();

        //loading the amount of mana a player has
        loadMana();

        if(health <= 0) {
            base.Die();
        }
    }

    void FixedUpdate() {
        Move();
    }

    //Getting and setting the user inputs for movement
    void Move() {
        rigidB.velocity = new Vector2 (moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        //updating the sprite to face the correct way when not moving by setting the idle blend tree values to last input value
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }
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

    //Lets the player character attack
    public override void attack() {
        //only allows attack if there are at least two mana jars

        if (Input.GetKeyDown("j") && mana >= 2) {
            Vector2 characterLoc=gameObject.transform.position;
            animator.SetBool("attacking", true);
            audioSource.Play(0);
            attackTime = maxAttackTime;
            ShootBullet(characterLoc);
            ManaDrain(2);

            //TODO may need to add bullet functionality
        }

        //Ends the attack animation after a short period of time
        if (animator.GetBool("attacking")) {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0) {
                animator.SetBool("attacking", false);
            }
        }
    }

    //Loads the health of the character of a character
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

    //Loads the mana of the character
    public void loadMana() {
        //Enabling the mana bar for the mage
        manaBar.SetActive(true);

        //Getting the mana objects from the charUIcanvas
        GameObject[] manaHolder = GameObject.FindGameObjectsWithTag("manaicon");
        
        manaBottle = GameObject.FindWithTag("manapotion");
        emptyManaBottle = GameObject.FindWithTag("emptymanapotion");

        //Sets the icon image array length
        manaIcons = new Image[Convert.ToInt32(maxMana)];
        
        //Looping through to get the amount of mana icons needed for the character
        for (int i = 0; i < manaIcons.Length; ++i) {
            manaIcons[i] = manaHolder[i].GetComponent<Image>();
        }

        //Loading the icons in relation to max mana 
        for (int i = 0; i < manaIcons.Length; ++i) {
             //Enables the amount of icons needed for max stamina
            if (i < maxMana) {
                manaIcons[i].enabled = true;
            }

            //Enables the amount of icons for current mana
            if (i < mana) {
                manaIcons[i].sprite = manaBottle.GetComponent<SpriteRenderer>().sprite;
            } else {
                //Everything greater than health's value is an empty heart
                manaIcons[i].sprite = emptyManaBottle.GetComponent<SpriteRenderer>().sprite;
            }
        }

        //Clears any remaining icons above the max
        for (int i = Convert.ToInt32(maxMana); i < manaHolder.Length; ++i) {
            manaHolder[i].SetActive(false);
        }
        
    }

    //changing the color of the mana bottles if juiced
    public override void ColorChange() {
        base.ColorChange();
        if (isEnhanced) {
            for (int i = 0; i < manaIcons.Length; ++i) {
                manaIcons[i].color = Color.magenta;
            }
        } else {
            for (int i = 0; i < manaIcons.Length; ++i) {
                manaIcons[i].color = Color.white;
            }
        }
    }

    //Whatever me an Nick need to do in order to shoot bullets ig
    public void ShootBullet(Vector2 characterLoc) {
        Vector3 dir = new Vector2(animator.GetFloat("LastHorizontal"), animator.GetFloat("LastVertical"));
        Instantiate(MageProjectile,gameObject.transform.position , Quaternion.identity).GetComponent<MageProjectile>().Setup(dir);
    }

    //Pauses the game
    public void Pause() {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    //Resumes the game
    public void Resume() {
        Time.timeScale = 0;
        pauseScreen.SetActive(false);
    }
}
