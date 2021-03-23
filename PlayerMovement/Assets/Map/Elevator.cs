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
    public int currScene;

    void Update()
    {
        currScene = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(currScene);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!triggered)
            {
                StartCoroutine(SceneChange());
                triggered = true;
                checkpoint.triggered = false;
                StopCoroutine(SceneChange());
            }
        }
    }

    IEnumerator SceneChange()
    {
        MusicManager.stopPlaying();
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
            case 2: // Slime to Ice
                Merchant.setNextScene("IceLevel");
                break;

            case 3: // Ice to Lava
                Merchant.setNextScene("LavaLevel");
                break;

            case 4: // Lava to Throne
                Merchant.setNextScene("ThroneRoom");
                break;

            case 5: // Throne to Slime
                Merchant.setNextScene("SlimeLevel");
                break;
        }
        SceneManager.LoadScene("LoadingNextLevel");
    }
}
