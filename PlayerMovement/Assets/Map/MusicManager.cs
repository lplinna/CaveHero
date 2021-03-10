using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static AudioClip crystalCaves;
    static AudioSource audioSrc;
    private static MusicManager instance;

    void Awake()
    {
        
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SlimeLevel"))
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
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        crystalCaves = Resources.Load<AudioClip>("CrystalCaves");
        audioSrc = GetComponent<AudioSource>();
        MusicManager.PlaySound("CrystalCaves");
    }

    public static bool isPlayingNow()
    {
        return audioSrc.isPlaying;
    }

    public static void stopPlaying()
    {
        audioSrc.Stop();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "CrystalCaves":
                audioSrc.PlayOneShot(crystalCaves);
                break;
        }
    }
}
