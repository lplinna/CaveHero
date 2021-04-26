using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject endGame;
    public DoNotDestroy doNot;
    public ThroneRoomHealth healthPotionSpawner;
    public bool finished;
    public Message0 message;
    // Start is called before the first frame update
    void Start()
    {


        if (!PlayerModifiers.hasNot)
        {
            Debug.Log(this.gameObject.name + " awaiting doNot");
            Invoke("Start", 0.001f);
            return;
        }
        else
        {
            doNot = PlayerModifiers.doNot;
        }

        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!doNot)
        {
            doNot = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().doNot;
        }
        else
        {
            if (doNot.getKingDead())
            {
                endGame.SetActive(true);
                healthPotionSpawner.stopSpawn();
            }
        }

        if(message.endGame)
        { 
            if (finished)
            {
                MusicManager.stopPlaying();
                MusicManager.muteAudio = true;
                SoundManager.stopAudio();

                LoadingNextLevel.setLevelName("TitleScreen");
                SceneManager.LoadScene("LoadingNextLevel");
            }
        }

        
    }
}
