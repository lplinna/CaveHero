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
        if (!displayOnce && dialogue.await==2)
        {
            dialogue.LavaKey();
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
