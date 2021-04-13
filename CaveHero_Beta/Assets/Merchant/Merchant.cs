using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Merchant : MonoBehaviour
{
    public static string nextLevelName;
    public bool enterShopTrigger;
    public GameObject shop;
    public Canvas goNextCanvas, goBackCanvas, doNotHaveKey;
    public DoNotDestroy doNot;
    public GameObject player;
    public GameObject UI;
    public TextMeshProUGUI healthCost, damageCost, energyCost;
    public float healthCostFloat, damageCostFloat, energyCostFloat;
    public MerchantMessage message;
    public MoneyCounter moneyCounter;

    void Awake()
    {
        enterShopTrigger = false;
        goBackCanvas.gameObject.SetActive(false);
        shop.SetActive(false);
        goNextCanvas.gameObject.SetActive(false);
        doNotHaveKey.gameObject.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        doNot = GameObject.FindGameObjectWithTag("DoNotDestroy").GetComponent<DoNotDestroy>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthCostFloat = 200 * PlayerModifiers.healthModifier;
        energyCostFloat = 250 * PlayerModifiers.energyModifier;
        damageCostFloat = 300 * PlayerModifiers.damageModifier;
        healthCost.text = "$" + healthCostFloat;
        energyCost.text = "$" + energyCostFloat;
        damageCost.text = "$" + damageCostFloat;
    }

    public static void setNextScene(string next)
    {
        nextLevelName = next;
    }

    public void loadNextScene()
    {
        doNot.setChallengeTrigger(false);
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
        if(moneyCounter.getMoney() >= damageCostFloat)
        {
            if (PlayerModifiers.damageModifier < 2f)
            {
                PlayerModifiers.damageModifier += 0.5f;
                moneyCounter.TakeMoney(damageCostFloat);
                Debug.Log(PlayerModifiers.damageModifier);
            }
        }
        else
        {
            message.NotEnough();
        }
        
    }

    public void upgradeHealth()
    {
        if(moneyCounter.getMoney() >= healthCostFloat)
        {
            if (PlayerModifiers.healthModifier < 2f)
            {
                PlayerModifiers.healthModifier += 0.5f;
                moneyCounter.TakeMoney(healthCostFloat);
                Debug.Log(PlayerModifiers.healthModifier);
            }
        }
        else
        {
            message.NotEnough();
        }
    }

    public void upgradeEnergy()
    {
        if(moneyCounter.getMoney() >= energyCostFloat)
        {
            if (PlayerModifiers.energyModifier < 2f)
            {
                PlayerModifiers.energyModifier += 0.5f;
                moneyCounter.TakeMoney(energyCostFloat);
                Debug.Log(PlayerModifiers.energyModifier);
            }
        }
        else
        {
            message.NotEnough();
        }
    }

    public void buyIceKey()
    {
        if (nextLevelName == "SlimeLevel")
        {
            doNot.setIceKey(true);
        }
    }

    public void buyLavaKey()
    {
        if (doNot.getIceKey() && nextLevelName == "LavaLevel")
        {
            doNot.setLavaKey(true);
        }
    }

    public void buyThroneKey()
    {
        if (doNot.getIceKey() && doNot.getLavaKey() && nextLevelName == "ThroneRoom")
        {
            doNot.setThroneKey(true);
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
        goBackCanvas.gameObject.SetActive(true);
        player.GetComponent<PlayerCharacter>().isDialog = true;
        player.GetComponent<AudioSource>().Stop();
    }
    public void goBackConfirmationEnd()
    {
        goBackCanvas.gameObject.SetActive(false);
        player.GetComponent<PlayerCharacter>().isDialog = false;
    }

    public void goNextConfirmationStart()
    {
        goNextCanvas.gameObject.SetActive(true);
        player.GetComponent<PlayerCharacter>().isDialog = true;
        player.GetComponent<AudioSource>().Stop();
    }

    public void goNextConfirmationEnd()
    {
        goNextCanvas.gameObject.SetActive(false);
        doNotHaveKey.gameObject.SetActive(false);
        player.GetComponent<PlayerCharacter>().isDialog = false;
    }

    public void goNext()
    {
        Debug.Log("Button Clicked");
        switch (nextLevelName)
        {
            case "SlimeLevel":
                if (doNot.getIntroduceMerchant())
                {
                    loadNextScene();
                }
                else
                {
                    goNextCanvas.gameObject.SetActive(false);
                    doNotHaveKey.gameObject.SetActive(true);
                }
                
                break;

            case "IceLevel":
                if (doNot.getIceKey())
                {
                    loadNextScene();
                }
                else
                {
                    goNextCanvas.gameObject.SetActive(false);
                    doNotHaveKey.gameObject.SetActive(true);
                }
                break;

            case "LavaLevel":
                if (doNot.getLavaKey())
                {
                    loadNextScene();
                }
                else
                {
                    goNextCanvas.gameObject.SetActive(false);
                    doNotHaveKey.gameObject.SetActive(true);
                }
                break;

            case "ThroneRoom":
                if (doNot.getThroneKey())
                {
                    loadNextScene();
                }
                else
                {
                    goNextCanvas.gameObject.SetActive(false);
                    doNotHaveKey.gameObject.SetActive(true);
                }
                break;
        }
    }
}
