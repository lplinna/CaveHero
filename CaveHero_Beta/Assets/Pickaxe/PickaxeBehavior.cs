﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeBehavior : MonoBehaviour
{
    public AudioSource audioSrc;

    public bool canHit;
    public bool PowerAttack;
    public GameObject FIRE;
    public GameObject POISON;
    public GameObject ICE;
    public int elemental = 0;


    private void Start()
    {
        canHit = true;
        audioSrc = GetComponent<AudioSource>();
        //PlayerModifiers.damageModifier = 300f;
        //PlayerModifiers.healthModifier = 300f;
        //PlayerModifiers.energyModifier = 300f;
    }


    public void EnergyPenalty(float b)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().currEnergy -= b;
    }

    public void HealthPenalty(float b)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().currHealth -= b;
    }



    



    public void OnCollisionStay2D(Collision2D collision)
    {

        Debug.Log(collision.collider.gameObject.name);
        bool isEnemy = collision.gameObject.CompareTag("Bat") || collision.gameObject.CompareTag("Beetle") || collision.gameObject.CompareTag("Spider") || collision.gameObject.CompareTag("ChallengeEnemies") || collision.gameObject.CompareTag("King");
        if (isEnemy)
        {
            Debug.Log("Enemy hit!");
            var dm = collision.gameObject.GetComponent<EnemyHealth>();
           
            
            if (canHit && elemental==0)
            {
                dm.Damage(30f * PlayerModifiers.damageModifier);
                canHit = false;
            }

            if (PowerAttack && elemental==0)
            {
                dm.Damage(5f * PlayerModifiers.damageModifier);
            }


            if (elemental!=0)
            {
                if (elemental == 1)
                {
                    PlayerAttackMining.poisonAttack = false;
                    var p = Instantiate(POISON);
                    p.GetComponent<SlimeActorBehavior>().actee = collision.gameObject;
                    elemental = 0;
                }
                if (elemental == 2)
                {
                    PlayerAttackMining.fireAttack = false;
                    var p = Instantiate(FIRE);
                    p.GetComponent<FireActorBehavior>().actee = collision.gameObject;
                    elemental = 0;
                    HealthPenalty(5f);
                    EnergyPenalty(30f);
                }
                if (elemental == 3)
                {
                    PlayerAttackMining.iceAttack = false;
                    var p = Instantiate(ICE);
                    p.GetComponent<IceActorBehavior>().actee = collision.gameObject;
                    elemental = 0;
                }
            }

        }

        if (collision.gameObject.CompareTag("Stone") && canHit)
        {
            Debug.Log("Stone hit!");
            var stone = collision.gameObject.GetComponent<StoneHealth>();
            stone.Damage(20f);
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
            else
            {
                audioSrc.Stop();
                audioSrc.Play();
            }
            canHit = false;
        }

        if (collision.gameObject.CompareTag("EndTutorial") && canHit)
        {
            collision.gameObject.GetComponent<EndTutorial>().doEnd();
        }

        if (collision.gameObject.CompareTag("Chest") && canHit)
        {
            Debug.Log("chest hit!");
            var chest = collision.gameObject.GetComponent<ChestBehavior>();
            chest.Damage(1f);
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
            else
            {
                audioSrc.Stop();
                audioSrc.Play();
            }
            canHit = false;
        }
    }
}
