using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryCounter : MonoBehaviour
{
    public int stoneCount, amethystCount, emeraldCount, rubyCount, diamondCount;
    public TextMeshProUGUI stoneText, amethystText, emeraldText, rubyText, diamondText;
    private InventoryCounter instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        stoneCount = 0;
        amethystCount = 0;
        emeraldCount = 0;
        rubyCount = 0;
        diamondCount = 0;
    }

    void Update()
    {
        updateStone();
        updateAmethyst();
        updateEmerald();
        updateRuby();
        updateDiamond();
    }

    public void forceStone(int value)
    {
        stoneCount = value;
    }

    public void forceAmethyst(int value)
    {
        amethystCount = value;
    }

    public void forceEmerald(int value)
    {
        emeraldCount = value;
    }

    public void forceRuby(int value)
    {
        rubyCount = value;
    }

    public void forceDiamond(int value)
    {
        diamondCount = value;
    }

    // STONE GETTERS AND SETTERS
    public void addStone(int value)
    {
        stoneCount += value;
    }

    public int getStone()
    {
        return stoneCount;
    }

    public void updateStone()
    {
        stoneText.text = "" + stoneCount;
    }

    public void removeStone(int value)
    {
        stoneCount -= value;
    }

    // AMETHYST GETTERS AND SETTERS
    public void addAmethyst(int value)
    {
        amethystCount += value;
    }

    public int getAmethyst()
    {
        return amethystCount;
    }

    public void updateAmethyst()
    {
        amethystText.text = "" + amethystCount;
    }

    public void removeAmethyst(int value)
    {
        amethystCount -= value;
    }

    // EMERALD GETTERS AND SETTERS
    public void addEmerald(int value)
    {
        emeraldCount += value;
    }

    public int getEmerald()
    {
        return emeraldCount;
    }

    public void updateEmerald()
    {
        emeraldText.text = "" + emeraldCount;
    }

    public void removeEmerald(int value)
    {
        emeraldCount -= value;
    }

    // RUBY GETTERS AND SETTERS
    public void addRuby(int value)
    {
        rubyCount += value;
    }

    public int getRuby()
    {
        return rubyCount;
    }

    public void updateRuby()
    {
        rubyText.text = "" + rubyCount;
    }

    public void removeRuby(int value)
    {
        rubyCount -= value;
    }

    // DIAMOND GETTERS AND SETTERS 
    public void addDiamond(int value)
    {
        diamondCount += value;
    }

    public int getDiamond()
    {
        return diamondCount;
    }

    public void updateDiamond()
    {
        diamondText.text = "" + diamondCount;
    }

    public void removeDiamond(int value)
    {
        diamondCount -= value;
    }
}
