using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer elevatorSprite;
    public Sprite close, smidge, partial, open;
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(SceneChange());
    }

    IEnumerator SceneChange()
    {
        SoundManager.PlaySound("ElevatorOpening");
        player.transform.position.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        elevatorSprite.sprite = smidge;
        yield return new WaitForSeconds(1);
        elevatorSprite.sprite = partial;
        yield return new WaitForSeconds(1);
        elevatorSprite.sprite = open;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("LoadingNextLevel");

    }
}
