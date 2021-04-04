using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;


public class Message0 : MonoBehaviour
{
    public DialogManager DialogManager;

    public GameObject[] Example;

    private void Awake()
    {
        DialogManager.gameObject.active = false;

        
       
        if (SceneManager.GetActiveScene().name == "SlimeLevel")
        {
            DoConvo1();
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



    private void DoConvo1()
    {

        DialogManager.gameObject.active = true;
        this.GetComponent<PlayerCharacter>().isDialog = true;
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("Ahh, another day at the mine.", "Li"));
        dialogTexts.Add(new DialogData("I can smack rocks with left click!", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
    }

    private void DoConvo2()
    {
        var dialogTexts = new List<DialogData>();
        //Add text here
        dialogTexts.Add(new DialogData("Ahh, another day at the mine.", "Li"));
        dialogTexts.Add(new DialogData("I can smack rocks with left click!", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
    }

    

    private void Show_Example(int index)
    {
        Example[index].SetActive(true);
    }
}
