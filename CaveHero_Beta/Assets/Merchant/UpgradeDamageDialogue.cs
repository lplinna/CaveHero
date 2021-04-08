using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDamageDialogue : MonoBehaviour
{
    public bool displayOnce;
    public MerchantMessage dialogue;
    void Start()
    {
        displayOnce = false;
    }

    public void OnMouseOver()
    {

        if (!displayOnce && dialogue.await == 2)
        {
            dialogue.UpgradeDamage();
            displayOnce = true;
        }
    }

    public void OnMouseExit()
    {
        if (dialogue.await == 2)
        {
            dialogue.BlankSlate();
            displayOnce = false;
        }
    }

}
