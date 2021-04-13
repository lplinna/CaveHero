using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueKing : MonoBehaviour
{
    public Animator king;
    public GameObject kingObject;

    public void walkForward()
    {
        king.SetBool("forward", true);
        king.SetBool("idle", false);
    }

    public void Idle()
    {
        king.SetBool("forward", false);
        king.SetBool("back", false);
        king.SetBool("idle", true);
    }

    public void walkBack()
    {
        king.SetBool("back", true);
        king.SetBool("idle", false);
    }

    public void slimeTransition()
    {
        king.SetBool("forward", false);
        king.SetBool("back", false);
        king.SetTrigger("transition");
    }
}
