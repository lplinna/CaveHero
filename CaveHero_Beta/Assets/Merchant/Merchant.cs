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



        if (!PlayerModifiers.hasNot)
        {
            Debug.Log(this.gameObject.name + " awaiting doNot");
            Invoke("Awake", 0.001f);
            return;
        }
        else
        {
            doNot = PlayerModifiers.doNot;
            inventory = PlayerModifiers.inventory.GetComponent<InventoryCounter>();
        }
    }


    


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (!doNot.getIceKey())
        {
            iceGoldCost.text = "$" + 350;
            iceAmethystCost.text = "50";
        }
        else
        {
            iceGoldCost.text = "---";
            iceAmethystCost.text = "--";
        }

        if (!doNot.getLavaKey())
        {
            lavaGoldCost.text = "$" + 700;
            lavaRubyCost.text = "70";
        }
        else
        {
            lavaGoldCost.text = "---";
            lavaRubyCost.text = "--";
        }

        if (!doNot.getThroneKey())
        {
            throneGoldCost.text = "$" + 1000;
            throneEmeraldCost.text = "90";
        }
        else
        {
            throneGoldCost.text = "---";
            throneEmeraldCost.text = "--";
        }

        if (PlayerModifiers.healthModifier < 5f)
        {
            healthCostFloat = 150 * PlayerModifiers.healthModifier;
            healthCost.text = "$" + healthCostFloat;
        }
        else
        {
            healthCost.text = "-----";
        }

        if (PlayerModifiers.energyModifier < 5f)
        {
            energyCostFloat = 200 * PlayerModifiers.energyModifier;
            energyCost.text = "$" + energyCostFloat;
        }
        else
        {
            energyCost.text = "-----";
        }
        
        if (PlayerModifiers.damageModifier < 5f)
        {
            damageCostFloat = 250 * PlayerModifiers.damageModifier;
            damageCost.text = "$" + damageCostFloat;
        }
        else
        {
            damageCost.text = "-----";
        }

        stoneSell = inventory.getStone() * 1;
        stoneText.text = "$" + stoneSell;
        amethystSell = (int)(inventory.getAmethyst() * 2);
        amethystText.text = "$" + amethystSell;
        rubySell = (int)(inventory.getRuby() * 2.5);
        rubyText.text = "$" + rubySell;
        emeraldSell = (int)(inventory.getEmerald() * 3);
        emeraldText.text = "$" + emeraldSell;
        diamondSell = (int)(inventory.getDiamond() * 4);
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
        player.GetComponent<PlayerCharacter>().shop = buyShop;
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
            if (PlayerModifiers.damageModifier < 5f)
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
            if (PlayerModifiers.healthModifier < 5f)
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
            if (PlayerModifiers.energyModifier < 5f)
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
        if(moneyCounter.getMoney() >= 350 && inventory.getAmethyst() >= 50)
        {
            Debug.Log("ICE KEY HAS BEEN BOUGHT");
            moneyCounter.TakeMoney(350);
            inventory.removeAmethyst(50);
            doNot.setIceKey(true);
        }
        else
        {
            message.NotEnough();
        }
    }

    public void buyLavaKey()
    {
        if (moneyCounter.getMoney() >= 700 && inventory.getRuby() >= 70)
        {
            moneyCounter.TakeMoney(700);
            inventory.removeRuby(70);
            doNot.setLavaKey(true);
        }
        else
        {
            message.NotEnough();
        }
    }

    public void buyThroneKey()
    {
        if (moneyCounter.getMoney() >= 1000 && inventory.getEmerald() >= 90)
        {
            moneyCounter.TakeMoney(1000);
            inventory.removeEmerald(90);
            doNot.setThroneKey(true);
        }
        else
        {
            message.NotEnough();
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
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().inventoryCounter;
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
