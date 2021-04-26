using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModifiers : MonoBehaviour
{
    private static PlayerModifiers instance;
    public static float healthModifier = 1f;
    public static float damageModifier = 1f;
    public static float energyModifier = 1f;
    public static bool hasNot = false;
    public static bool hasInventory = false;
    public static DoNotDestroy doNot;
    public static GameObject inventory;
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
    }


    
}
