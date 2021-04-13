using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTutorial : MonoBehaviour
{
    ShakeBehavior shake;
    public static bool endTutorialTriggered;
    // Start is called before the first frame update
    void Start()
    {
        endTutorialTriggered = false;
        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(endTutorial());
    }

    IEnumerator endTutorial()
    {
        if (endTutorialTriggered)
        {
            MusicManager.stopPlaying();
            SoundManager.PlaySound("Cave");
            shake.TriggerShake();
            yield return new WaitForSeconds(1f);
            LoadingNextLevel.setLevelName("ThroneRoom");
            SceneManager.LoadScene("LoadingNextLevel");
        }
    }
}
