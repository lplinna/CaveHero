using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModifiers : MonoBehaviour
{
    private static PlayerModifiers instance;
    public static float healthModifier;
    public static float damageModifier;
    public static float stoneDamageModifier;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            healthModifier = 1f;
            damageModifier = 1f;
            stoneDamageModifier = 1f;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
