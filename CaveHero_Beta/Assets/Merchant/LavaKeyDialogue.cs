using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaKeyDialogue : MonoBehaviour
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
            dialogue.LavaKey();
            displayOnce = true;
        }
    }

    public void OnMouseExit()
    {
        dialogue.BlankSlate();
        displayOnce = false;
    }
}
