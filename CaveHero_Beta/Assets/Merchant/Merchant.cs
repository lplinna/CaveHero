using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Merchant : MonoBehaviour
{
    public static string nextLevelName;
    public bool enterShopTrigger;
    public GameObject buyShop, sellShop;
    public Canvas goNextCanvas, goBackCanvas, doNotHaveKey;
    public DoNotDestroy doNot;
    public GameObject player;
    public GameObject UI;
    public TextMeshProUGUI healthCost, damageCost, energyCost, iceGoldCost, iceAmethystCost, lavaGoldCost, lavaRubyCost, throneGoldCost, throneEmeraldCost;
    public float healthCostFloat, damageCostFloat, energyCostFloat;
    public int stoneSell, amethystSell, rubySell, emeraldSell, diamondSell;
    public TextMeshProUGUI stoneText, amethystText, rubyText, emeraldText, diamondText;
    public MerchantMessage message;
    public MoneyCounter moneyCounter;
    public InventoryCounter inventory;

    void Awake()
    {
        enterShopTrigger = false;
        goBackCanvas.gameObject.SetActive(false);
        buyShop.SetActive(false);
        sellShop.SetActive(false);
        goNextCanvas.gameObject.SetActive(false);
        doNotHaveKey.gameObject.SetActive(false);

        MusicManager.setElevator(false);
        player = GameObject.FindGameObjectWithTag("Player");
        doNot = GameObject.FindGameObjectWithTag("DoNotDestroy").GetComponent<DoNotDestroy>();
    }

    // Start is called before the first frame update
    void Start()
    {
        iceGoldCost.text = "$" + 250;
        iceAmethystCost.text = "20";

        lavaGoldCost.text = "$" + 500;
        lavaRubyCost.text = "40";

        throneGoldCost.text = "$" + 750;
        throneEmeraldCost.text = "60";

        //REMOVE
        inventory.forceDiamond(10);
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

        stoneSell = inventory.getStone();
        stoneText.text = "$" + stoneSell;
        amethystSell = inventory.getAmethyst() * 2;
        amethystText.text = "$" + amethystSell;
        rubySell = inventory.getRuby() * 3;
        rubyText.text = "$" + rubySell;
        emeraldSell = inventory.getEmerald() * 4;
        emeraldText.text = "$" + emeraldSell;
        diamondSell = inventory.getDiamond() * 5;
        diamondText.text = "$" + diamondSell;

        if (sellShop.activeInHierarchy || buyShop.activeInHierarchy)
        {
            player.GetComponent<PlayerCharacter>().isDialog = true;
        }
    }

    public static void setNextScene(string next)
    {
        nextLevelName = next;
    }

    public void loadNextScene()
    {
        MusicManager.setElevator(true);
        MusicManager.stopPlaying();
        doNot.setChallengeTrigger(false);
        LoadingNextLevel.setLevelName(nextLevelName);
        SceneManager.LoadScene("LoadingNextLevel");
    }

    public void enterShop()
    {
        buyShop.SetActive(true);
        enterShopTrigger = true;
        player.GetComponent<PlayerCharacter>().isDialog = true;
        player.GetComponent<AudioSource>().Stop();
        UI.SetActive(false);

        Debug.Log(enterShopTrigger);
    }

    public void exitShop()
    {
        buyShop.SetActive(false);
        sellShop.SetActive(false);
        enterShopTrigger = false;
        player.GetComponent<PlayerCharacter>().isDialog = false;
        UI.SetActive(true);
        Debug.Log(enterShopTrigger);
    }

    public void enterSell()
    {
        buyShop.SetActive(false);
        sellShop.SetActive(true);
    }

    public void enterBuy()
    {
        buyShop.SetActive(true);
        sellShop.SetActive(false);
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
            if(moneyCounter.getMoney() >= 250 && inventory.getAmethyst() >= 20)
            {
                moneyCounter.TakeMoney(250);
                inventory.removeAmethyst(20);
                doNot.setIceKey(true);
            }
            else
            {
                message.NotEnough();
            }
        }
    }

    public void buyLavaKey()
    {
        if (doNot.getIceKey() && nextLevelName == "LavaLevel")
        {
            if (moneyCounter.getMoney() >= 500 && inventory.getRuby() >= 40)
            {
                moneyCounter.TakeMoney(500);
                inventory.removeRuby(40);
                doNot.setLavaKey(true);
            }
            else
            {
                message.NotEnough();
            }
        }
    }

    public void buyThroneKey()
    {
        if (doNot.getIceKey() && doNot.getLavaKey() && nextLevelName == "ThroneRoom")
        {
            if (moneyCounter.getMoney() >= 750 && inventory.getEmerald() >= 60)
            {
                moneyCounter.TakeMoney(750);
                inventory.removeEmerald(60);
                doNot.setThroneKey(true);
            }
            else
            {
                message.NotEnough();
            }
        }
    }

    public void sellStone()
    {
        moneyCounter.AddMoney(stoneSell);
        inventory.forceStone(0);
    }

    public void sellAmethyst()
    {
        moneyCounter.AddMoney(amethystSell);
        inventory.forceAmethyst(0);
    }

    public void sellRuby()
    {
        moneyCounter.AddMoney(rubySell);
        inventory.forceRuby(0);
    }

    public void sellEmerald()
    {
        moneyCounter.AddMoney(emeraldSell);
        inventory.forceEmerald(0);
    }

    public void sellDiamond()
    {
        moneyCounter.AddMoney(diamondSell);
        inventory.forceDiamond(0);
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
