using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEnergy : MonoBehaviour
{

    public void OnButtonPress()
    {
        if (PlayerModifiers.energyModifier < 2f)
        {
            PlayerModifiers.energyModifier += 0.5f;
            Debug.Log(PlayerModifiers.energyModifier);
        }
    }
}
