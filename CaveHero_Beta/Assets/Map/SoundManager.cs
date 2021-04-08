using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip spiderDeath, playerDeath, healthPotion;
    public static AudioClip winChallenge, elevator, batDeath, beetleDeath;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        spiderDeath = Resources.Load<AudioClip>("SpiderDeath");
        playerDeath = Resources.Load<AudioClip>("PlayerDeath");
        batDeath = Resources.Load<AudioClip>("BatDeath");
        beetleDeath = Resources.Load<AudioClip>("BeetleDeath");

        healthPotion = Resources.Load<AudioClip>("HealthPotion");
        winChallenge = Resources.Load<AudioClip>("WinChallenge");
        elevator = Resources.Load<AudioClip>("ElevatorOpening");

        audioSrc = GetComponent<AudioSource>();
        audioSrc.volume = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "SpiderDeath":
                audioSrc.volume = 1f;
                audioSrc.PlayOneShot(spiderDeath);
                break;

            case "PlayerDeath":
                audioSrc.volume = 1f;
                audioSrc.PlayOneShot(playerDeath);
                break;

            case "HealthPotion":
                audioSrc.volume = 1f;
                audioSrc.PlayOneShot(healthPotion);
                break;

            case "WinChallenge":
                audioSrc.volume = 0.5f;
                audioSrc.PlayOneShot(winChallenge);
                break;

            case "ElevatorOpening":
                audioSrc.volume = 1f;
                audioSrc.PlayOneShot(elevator);
                break;

            case "BatDeath":
                audioSrc.volume = 1f;
                audioSrc.PlayOneShot(batDeath);
                break;

            case "BeetleDeath":
                audioSrc.volume = 1f;
                audioSrc.PlayOneShot(beetleDeath);
                break;
        }
    }
}
