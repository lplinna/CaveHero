using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip spiderDeath, playerDeath, healthPotion;
    public static AudioClip winChallenge, elevator, batDeath, beetleDeath;
    public static AudioClip coinClink, caveCollapse, pickup, chestOpening;
    static AudioSource audioSrc;
    public static bool muteAudio;
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

        coinClink = Resources.Load<AudioClip>("coinClink");
        caveCollapse = Resources.Load<AudioClip>("CaveCollapse");
        pickup = Resources.Load<AudioClip>("itemPickup");
        chestOpening = Resources.Load<AudioClip>("ChestOpening");


        audioSrc = GetComponent<AudioSource>();
        muteAudio = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            if (!muteAudio)
            {
                stopAudio();
            }
            else
            {
                resumeAudio();
            }
        }
    }

    public static void stopAudio()
    {
        AudioListener.pause = true;
        AudioListener.volume = 0;
        muteAudio = true;
    }

    public static void resumeAudio()
    {
        AudioListener.pause = false;
        AudioListener.volume = 1;
        muteAudio = false;
    }

    public static void PlaySound(string clip)
    {
        if (!muteAudio)
        {
            switch (clip)
            {
                case "SpiderDeath":
                    audioSrc.volume = 0.4f;
                    audioSrc.PlayOneShot(spiderDeath);
                    break;

                case "PlayerDeath":
                    audioSrc.volume = 0.6f;
                    audioSrc.PlayOneShot(playerDeath);
                    break;

                case "HealthPotion":
                    audioSrc.volume = 0.2f;
                    audioSrc.PlayOneShot(healthPotion);
                    break;

                case "WinChallenge":
                    audioSrc.volume = 0.4f;
                    audioSrc.PlayOneShot(winChallenge);
                    break;

                case "ElevatorOpening":
                    audioSrc.volume = 1f;
                    audioSrc.PlayOneShot(elevator);
                    break;

                case "BatDeath":
                    audioSrc.volume = 0.5f;
                    audioSrc.PlayOneShot(batDeath);
                    break;

                case "BeetleDeath":
                    audioSrc.volume = 0.5f;
                    audioSrc.PlayOneShot(beetleDeath);
                    break;

                case "Cave":
                    audioSrc.volume = 0.5f;
                    audioSrc.PlayOneShot(caveCollapse);
                    break;

                case "Coin":
                    audioSrc.volume = 1f;
                    audioSrc.PlayOneShot(coinClink);
                    break;

                case "Pickup":
                    audioSrc.volume = 0.5f;
                    audioSrc.PlayOneShot(pickup);
                    break;

                case "Chest":
                    audioSrc.volume = 0.1f;
                    audioSrc.PlayOneShot(chestOpening);
                    break;
            }
        }

    }
}
