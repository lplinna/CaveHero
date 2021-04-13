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
        challengeRoomFinished = false;
        iceKey = false;
        lavaKey = false;
        throneKey = false;
        introduceMerchant = false;
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
}
