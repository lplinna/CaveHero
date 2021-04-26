using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer elevatorSprite;
    public Sprite close, smidge, partial, open;
    public bool triggered;
    public CheckpointSystem checkpoint;
    public string currScene;
    public DoNotDestroy doNot;

    void Awake()
    {
        MusicManager.setElevator(false);
        if (!PlayerModifiers.hasNot)
        {
            Debug.Log(this.gameObject.name + " awaiting doNot");
            Invoke("Awake", 0.001f);
            return;
        }
        else
        {
            doNot = PlayerModifiers.doNot;
        }


    }


    void Update()
    {
        currScene = SceneManager.GetActiveScene().name;
        //Debug.Log(currScene);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!triggered)
            {
                if (!doNot.getBeenToThrone())
                {
                    StartCoroutine(SceneChange());
                    triggered = true;
                    checkpoint.triggered = false;
                    StopCoroutine(SceneChange());
                }
            }
        }
    }

    IEnumerator SceneChange()
    {
        MusicManager.stopPlaying();
        MusicManager.setElevator(true);

        SoundManager.PlaySound("ElevatorOpening");
        yield return new WaitForSeconds(1.5f);
        player.transform.position.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        elevatorSprite.sprite = smidge;
        yield return new WaitForSeconds(1);
        elevatorSprite.sprite = partial;
        yield return new WaitForSeconds(1);
        elevatorSprite.sprite = open;
        yield return new WaitForSeconds(2);

        switch (currScene) {
            case "SlimeLevel": // Slime to Ice
                Merchant.setNextScene("IceLevel");
                break;

            case "IceLevel": // Ice to Lava
                Merchant.setNextScene("LavaLevel");
                break;

            case "LavaLevel": // Lava to Throne
                Merchant.setNextScene("ThroneRoom");
                break;

            case "ThroneRoom": // Throne to Slime
                Merchant.setNextScene("SlimeLevel");
                doNot.setBeenToThrone(true);
                
                break;
        }
        LoadingNextLevel.setLevelName("Merchant");
        SceneManager.LoadScene("LoadingNextLevel");
    }
}
