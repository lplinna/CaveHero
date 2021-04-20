using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Doublsb.Dialog;

public class PlayerCharacter : MonoBehaviour
{
    // walking speed
    public float speed = 5.0f;
    // current energy at the moment initialized at 0
    public float currEnergy = 0.0f;
    // max amount of energy
    public float maxEnergy = 100.0f;
    public Vector3 playerwayP;
    // To create the visual energy bar
    public EnergyBar energyBar;
    public MoneyCounter MONEY;
    public InventoryCounter inventoryCounter;
    public GameObject inventoryMenu;
    public GameObject escMenu;
    public GameObject optionsMenu;
    public GameObject confirmationMenu;


    // Player Sprites
    public SpriteRenderer playerSprite;
    public Sprite upSprite, sideSprite, downSprite, idle;
    public Animator playerAnim;

    // Player Physics
    public Rigidbody2D playerPhysics;
    public Vector3 move = Vector3.zero;
    public bool onSlippery;
    public float slippery;

    // Challenge Room
    public CheckpointSystem checkpoint;
    public ChallengeRoom challenge;

    //Player Audio
    public AudioSource audioSrc;
    public bool isMoving;
    public bool isAttackingAnim;
    public bool muteAudio;
    public bool isDialog;


    public DoNotDestroy doNot;
    public GameObject tempDoNot;
    public GameObject tempInv;
    // Start is called before the first frame update
    void Start()
    {
        // sets current energy to max energy at start of scene
        currEnergy = maxEnergy;
        playerPhysics = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "ThroneRoom" || SceneManager.GetActiveScene().name == "Merchant" || SceneManager.GetActiveScene().name == "TutorialLevel")
        {

        }
        else
        {
            checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointSystem>();
            if (checkpoint.triggered)
            {
                transform.position = checkpoint.checkpointPos;
            }
        }

        
        MONEY = GameObject.FindGameObjectWithTag("Money").GetComponent<MoneyCounter>();

        audioSrc = GetComponent<AudioSource>();
        isMoving = false;
        muteAudio = false;
        isAttackingAnim = false;
        slippery = 1f;
        onSlippery = false;
        isPaused = false;
        escMenu.SetActive(false);



        try
        {
            doNot = GameObject.FindGameObjectWithTag("DoNotDestroy").GetComponent<DoNotDestroy>();
        }
        catch
        {
            doNot = GameObject.Instantiate(tempDoNot).GetComponent<DoNotDestroy>();
        }

        try
        {
            inventoryMenu = GameObject.FindGameObjectWithTag("Inventory").transform.GetChild(0).gameObject;
            inventoryCounter = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryCounter>();
            inventoryMenu.SetActive(false);
        }
        catch
        {
            GameObject.Instantiate(tempInv);

            Start();
        }


        if (doNot.hasReset)
        {
            MONEY.ForceMoney(doNot.lastingMoney);
            inventoryCounter.forceStone(doNot.stoneCount);
            inventoryCounter.forceAmethyst(doNot.amethystCount);
            inventoryCounter.forceEmerald(doNot.emeraldCount);
            inventoryCounter.forceRuby(doNot.rubyCount);
            inventoryCounter.forceDiamond(doNot.diamondCount);
            doNot.hasReset = false;
        }
        else
        {
            doNot.lastingMoney = MONEY.getMoney();
            doNot.stoneCount = inventoryCounter.getStone();
            doNot.amethystCount = inventoryCounter.getAmethyst();
            doNot.emeraldCount = inventoryCounter.getEmerald();
            doNot.diamondCount = inventoryCounter.getDiamond();
            doNot.rubyCount = inventoryCounter.getRuby();
        }



    }

    // Update is called once per frame
    void Update()
    {
        // Used to set checkpoint if one was not found in Awake
        if (SceneManager.GetActiveScene().name == "ThroneRoom" || SceneManager.GetActiveScene().name == "Merchant" || SceneManager.GetActiveScene().name == "TutorialLevel")
        {
            
        }
        else
        {
            if (checkpoint.name == string.Empty)
            {
                checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointSystem>();
            }
        }

        if (Input.GetKeyDown("i"))
        {
            if (!isPaused && inventoryMenu!=null)
            {
                Time.timeScale = 0;
                inventoryMenu.SetActive(true);
            }
            else if (isPaused && inventoryMenu!=null)
            {
                Time.timeScale = 1;
                inventoryMenu.SetActive(false);
            }
            else { Time.timeScale = 1; }
            if (inventoryMenu != null) { isPaused = !isPaused; }
        }

        if (isPaused) return;

        if (!isAttackingAnim && !isDialog)
        {
            PlayerAnimation();
        }


        
        if (Input.GetKeyDown("m"))
        {
            if (!muteAudio)
            {
                AudioListener.pause = true;
                muteAudio = true;
            }
            else
            {
                AudioListener.pause = false;
                muteAudio = false;
            }
        }

        if (onSlippery)
        {
            slippery = 3f;
        }
        else
        {
            slippery = 1f;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (escMenu.activeInHierarchy)
            {
                escMenu.SetActive(false);
            }
            else
            {
                PauseEverything();
            }
        }

        if (isPaused) return;

        if (isDialog)
        {
            playerAnim.SetBool("Idle", true);
            playerAnim.SetBool("WalkUp", false);
            playerAnim.SetBool("WalkLeft", false);
            playerAnim.SetBool("WalkRight", false);
            playerAnim.SetBool("WalkDown", false);


        }


        

        if (!isDialog && !onSlippery)
        {
            PlayerMovement();
            PlayerSprint();
        }

        if (onSlippery)
        {
            SlippyMovement();
        }

        if (isDialog)
        {
            var dialogue = GetComponent<Message0>();
            if (Input.GetMouseButton(0) && dialogue.DialogManager.state == Doublsb.Dialog.State.Wait)
            {
                dialogue.Advance();
            }
        }

    }




    public void SlippyMovement()
    {
        float rawHorizontalAxis = Input.GetAxisRaw("Horizontal");
        float rawVerticalAxis = Input.GetAxisRaw("Vertical");

        if (rawHorizontalAxis != 0) move.x = Mathf.Lerp(move.x, rawHorizontalAxis, 0.6f);
        if (rawVerticalAxis != 0) move.y = Mathf.Lerp(move.x, rawVerticalAxis, 0.6f);

        Vector3 translation = move.normalized * 12f * Time.fixedDeltaTime;
        Vector3 newPosition = transform.position + translation;
        playerPhysics.MovePosition(newPosition);

    }



    //all player movement statements
    public void PlayerMovement()
    {

        float rawHorizontalAxis = Input.GetAxisRaw("Horizontal");
        float rawVerticalAxis = Input.GetAxisRaw("Vertical");
        move.x = rawHorizontalAxis;
        move.y = rawVerticalAxis;

        Vector3 translation;

        if (move != Vector3.zero)
        {
            if (onSlippery)
            {
                translation = move * speed * Time.fixedDeltaTime * slippery;
            }
            else
            {
                translation = move * speed * Time.fixedDeltaTime;

            }
            Vector3 newPosition = transform.position + translation;
            playerPhysics.MovePosition(newPosition);
            playerwayP = move;
        }
    }


    public void DoAttackAnimation()
    {
        if (!isDialog)
        {
            playerAnim.SetBool("WalkUp", false);
            playerAnim.SetBool("Idle", false);
            playerAnim.SetBool("WalkLeft", false);
            playerAnim.SetBool("WalkRight", false);
            playerAnim.SetBool("WalkDown", false);
            playerAnim.speed = 1;

            if (playerwayP.x < 0)
            {
                playerSprite.flipX = true;
                playerAnim.Play("Base Layer.AttackSide", 0, 0f);
            }
            if (playerwayP.x > 0)
            {
                playerSprite.flipX = false;
                playerAnim.Play("Base Layer.AttackSide", 0, 0f);
            }

            if (playerwayP.y < 0)
            {
                playerSprite.flipX = true;
                playerAnim.Play("Base Layer.AttackDown", 0, 0f);
            }
            if (playerwayP.y > 0)
            {
                playerSprite.flipX = false;
                playerAnim.Play("Base Layer.AttackUp", 0, 0f);
            }

            if (GetComponent<PlayerAttackMining>().swiping == 2)
            {
                StartCoroutine(DrainSwipe(1f / 30f, 1.5f));
            }

        }

    }



    public void LeaveAttack()
    {
        playerAnim.speed = 100f;
        playerAnim.SetBool("WalkRight", false);
        playerAnim.SetBool("WalkLeft", false);
        playerAnim.SetBool("Idle", true);
        playerAnim.SetBool("WalkDown", false);
        playerAnim.SetBool("WalkUp", false);
    }


    public void PlayerAnimation()
    {

        playerSprite.flipX = false;



        if (Input.GetKey("d"))
        {
            playerAnim.speed = 1;
            playerAnim.SetBool("WalkRight", true);
            playerAnim.SetBool("WalkLeft", false);
            playerAnim.SetBool("Idle", false);
            playerAnim.SetBool("WalkDown", false);
            playerAnim.SetBool("WalkUp", false);
            idle = sideSprite;
            isMoving = true;
        }
        if (Input.GetKey("a"))
        {
            playerAnim.speed = 1;
            playerAnim.SetBool("WalkRight", false);
            playerAnim.SetBool("WalkLeft", true);
            playerAnim.SetBool("Idle", false);
            playerAnim.SetBool("WalkDown", false);
            playerAnim.SetBool("WalkUp", false);
            idle = sideSprite;
            isMoving = true;
        }



        if (Input.GetKey("s"))
        {
            playerAnim.speed = 1;
            playerAnim.SetBool("WalkDown", true);
            playerAnim.SetBool("Idle", false);
            playerAnim.SetBool("WalkLeft", false);
            playerAnim.SetBool("WalkRight", false);
            playerAnim.SetBool("WalkUp", false);
            idle = downSprite;
            isMoving = true;
        }


        if (Input.GetKey("w"))
        {
            playerAnim.speed = 1;
            playerAnim.SetBool("WalkUp", true);
            playerAnim.SetBool("Idle", false);
            playerAnim.SetBool("WalkLeft", false);
            playerAnim.SetBool("WalkRight", false);
            playerAnim.SetBool("WalkDown", false);
            idle = upSprite;
            isMoving = true;
        }

        if (move == Vector3.zero)
        {
            if (playerwayP.y != 0)
            {

                playerAnim.SetBool("WalkUp", false);
                playerAnim.SetBool("Idle", false);
                playerAnim.SetBool("WalkLeft", false);
                playerAnim.SetBool("WalkRight", false);
                playerAnim.SetBool("WalkDown", false);

                if (playerwayP.y > 0)
                    playerAnim.Play("Base Layer.Walk Up", 0, 0f);
                else
                    playerAnim.Play("Base Layer.Walk Down", 0, 0f);

            }

            if (playerwayP.x != 0)
            {

                playerAnim.SetBool("WalkLeft", false);
                playerAnim.SetBool("WalkRight", false);
                playerAnim.SetBool("Idle", false);
                if (playerwayP.x < 0)
                    playerAnim.Play("Base Layer.Special Idle", 0, 0f);
                else
                    playerAnim.Play("Base Layer.Special Idle", 0, 0.5f);
            }
        }

        if (isMoving && !isDialog && !isPaused)
        {
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();

            }
        }
        else
        {
            audioSrc.Stop();
        }

        isMoving = false;
    }


    private bool isSprint = false;
    public void PlayerSprint()
    {
        // prints current energy and speed of player to console
        //Debug.Log(currEnergy + " " + speed);
        // Checks to see if user is holding a movement key and shift to enact sprinting
        if (Input.GetKey("left shift") && (Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("s") || Input.GetKey("w")))
        {
            if (!(currEnergy <= 0))
            {
                // speed goes up to 14, energy gets drained at a rate of 0.12 per frame
                speed = 9;
                playerAnim.speed = 2;

                if (isSprint == false)
                {
                    isSprint = true;
                    StartCoroutine(doSprint(1f / 30f, 3f));
                }
                 
                //Drain(0.12f * q);
            }
            // if energy is not enough for sprinting, speed changes to normal
            if (currEnergy < 20)
            {
                playerAnim.speed = 1;
                speed = 5;
            }
        }
        else
        {
            // keeps speed at 10, gains energy at a rate of 0.03 per frame


            speed = 5;
            if (isSprint == true)
            {
                isSprint = false;
                doRecover();
            }

            
            if (isAttackingAnim && GetComponent<PlayerAttackMining>().swiping == 2)
            {
                speed = 2.5f;
            }

            
            
        }
    }

    public IEnumerator doSprint(float speed,float drain)
    {
        
        while (true)
        {
            if(isSprint==false)
            {
                yield break;
            }
            if(currEnergy < 20)
            {
                yield break;
            }
            Drain(drain);
            yield return new WaitForSeconds(speed);
        }
    }

    public IEnumerator doGain(float speed, float gain)
    {
        while (true)
        {
            if(isSprint==true || currEnergy > 100)
            {
                yield break;
            }
            Gain(gain);
            yield return new WaitForSeconds(speed);
        }
    }


    public IEnumerator DrainSwipe(float speed, float drain)
    {
        while (isAttackingAnim)
        {
            Drain(drain);
            yield return new WaitForSeconds(speed);
        }
        doRecover();
    }
    
    public void doRecover()
    {
        StartCoroutine(doGain(1f / 30f, 0.4f));
    }

    // drains energy
    public void Drain(float drainEnergy)
    {
        currEnergy -= drainEnergy;

        energyBar.SetEnergy(currEnergy);
    }

    // gains energy
    public void Gain(float gainEnergy)
    {
        currEnergy += gainEnergy;

        energyBar.SetEnergy(currEnergy);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StonePickup"))
        {
            if (collision.gameObject.name.Contains("Gold"))
            {
                MONEY.AddMoney(1);
            }
            else if (collision.gameObject.name.Contains("Sapphire"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Message0>().Sapphire();
            }
            else
            {
                SoundManager.PlaySound("Pickup");
            }
            // adds the stones to the inventory
            addToInventory(collision);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            checkpoint.checkpointPos = transform.position;
            checkpoint.triggered = true;
        }
        if (collision.gameObject.CompareTag("Challenge"))
        {
            challenge.triggered = true;
        }
        if (collision.gameObject.CompareTag("SlipperyIce"))
        {
            onSlippery = true;
        }
        if (collision.gameObject.name.Contains("MerchantTrigger"))
        {
            GameObject.FindGameObjectWithTag("Merchant").GetComponent<Merchant>().enterShop();
        }
        if (collision.gameObject.name.Contains("GoBack"))
        {
            GameObject.FindGameObjectWithTag("Merchant").GetComponent<Merchant>().goBackConfirmationStart();
        }
        if (collision.gameObject.name.Contains("GoNext"))
        {
            GameObject.FindGameObjectWithTag("Merchant").GetComponent<Merchant>().goNextConfirmationStart();
        }
        if (collision.gameObject.name.Contains("LeftClick"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Message0>().LeftClickTutorial();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.name.Contains("ShiftSprint"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Message0>().ShiftSprintTutorial();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.name.Contains("Combat"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Message0>().CombatTutorial();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.name.Contains("Chest"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Message0>().ChestTutorial();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.name.Contains("EndGame") && doNot.getKingDead())
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Message0>().EndGame();
            collision.gameObject.SetActive(false);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlipperyIce"))
        {
            onSlippery = false;
        }
    }

    public void addToInventory(Collider2D collision)
    {

        if (collision.gameObject.name.Contains("Stone"))
        {
            inventoryCounter.addStone(1);
        }
        if (collision.gameObject.name.Contains("Amethyst"))
        {
            inventoryCounter.addAmethyst(1);
        }
        if (collision.gameObject.name.Contains("Emerald"))
        {
            inventoryCounter.addEmerald(1);
        }
        if (collision.gameObject.name.Contains("Ruby"))
        {
            inventoryCounter.addRuby(1);
        }
        if (collision.gameObject.name.Contains("Diamond"))
        {
            inventoryCounter.addDiamond(1);
        }
    }




    private bool isPaused;
    public void PauseEverything()
    {
        escMenu.SetActive(true);
        optionsMenu.SetActive(true);
        confirmationMenu.SetActive(false);
        Time.timeScale = 0;
        isPaused = true;
        inventoryMenu.SetActive(false);
        GetComponentInChildren<DialogManager>().Hide();
    }




    public void CloseGameConfirmation()
    {
        confirmationMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        ResumeEverything();
        var p = SceneManager.GetActiveScene();
        SceneManager.LoadScene(p.name);
        doNot.hasReset = true;
        inventoryMenu.SetActive(false);

    }
    public void ResumeEverything()
    {
        Time.timeScale = 1f;
        isPaused = false;
        escMenu.SetActive(false);
    }


}