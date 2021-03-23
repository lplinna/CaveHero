using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
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
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("IceLevel"))
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
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LavaLevel"))
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
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ThroneRoom"))
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
        audioSrc = GetComponent<AudioSource>();
        //audioSrc.Play();
    }

    public static bool isPlayingNow()
    {
        return audioSrc.isPlaying;
    }

    public static void stopPlaying()
    {
        audioSrc.Stop();
    }
}
