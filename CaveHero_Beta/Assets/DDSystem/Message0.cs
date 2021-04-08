using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;


public class Message0 : MonoBehaviour
{
    public DialogManager DialogManager;
    public DoNotDestroy doNot;
    public GameObject[] Example;

    void Awake()
    {
        doNot = GameObject.FindGameObjectWithTag("DoNotDestroy").GetComponent<DoNotDestroy>();
        DialogManager.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "TutorialLevel")
        {
            StartTutorial();
        }
        if (SceneManager.GetActiveScene().name == "SlimeLevel")
        {
            //SlimeLevel1();
        }
        if (SceneManager.GetActiveScene().name == "Merchant")
        {
            Merchant1();
        }
        if(SceneManager.GetActiveScene().name == "ThroneRoom" && doNot.getBeenToThrone() == false)
        {
            Throne1();
        }
    }



    private IEnumerator AwaitDLOG() //Enables player movement after dialogue ends.
    {
        while (true)
        {
            if(DialogManager.state!=State.Deactivate) yield return null;
            if (DialogManager.state == State.Deactivate)
            {
                this.GetComponent<PlayerCharacter>().isDialog = false;
                yield break;
            }
        }
    }



public  void Advance()
    {
        DialogManager.Click_Window();
    }

    public void StartTutorial()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("\"Just another day of mining, gotta meet quota.\"", "Li"));
        dialogTexts.Add(new DialogData("You can move around by using the W, A, S, D keys.", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void LeftClickTutorial()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("You can use the Left Mouse Click to mine rocks.", "Li"));
        dialogTexts.Add(new DialogData("Rocks will drop some pickups upon being mined. These pickups are important and will get you money.", "Li"));
        dialogTexts.Add(new DialogData("You can see how much money you have by the box with the '$' in it.", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void ShiftSprintTutorial()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("You can use the Left Shift key to sprint, but be wary, you only have a limited amount of energy.", "Li"));
        dialogTexts.Add(new DialogData("Your energy bar is located in the top right of your screen, symbolized by the green bolt.", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void CombatTutorial()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("You can use Left Mouse Click to attack enemies. If you are in a tight spot, you can use Right Mouse Click to do a ground slam attack.", "Li"));
        dialogTexts.Add(new DialogData("Be wary though, the ground slam attack takes away some of your energy.", "Li"));
        dialogTexts.Add(new DialogData("Remember to keep your health above 0 or else you will perish! Your health bar is located above your energy bar symbolized by the heart.", "Li"));

        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void SlimeLevel1()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("\"Just another day of mining I guess, just a new boss\"", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void Merchant1()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();
        //Add text here
        dialogTexts.Add(new DialogData("\"Maybe this can go over here..... hmmm. Nah that doesn't look good. Maybe here?\"", "Li"));
        dialogTexts.Add(new DialogData("\"That looks good. I should... wait what was that? Hello? Anyone there?\"", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void Throne1()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("\"WHAT HAVE YOU DONE TO MY THRONE ROOM YOU PEASANT?!\"", "Li"));
        dialogTexts.Add(new DialogData("\"THIS WILL COST SO MUCH TO FIX! YOU WILL PAY FOR IT ALL! THIS IS YOUR FAULT!\"", "Li"));
        dialogTexts.Add(new DialogData("\"Wait... are you a... human? I haven't seen one of you in a long while...\"", "Li"));
        dialogTexts.Add(new DialogData("\"How did you... /click//speed:init/well obviously you fell or something, hence all the rocks in my throne room.\"", "Li"));
        dialogTexts.Add(new DialogData("\"Are you a miner, by any chance? /click//speed:init/I noticed your pickaxe and hard hat.\"", "Li"));
        dialogTexts.Add(new DialogData("\"How about we make a deal? You get something from the mines for me, and I'll help you get back home.\"", "Li"));
        dialogTexts.Add(new DialogData("\"/click//speed:init/Not much of a talker, huh? I'm going to take your silence as a form of agreement.\"", "Li"));
        dialogTexts.Add(new DialogData("\"I need a specific gem for a drill to help you get out of here. We need the drill to expand our city.\"", "Li"));
        dialogTexts.Add(new DialogData("\"This gem is at the bottom of the mineshafts connected to the elevator to my left.\"", "Li"));
        dialogTexts.Add(new DialogData("\"Please enter whenever you are ready. Thanks for your help!\"", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void Throne2()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("\"Now can someone please clean up these rocks? They are a dreadful sight.\"", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }
}
