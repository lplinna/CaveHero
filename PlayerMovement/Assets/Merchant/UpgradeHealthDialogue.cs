using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHealthDialogue : MonoBehaviour
{
    public bool displayOnce;
    public MerchantMessage dialogue;
    void Start()
    {
        displayOnce = false;
    }

    public void OnMouseOver()
    {
        if (!displayOnce)
        {
            dialogue.UpgradeHealth();
            displayOnce = true;
        }
    }

    public void OnMouseExit()
    {
        dialogue.BlankSlate();
        displayOnce = false;
    }

}
