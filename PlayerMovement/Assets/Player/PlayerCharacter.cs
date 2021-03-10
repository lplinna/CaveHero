using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    int currMoney = 0;
    public MoneyCounter MONEY;

    // Player Sprites
    public SpriteRenderer playerSprite;
    public Sprite upSprite, sideSprite, downSprite, idle;
    public Animator playerAnim;

    // Player Physics
    public Rigidbody2D playerPhysics;
    public Vector3 move = Vector3.zero;

    // Challenge Room
    public CheckpointSystem checkpoint;
    public ChallengeRoom challenge;

    //Player Audio
    public AudioSource audioSrc;
    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        // sets current energy to max energy at start of scene
        currEnergy = maxEnergy;
        playerPhysics = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointSystem>();
        if (checkpoint.triggered)
        {
            transform.position = checkpoint.checkpointPos;
        }
        audioSrc = GetComponent<AudioSource>();
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimation();
        PlayerSprint();
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    //all player movement statements
    public void PlayerMovement()
    {
        float rawHorizontalAxis = Input.GetAxisRaw("Horizontal");
        float rawVerticalAxis = Input.GetAxisRaw("Vertical");
        move.x = rawHorizontalAxis;
        move.y = rawVerticalAxis;

        if (move != Vector3.zero)
        {
            Vector3 translation = move * speed * Time.fixedDeltaTime;
            Vector3 newPosition = transform.position + translation;
            playerPhysics.MovePosition(newPosition);
            playerwayP = move;
        }

        // moves the player
        //if (move != Vector3.zero) { playerwayP = move; }
        //var hit = Physics2D.Raycast(transform.position, move.normalized, move.magnitude * 80f);

        //if (hit.collider)
        //{
        //    bool isEnemy = hit.collider.gameObject.CompareTag("Bat") || hit.collider.gameObject.CompareTag("Beetle") || hit.collider.gameObject.CompareTag("Spider");
        //    if (!isEnemy)
        //    {
        //        move = transform.position;
        //    }
        //}
    }

    public void PlayerAnimation()
    {


        if (Input.GetKey("d"))
        {
            playerAnim.speed = 1;
            playerAnim.SetBool("WalkSide", true);
            playerAnim.SetBool("Idle", false);
            playerSprite.flipX = false;
            playerAnim.SetBool("WalkDown", false);
            playerAnim.SetBool("WalkUp", false);
            idle = sideSprite;
            playerSprite.sprite = idle;
            isMoving = true;
        }
        if (Input.GetKey("a"))
        {
            playerAnim.speed = 1;
            playerAnim.SetBool("WalkSide", true);
            playerAnim.SetBool("Idle", false);
            playerSprite.flipX = true;
            playerAnim.SetBool("WalkDown", false);
            playerAnim.SetBool("WalkUp", false);
            idle = sideSprite;
            playerSprite.sprite = idle;
            isMoving = true;
        }



        if (Input.GetKey("s"))
        {
            playerAnim.speed = 1;
            playerAnim.SetBool("WalkDown", true);
            playerAnim.SetBool("Idle", false);
            playerAnim.SetBool("WalkSide", false);
            playerAnim.SetBool("WalkUp", false);
            idle = downSprite;
            playerSprite.sprite = idle;
            isMoving = true;
        }


        if (Input.GetKey("w"))
        {
            playerAnim.speed = 1;
            playerAnim.SetBool("WalkUp", true);
            playerAnim.SetBool("Idle", false);
            playerAnim.SetBool("WalkSide", false);
            playerAnim.SetBool("WalkDown", false);
            idle = upSprite;
            playerSprite.sprite = idle;
            isMoving = true;
        }

        if (move == Vector3.zero)
        {
            if (playerwayP.y != 0)
            {
                playerAnim.SetBool("WalkUp", false);
                playerAnim.SetBool("Idle", false);
                playerAnim.SetBool("WalkSide", false);
                playerAnim.SetBool("WalkDown", false);

                if (playerwayP.y > 0)
                    playerAnim.Play("Base Layer.Walk Up", 0, 0f);
                else
                    playerAnim.Play("Base Layer.Walk Down", 0, 0f);

            }

            if (playerwayP.x != 0)
            {
                playerAnim.SetBool("WalkSide", false);
                playerAnim.SetBool("Idle", true);
                playerSprite.sprite = idle;
            }
        }

        if (isMoving)
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
                Drain(0.12f);
            }
            // if energy is not enough for sprinting, speed changes to normal
            if (currEnergy < 20)
            {
                speed = 5;
            }
        }
        else
        {
            // keeps speed at 10, gains energy at a rate of 0.03 per frame
            speed = 5;
            if (currEnergy < 100)
            {
                Gain(0.03f);
            }
        }
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


    public void AddMoney(int money)
    {
        currMoney += money;
        MONEY.SetMoney(currMoney);
    }

    public void TakeMoney(int money)
    {
        currMoney -= money;
        MONEY.SetMoney(currMoney);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StonePickup") || collision.gameObject.CompareTag("AmethystPickup"))
        {
            collision.gameObject.SetActive(false);

        }
        if(collision.gameObject.CompareTag("Checkpoint"))
        {
            checkpoint.checkpointPos = transform.position;
            checkpoint.triggered = true;
        }
        if(collision.gameObject.CompareTag("Challenge"))
        {
            challenge.triggered = true;
        }
    }
}