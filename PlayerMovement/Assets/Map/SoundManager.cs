using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip spiderDeath, playerDeath, healthPotion;
    public static AudioClip winChallenge, elevator;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        spiderDeath = Resources.Load<AudioClip>("SpiderDeath");
        playerDeath = Resources.Load<AudioClip>("PlayerDeath");
        healthPotion = Resources.Load<AudioClip>("HealthPotion");
        winChallenge = Resources.Load<AudioClip>("WinChallenge");
        elevator = Resources.Load<AudioClip>("ElevatorOpening");

        audioSrc = GetComponent<AudioSource>();
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
                audioSrc.PlayOneShot(spiderDeath);
                break;

            case "PlayerDeath":
                audioSrc.PlayOneShot(playerDeath);
                break;

            case "HealthPotion":
                audioSrc.PlayOneShot(healthPotion);
                break;
            case "WinChallenge":
                audioSrc.PlayOneShot(winChallenge);
                break;

            case "ElevatorOpening":
                audioSrc.PlayOneShot(elevator);
                break;
        }
    }
}
