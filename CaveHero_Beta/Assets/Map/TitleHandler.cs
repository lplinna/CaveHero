using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleHandler : MonoBehaviour
{
    // Start is called before the first frame update
    

    public void PlayGame()
    {
        LoadingNextLevel.setLevelName("TutorialLevel");
        SceneManager.LoadScene("LoadingNextLevel");
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
