using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;


public class Message0 : MonoBehaviour
{
    public DialogManager DialogManager;
    public DoNotDestroy doNot;
    public bool slimeToGrandpa;
    public bool endGame;
    public DialogueKing dialogueKing;
    void Awake()
    {
        //Invoke("GetDoNot", 0f);
        try
        {
            dialogueKing = GameObject.FindGameObjectWithTag("DialogueKing").GetComponent<DialogueKing>();
        }
        catch { }

       

        try
        {
            doNot = GameObject.FindGameObjectWithTag("DoNotDestroy").GetComponent<DoNotDestroy>();
        }
        catch
        {
            Invoke("Awake", 0.001f);
        }

        try
        {
            DialogManager = GameObject.Find("Player").GetComponentInChildren<DialogManager>();
            DialogManager.gameObject.SetActive(false);
        }
        catch
        {
            Invoke("Awake", 0.001f);
        }

        

        MessageChoices();

    }

    

    void MessageChoices()
    {
        if (SceneManager.GetActiveScene().name == "TutorialLevel")
        {
            StartTutorial();
        }
        if (SceneManager.GetActiveScene().name == "SlimeLevel")
        {
            SlimeLevel1();
        }
        if (SceneManager.GetActiveScene().name == "Merchant" && !doNot.getIntroduceMerchant())
        {

            Merchant1();
        }

        if (SceneManager.GetActiveScene().name == "ThroneRoom" && !doNot.getBeenToThrone())
        {
            Throne1();

        }

        if (SceneManager.GetActiveScene().name == "ThroneRoom" && doNot.getBeenToThrone())
        {
            Throne2();
        }
    }

    void Update()
    {
        if (!DialogManager.gameObject.activeInHierarchy && slimeToGrandpa)
        {
            dialogueKing.slimeTransition();
        }
        if (!DialogManager.gameObject.activeInHierarchy && endGame)
        {
            LoadingNextLevel.setLevelName("TitleScreen");
            SceneManager.LoadScene("LoadingNextLevel");
        }
    }



    private IEnumerator AwaitDLOG() //Enables player movement after dialogue ends.
    {
        while (true)
        {
            if (DialogManager.state != State.Deactivate) yield return null;
            if (DialogManager.state == State.Deactivate)
            {
                this.GetComponent<PlayerCharacter>().isDialog = false;
                yield break;
            }
        }
    }



    public void Teardown()
    {
        DialogManager.gameObject.SetActive(false);
        this.GetComponent<PlayerCharacter>().isDialog = false;
    }


    public void Advance()
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
        dialogTexts.Add(new DialogData("/speed:0.04/\"Just another day of mining, gotta meet my quota.\"", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/You can move around by using the W, A, S, D keys.", "Li"));
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
        dialogTexts.Add(new DialogData("/speed:0.04/You can use the Left Mouse Click to mine rocks.", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/Rocks will drop some pickups upon being mined. These pickups are important and will get you money.", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/You can see how much money you have by the box with the '$' in it.", "Li"));
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
        dialogTexts.Add(new DialogData("/speed:0.04/You can use the Left Shift key to sprint, but be wary, you only have a limited amount of energy.", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/Your energy bar is located in the top right of your screen, symbolized by the green bolt.", "Li"));
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
        dialogTexts.Add(new DialogData("/speed:0.04/You can use Left Mouse Click to attack enemies. If you are in a tight spot, you can use Right Mouse Click to do a ground slam attack.", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/Be wary though, the ground slam attack takes away some of your energy.", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/Remember to keep your health above 0 or else you will perish! Your health bar is located above your energy bar symbolized by the heart.", "Li"));

        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void ChestTutorial()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("/speed:0.04/Seems you have found yourself a chest! You can hit it with your pickaxe to open it up!", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/There are many possible treasures inside so be sure to keep a look out for them!", "Li"));
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
        dialogTexts.Add(new DialogData("/speed:0.04/\"Just another day of mining I guess, just a new boss\"", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/You can open up your inventory to see what kind of rocks you have collected with the I key.", "Li"));
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
        dialogTexts.Add(new DialogData("\"How did you... /click//speed:init/well obviously you fell or something, hence all the rocks in my throne room.\"", "action1"));
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

        transform.Translate(new Vector3(0f, 3f));
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("/size:140//speed:0.02/\"SERVANTS!!\"", "Li"));
        dialogTexts.Add(new DialogData("\"/speed:0.06/Why are these rocks still on my floor?!?\"", "slimeWalkDown"));
        dialogTexts.Add(new DialogData("\"I told you to-/wait:0.5/-/wait:0.5/-\"", "slimeWalkDown"));
        dialogTexts.Add(new DialogData("\"Oh.\"", "Li"));
        dialogTexts.Add(new DialogData("\"The Gem? You have it?? \"", "Li"));
        dialogTexts.Add(new DialogData("\"Excellent. Now the drill will work!\"", "Li"));
        dialogTexts.Add(new DialogData("\"But there is one problem...\"", "Li"));
        dialogTexts.Add(new DialogData("\"The drill is a relic, a tool with a mind. It will only let one of us return to the surface.\"", "Li"));
        dialogTexts.Add(new DialogData("\"And I have been banished from there for far, far too long.\"", "Li"));
        dialogTexts.Add(new DialogData("\"Your service to me has been splendid, miner...\"", "Li"));
        dialogTexts.Add(new DialogData("*shuffling*", "transformTime"));
        dialogTexts.Add(new DialogData("\"But your time ends here!\"", "Li"));
        dialogTexts.Add(new DialogData("/close/", "goKING"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
        slimeToGrandpa = true;
    }


    public void Throne3()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("/speed:0.02/\"N-/wait:0.5/noo! /wait:1/Impossible!\"", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.02/\"The.../wait:1/ The drill is mine...\"", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.02/\"IT ALWAYS HAS BEEN!\"", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.02/\"U-ughghhh..\"", "Li"));
        //End of text
        doNot.setKingDead(true);
        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void PoisonAttackGained()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("/speed:0.04/You have gained the Poison Attack!", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/By clicking the E key, you can now poison your enemies!", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void IceAttackGained()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("/speed:0.04/You have gained the Ice Attack!", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/By clicking the R key, you can now freeze your enemies!", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void FireAttackGained()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("/speed:0.04/You have gained the Fire Attack!", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/By clicking the F key, you can now burn your enemies! But be wary! It comes at a cost!", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/You will lose some energy and health each time you attempt to burn an enemy.", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void Sapphire()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("/speed:0.04/You have found the sapphire that the king needs!", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/Time to go forth and bring him the sapphire!", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void EndGame()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        this.GetComponent<AudioSource>().Stop();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("/speed:0.04/You have defeated the King!", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/Time to go back to the surface. Back to your home.", "Li"));
        dialogTexts.Add(new DialogData("/speed:0.04/Thank you for playing.", "Li"));
        //End of text
        endGame = true;
        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

}
