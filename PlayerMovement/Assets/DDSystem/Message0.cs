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
