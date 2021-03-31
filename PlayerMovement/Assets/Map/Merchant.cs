using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Merchant : MonoBehaviour
{
    public static string nextLevelName;
    public static bool enterShopTrigger;
    public static GameObject shop;
    void Awake()
    {
        this.gameObject.SetActive(false);
        enterShopTrigger = false;
        shop = this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public static void setNextScene(string next)
    {
        nextLevelName = next;
    }

    public static void loadNextScene()
    {
        LoadingNextLevel.setLevelName(nextLevelName);
        SceneManager.LoadScene("LoadingNextLevel");
    }

    public static void enterShop()
    {
        shop.SetActive(true);
        enterShopTrigger = true;
        Debug.Log(enterShopTrigger);
    }

    public static void exitShop()
    {
        shop.SetActive(false);
        enterShopTrigger = false;
        Debug.Log(enterShopTrigger);
    }
}
