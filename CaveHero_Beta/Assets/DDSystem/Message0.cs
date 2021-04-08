using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;


public class Message0 : MonoBehaviour
{
    public DialogManager DialogManager;

    public GameObject[] Example;

    void Awake()
    {
        DialogManager.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "TutorialLevel")
        {
            StartTutorial();
        }
        if (SceneManager.GetActiveScene().name == "SlimeLevel")
        {
            SlimeLevel1();
        }
        if (SceneManager.GetActiveScene().name == "Merchant")
        {
            Merchant1();
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
        dialogTexts.Add(new DialogData("\"/speed:up/Just another day of mining, gotta meet quota.\"", "Li"));
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
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("Ahh, another day at the mine.", "Li"));
        dialogTexts.Add(new DialogData("I can smack rocks with left click!", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    public void Merchant1()
    {
        DialogManager.gameObject.SetActive(true);
        this.GetComponent<PlayerCharacter>().isDialog = true;
        var dialogTexts = new List<DialogData>();
        //Add text here
        dialogTexts.Add(new DialogData("Maybe this can go over here..... hmmm. Nah that doesn't look good. Maybe here?", "Li"));
        dialogTexts.Add(new DialogData("That looks good. I should... wait what was that? Hello? Anyone there?", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    

    private void Show_Example(int index)
    {
        Example[index].SetActive(true);
    }
}
