using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    static AudioSource audioSrc;
    static AudioClip slime, ice, lava, throne, tutorial, merchant, boss;
    private static MusicManager instance;
    static bool elevator;
    static bool bossTrigger;
    public bool muteAudio;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        muteAudio = false;
        audioSrc = GetComponent<AudioSource>();
        slime = Resources.Load<AudioClip>("CrystalCaves");
        ice = Resources.Load<AudioClip>("IceMusic");
        lava = Resources.Load<AudioClip>("LavaMusic");
        throne = Resources.Load<AudioClip>("ThroneRoom Music");
        merchant = Resources.Load<AudioClip>("MerchantMusic");
        tutorial = Resources.Load<AudioClip>("TutorialMusic");
        boss = Resources.Load<AudioClip>("BossMusic");


        elevator = false;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TutorialLevel"))
        {
            PlayMusic("Tutorial");
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SlimeLevel"))
        {
            PlayMusic("SlimeMusic");
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("IceLevel"))
        {
            PlayMusic("IceMusic");
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LavaLevel"))
        {
            PlayMusic("LavaMusic");
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ThroneRoom"))
        {
            if (boss)
            {
                PlayMusic("Boss");
            }
            else
            {
                PlayMusic("ThroneRoom");
            }
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Merchant"))
        {
            PlayMusic("Merchant");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            if (!muteAudio)
            {
                AudioListener.pause = true;
                muteAudio = true;
                elevator = true;
            }
            else
            {
                AudioListener.pause = false;
                muteAudio = false;
                elevator = false;
            }
        }

        if ( !elevator && !muteAudio && !isPlayingNow())
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TutorialLevel"))
            {
                PlayMusic("Tutorial");
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SlimeLevel"))
            {
                PlayMusic("SlimeMusic");
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("IceLevel"))
            {
                PlayMusic("IceMusic");
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LavaLevel"))
            {
                PlayMusic("LavaMusic");
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ThroneRoom"))
            {
                PlayMusic("ThroneRoom");
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Merchant"))
            {
                PlayMusic("Merchant");
            }
        }
    }

    public static bool isPlayingNow()
    {
        return audioSrc.isPlaying;
    }

    public static void stopPlaying()
    {
        audioSrc.Stop();
    }

    public static void setElevator(bool nElevator)
    {
        elevator = nElevator;
    }

    public static void setBoss(bool nBoss)
    {
        bossTrigger = nBoss;
    }

    public static void PlayMusic(string clip)
    {
        switch (clip)
        {
            case "SlimeMusic":
                audioSrc.PlayOneShot(slime);
                break;

            case "IceMusic":
                audioSrc.PlayOneShot(ice);
                break;

            case "LavaMusic":
                audioSrc.PlayOneShot(lava);
                break;

            case "ThroneRoom":
                audioSrc.PlayOneShot(throne);
                break;

            case "Merchant":
                audioSrc.PlayOneShot(merchant);
                break;

            case "Tutorial":
                audioSrc.PlayOneShot(tutorial);
                break;

            case "Boss":
                audioSrc.PlayOneShot(boss);
                break;
        }
    }
}
