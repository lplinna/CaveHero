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
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            StartCoroutine(SceneChange());
            triggered = true;
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
        LoadingNextLevel.setLevelName("IceLevel");
        SceneManager.LoadScene("LoadingNextLevel");
    }
}
