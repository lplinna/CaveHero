using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Merchant : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //REMOVE AFTER MERCHANT HAS BEEN CREATED
        SceneManager.LoadScene("LoadingNextLevel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void setNextScene(string next)
    {
        LoadingNextLevel.setLevelName(next);
    }

}
