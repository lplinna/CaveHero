using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class DialogueKing : MonoBehaviour
{
    public Animator king;
    public GameObject kingObject;
    public GameObject fightKing;
    public SpriteRenderer drill;
    public Sprite drillWithGem;

    DialogManager b;

    private void Start()
    {
        try
        {
            b = GameObject.Find("Player").GetComponentInChildren<DialogManager>();
        }
        catch
        {
            Invoke("Start", 0.2f);
        }
    }





    private void Update()
    {
        var z = GameObject.Find("Player");
        Animator playerAnim = z.GetComponent<Animator>();
        if (b._current_Data.Character == "action1")
        {
            Vector3 place1 = new Vector3(-0.00999999978f, 4.6500001f, 0);      
            walkForward();
            if (Vector3.Distance(z.transform.position,place1) > 0.2f)
            {
                z.transform.position = Vector3.Lerp(z.transform.position, place1, 0.002f);
                playerAnim.SetBool("Idle", false);
                playerAnim.SetBool("WalkUp", true);
                playerAnim.SetBool("WalkLeft", false);
                playerAnim.SetBool("WalkRight", false);
                playerAnim.SetBool("WalkDown", false);
            }
            else
            {
                playerAnim.SetBool("Idle", false);
                playerAnim.SetBool("WalkUp", false);
                playerAnim.SetBool("WalkLeft", false);
                playerAnim.SetBool("WalkRight", false);
                playerAnim.SetBool("WalkDown", false);
                playerAnim.speed = 0f;
            }
        }


        GameObject fightking = GameObject.Find("FightKing");

        if (b._current_Data.Character == "slimeWalkDown")
        {
            walkForward();
            Vector3 place1 = new Vector3(-0.00999999978f, 4.6500001f, 0);
            
            transform.position = Vector3.Lerp(transform.position, place1, 0.002f);
        }

        if (b._current_Data.Character == "transformTime")
        {
            var g = GetComponent<Animator>();
            g.Play("SlimeTransition");
            MusicManager.stopPlaying();
            MusicManager.setBoss(true);
            drill.sprite = drillWithGem;
        }

        if(b._current_Data.Character == "goKING")
        {
            var g = GetComponent<Animator>();
            g.Play("NoKing");
            var q = Instantiate(fightKing);
            q.transform.position = transform.position;
            b._current_Data.Character = "NONE";

        }

        
    }




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
    }
}
