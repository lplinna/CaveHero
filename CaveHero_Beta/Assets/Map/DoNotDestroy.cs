using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    private DoNotDestroy instance;
    public bool slimeKey, iceKey, lavaKey, throneKey;
    public bool introduceMerchant;
    public bool challengeRoomFinished;
    public bool beenToThrone;
    public float lastingMoney = 0f;
    public bool hasReset;
    public bool sapphire;
    public bool poisonAttack, iceAttack, fireAttack;
    public bool kingDead;
    public GameObject invRef;

    public int stoneCount, amethystCount, emeraldCount, rubyCount, diamondCount;
    void Awake()
    {
        gameObject.tag = "DoNotDestroy";
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        stoneCount = 0;
        amethystCount = 0;
        emeraldCount = 0;
        rubyCount = 0;
        diamondCount = 0;
    }

    public void setChallengeTrigger(bool trigger)
    {
        challengeRoomFinished = trigger;
    }

    public bool getChallengeTrigger()
    {
        return challengeRoomFinished;
    }

    public void setIceKey(bool key)
    {
        iceKey = key;
    }

    public bool getIceKey()
    {
        return iceKey;
    }

    public void setLavaKey(bool key)
    {
        lavaKey = key;
    }

    public bool getLavaKey()
    {
        return lavaKey;
    }

    public void setThroneKey(bool key)
    {
        throneKey = key;
    }

    public bool getThroneKey()
    {
        return throneKey;
    }

    public void setIntroduceMerchant(bool trigger)
    {
        introduceMerchant = trigger;
    }

    public bool getIntroduceMerchant()
    {
        return introduceMerchant;
    }

    public void setBeenToThrone(bool trigger)
    {
        beenToThrone = trigger;
    }

    public bool getBeenToThrone()
    {
        return beenToThrone;
    }
    public bool getHasReset()
    {
        return hasReset;
    }

    public bool getPoisonAttack()
    {
        return poisonAttack;
    }

    public void setPoisonAttack(bool trigger)
    {
        poisonAttack = trigger;
    }

    public bool getIceAttack()
    {
        return iceAttack;
    }

    public void setIceAttack(bool trigger)
    {
        iceAttack = trigger;
    }

    public bool getFireAttack()
    {
        return fireAttack;
    }

    public void setFireAttack(bool trigger)
    {
        fireAttack = trigger;
    }

    public bool getSapphire()
    {
        return sapphire;
    }

    public void setSapphire(bool trigger)
    {
        sapphire = trigger;
    }

    public bool getKingDead()
    {
        return kingDead;
    }

    public void setKingDead(bool trigger)
    {
        kingDead = trigger;
    }
}
