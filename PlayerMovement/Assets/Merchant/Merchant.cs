using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Merchant : MonoBehaviour
{
    public string nextLevelName;
    public bool enterShopTrigger;
    public GameObject shop, goBackConfirmation, goNextConfirmation, doNotHaveKey;
    public DoNotDestroy keyTrigger;
    public GameObject player;
    public GameObject UI;

    void Awake()
    {
        enterShopTrigger = false;
        goBackConfirmation.SetActive(false);
        shop.SetActive(false);
        goNextConfirmation.SetActive(false);
        doNotHaveKey.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        keyTrigger = player.GetComponent<DoNotDestroy>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        setNextScene("IceLevel");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void setNextScene(string next)
    {
        nextLevelName = next;
    }

    public void loadNextScene()
    {
        keyTrigger.setChallengeTrigger(false);
        LoadingNextLevel.setLevelName(nextLevelName);
        SceneManager.LoadScene("LoadingNextLevel");
    }

    public void enterShop()
    {
        shop.SetActive(true);
        enterShopTrigger = true;
        player.GetComponent<PlayerCharacter>().isDialog = true;
        player.GetComponent<AudioSource>().Stop();
        UI.SetActive(false);

        Debug.Log(enterShopTrigger);
    }

    public void exitShop()
    {
        shop.SetActive(false);
        enterShopTrigger = false;
        player.GetComponent<PlayerCharacter>().isDialog = false;
        UI.SetActive(true);
        Debug.Log(enterShopTrigger);
    }

    public void upgradeDamage()
    {
        if (PlayerModifiers.damageModifier < 2f)
        {
            PlayerModifiers.damageModifier += 0.5f;
            Debug.Log(PlayerModifiers.damageModifier);
        }
    }

    public void upgradeHealth()
    {
        if (PlayerModifiers.healthModifier < 2f)
        {
            PlayerModifiers.healthModifier += 0.5f;
            Debug.Log(PlayerModifiers.healthModifier);
        }
    }

    public void upgradeEnergy()
    {
        if (PlayerModifiers.energyModifier < 2f)
        {
            PlayerModifiers.energyModifier += 0.5f;
            Debug.Log(PlayerModifiers.energyModifier);
        }
    }

    public void goBack()
    {
        Debug.Log("Button Clicked");
        switch (nextLevelName)
        {
            case "IceLevel":
                setNextScene("SlimeLevel");
                loadNextScene();
                break;

            case "LavaLevel":
                setNextScene("IceLevel");
                loadNextScene();
                break;

            case "ThroneRoom":
                setNextScene("LavaLevel");
                loadNextScene();
                break;
        }
    }

    public void goBackConfirmationStart()
    {
        goBackConfirmation.SetActive(true);
        player.GetComponent<PlayerCharacter>().isDialog = true;
        player.GetComponent<AudioSource>().Stop();
    }
    public void goBackConfirmationEnd()
    {
        goBackConfirmation.SetActive(false);
        player.GetComponent<PlayerCharacter>().isDialog = false;
    }

    public void goNextConfirmationStart()
    {
        goNextConfirmation.SetActive(true);
        player.GetComponent<PlayerCharacter>().isDialog = true;
        player.GetComponent<AudioSource>().Stop();
    }

    public void goNextConfirmationEnd()
    {
        goNextConfirmation.SetActive(false);
        player.GetComponent<PlayerCharacter>().isDialog = false;
    }

    public void goNext()
    {
        Debug.Log("Button Clicked");
        switch (nextLevelName)
        {
            case "IceLevel":
                if (keyTrigger.getIceKey())
                {
                    loadNextScene();
                }
                else
                {
                    goNextConfirmation.SetActive(false);
                    doNotHaveKey.SetActive(true);
                }
                break;

            case "LavaLevel":
                if (keyTrigger.getLavaKey())
                {
                    loadNextScene();
                }
                else
                {
                    goNextConfirmation.SetActive(false);
                    doNotHaveKey.SetActive(true);
                }
                break;

            case "ThroneRoom":
                if (keyTrigger.getThroneKey())
                {
                    loadNextScene();
                }
                else
                {
                    goNextConfirmation.SetActive(false);
                    doNotHaveKey.SetActive(true);
                }
                break;
        }
    }
}
