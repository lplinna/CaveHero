using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTutorial : MonoBehaviour
{
    public static bool endTutorialTriggered;
    // Start is called before the first frame update
    void Start()
    {
        endTutorialTriggered = false;
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
            yield return new WaitForSeconds(1f);
            LoadingNextLevel.setLevelName("SlimeLevel");
            SceneManager.LoadScene("LoadingNextLevel");
        }
    }
}
