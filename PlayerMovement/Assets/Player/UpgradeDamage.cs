using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDamage : MonoBehaviour
{
    public Slider slider;
    public void OnButtonPress()
    {
        if (PlayerModifiers.damageModifier < 2f)
        {
            PlayerModifiers.damageModifier += 0.5f;
            slider.value = PlayerModifiers.damageModifier;
            Debug.Log(PlayerModifiers.damageModifier);
        }
    }

    void Awake()
    {
        slider.value = PlayerModifiers.damageModifier;
    }
}
