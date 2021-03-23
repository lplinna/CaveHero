using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHealth : MonoBehaviour
{
    public Slider slider;

    public void OnButtonPress()
    {
        if (PlayerModifiers.healthModifier < 2f)
        {
            PlayerModifiers.healthModifier += 0.5f;
            slider.value = PlayerModifiers.healthModifier;
            Debug.Log(PlayerModifiers.healthModifier);
        }
    }
}
