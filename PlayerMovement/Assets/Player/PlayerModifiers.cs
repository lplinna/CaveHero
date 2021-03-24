using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModifiers : MonoBehaviour
{
    private static PlayerModifiers instance;
    public static float healthModifier = 1f;
    public static float damageModifier = 1f;
    public static float stoneDamageModifier = 1f;
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
