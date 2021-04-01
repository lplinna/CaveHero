using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDamage : MonoBehaviour
{
    public void OnButtonPress()
    {
        if (PlayerModifiers.damageModifier < 2f)
        {
            PlayerModifiers.damageModifier += 0.5f;
            Debug.Log(PlayerModifiers.damageModifier);
        }
    }
}
