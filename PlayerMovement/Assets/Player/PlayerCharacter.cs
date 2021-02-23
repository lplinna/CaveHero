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

    public SpriteRenderer playerSprite;
    public Sprite upSprite, sideSprite, downSprite, idleRight;
    
    // Start is called before the first frame update
    void Start()
    {
        // sets current energy to max energy at start of scene
        currEnergy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
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

    //all player movement statements
    public void PlayerMovement()
    {
        // used to transform the player's position
        Vector3 pos = transform.position;

        // forward movement
        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
            playerSprite.sprite = upSprite;
        }

        // backwards movement
        if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
            playerSprite.sprite = downSprite;
        }

        // left movement
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
            playerSprite.sprite = sideSprite;
            playerSprite.flipX = false;
        }

        // right movement
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
            playerSprite.sprite = sideSprite;
            playerSprite.flipX = true;
        }

        // moves the player
        var playerway = (pos - transform.position);
        if (playerway != Vector3.zero) { playerwayP = playerway; }
        var hit = Physics2D.Raycast(transform.position, playerway.normalized, playerway.magnitude * 80f);

        if (hit.collider)
        {
            pos = transform.position;
        }
        transform.position = pos;


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
}