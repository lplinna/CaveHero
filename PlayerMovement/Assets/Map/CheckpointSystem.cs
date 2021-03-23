using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointSystem : MonoBehaviour
{
    public bool triggered;
    private static CheckpointSystem instance;
    public Vector2 checkpointPos;

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

    void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SlimeLevel"))
        {
            checkpointPos.Set(172.5f, -11.5f);
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("IceLevel"))
        {
            checkpointPos.Set(142.5f, 1.65f);
        } 
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LavaLevel"))
        {
            checkpointPos.Set(159.5f, 26.7f);
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ThroneRoom"))
        {
            checkpointPos.Set(0, 0);
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Merchant"))
        {
            checkpointPos.Set(0, 0);
        }
    }
}
