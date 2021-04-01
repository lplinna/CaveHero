using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHealth : MonoBehaviour
{
    public void OnButtonPress()
    {
        if (PlayerModifiers.healthModifier < 2f)
        {
            PlayerModifiers.healthModifier += 0.5f;
            Debug.Log(PlayerModifiers.healthModifier);
        }
    }
}
