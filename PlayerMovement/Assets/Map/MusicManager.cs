using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    static AudioSource audioSrc;
    static AudioClip slime, ice, lava, throne, tutorial, merchant;
    private static MusicManager instance;
    static bool elevator;

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
        audioSrc = GetComponent<AudioSource>();
        slime = Resources.Load<AudioClip>("CrystalCaves");
        ice = Resources.Load<AudioClip>("IceMusic");
        lava = Resources.Load<AudioClip>("LavaMusic");

        elevator = false;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SlimeLevel"))
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
        else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Merchant"))
        {
            stopPlaying();
        }
    }

    void Update()
    {
        if(!isPlayingNow())
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SlimeLevel") && !elevator)
            {
                PlayMusic("SlimeMusic");
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("IceLevel") && !elevator)
            {
                PlayMusic("IceMusic");
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LavaLevel") && !elevator)
            {
                PlayMusic("LavaMusic");
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Merchant"))
            {
                stopPlaying();
                elevator = false;
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
        }
    }
}
