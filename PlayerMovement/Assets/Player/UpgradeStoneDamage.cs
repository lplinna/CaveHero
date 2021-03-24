using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStoneDamage : MonoBehaviour
{
    public Slider slider;
    public void OnButtonPress()
    {
        if (PlayerModifiers.stoneDamageModifier < 2f)
        {
            PlayerModifiers.stoneDamageModifier = 2.0f;
            slider.value = PlayerModifiers.stoneDamageModifier;
            Debug.Log(PlayerModifiers.stoneDamageModifier);
        }
    }
}
