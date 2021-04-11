using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleHandler : MonoBehaviour
{
    // Start is called before the first frame update
    

    public void PlayGame()
    {
        SceneManager.LoadScene("TutorialLevel");

    }


    public void ExitGame()
    {
        Application.Quit();

    }
}
