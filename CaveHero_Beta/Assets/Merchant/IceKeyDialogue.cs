using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceKeyDialogue : MonoBehaviour
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
            dialogue.IceKey();
            displayOnce = true;
        }
    }

    public void OnMouseExit()
    {
        dialogue.BlankSlate();
        displayOnce = false;
    }
}
