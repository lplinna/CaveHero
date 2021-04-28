using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;


public class MerchantMessage : MonoBehaviour
{
    public DialogManager DialogManager;
    public GameObject[] Example;

    public DoNotDestroy doNot;
    public int await = 0;
    public string sizeString;


    private void Awake()
    {
        DialogManager.gameObject.SetActive(false);

        if (!PlayerModifiers.hasNot)
        {
            Debug.Log(this.gameObject.name + " awaiting doNot");
            Invoke("Awake", 0.001f);
            return;
        }
        else
        {
            doNot = PlayerModifiers.doNot;
        }


    }

    void Update()
    {
        if (this.gameObject.activeSelf && !doNot.getIntroduceMerchant())
        {
            Introduction();
        }
    }



    private IEnumerator AwaitDLOG() //Enables player movement after dialogue ends.
    {
        while (true)
        {
            if (DialogManager.state != State.Deactivate) yield return null;
            if (DialogManager.state == State.Deactivate)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().isDialog = false;

                if (await == 1) await = 2;

                yield break;
            }
        }
    }





    public void Advance()
    {
        DialogManager.Click_Window();
    }

    public void BlankSlate()
    {
        DialogManager.gameObject.SetActive(true);
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("/close/", "Li"));
        DialogManager.Show(dialogTexts);
    }


    public void Introduction()
    {
        DialogManager.gameObject.SetActive(true);

        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("\"/speed:0.03/I've never seen you before. You new here or something?\"", "Li"));
        dialogTexts.Add(new DialogData("\"/speed:0.03/You aren't exactly what I normally see in these mineshafts.\"", "Li"));
        dialogTexts.Add(new DialogData("\"/speed:0.03/Well, nonetheless, the name's Axol! And welcome to my traveling shop.\"", "Li"));
        dialogTexts.Add(new DialogData("\"/speed:0.03/I sell some upgrades that can keep you alive in these mineshafts.\"", "Li"));
        dialogTexts.Add(new DialogData("\"/speed:0.03/I also sell the keys to the mineshafts. They do cost both money and materials to make though.\"", "Li"));
        dialogTexts.Add(new DialogData("\"/speed:0.03/I've been here many years,/wait:0.3/ and know many things.\"", "Li"));
        dialogTexts.Add(new DialogData("\"/speed:0.03/I heard the cave collapse./wait:0.6/Was that you? I hope you are okay.", "Li"));
        dialogTexts.Add(new DialogData("\"/speed:0.03/If you're running errands for the king, take this key. Go through that door to the right. Get mining!\"", "Li"));
        dialogTexts.Add(new DialogData("\"/speed:0.03/If you finish a mineshaft and want to go back, just head back to the left door!\"", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
        doNot.setIntroduceMerchant(true);
        await = 1;
    }

    public void UpgradeHealth()
    {
        if (doNot.getIntroduceMerchant())
        {
            DialogManager.gameObject.SetActive(true);
            BlankSlate();
            var dialogTexts = new List<DialogData>();
            if (PlayerModifiers.healthModifier < 2f)
            {
                //Add text here
                dialogTexts.Add(new DialogData("\"That will upgrade your health by 50%! Just click if you wish to buy!\"", "Li"));
                //End of text
            }
            else
            {
                dialogTexts.Add(new DialogData("\"Sorry I've run out of those upgrades!\"", "Li"));
            }
            DialogManager.Show(dialogTexts);
        }
    }

    public void UpgradeEnergy()
    {
        if (doNot.getIntroduceMerchant())
        {
            DialogManager.gameObject.SetActive(true);
            BlankSlate();
            var dialogTexts = new List<DialogData>();
            if (PlayerModifiers.energyModifier < 2f)
            {
                //Add text here
                dialogTexts.Add(new DialogData("\"That will upgrade your energy by 50%! Just click if you wish to buy!\"", "Li"));
                //End of text
            }
            else
            {
                dialogTexts.Add(new DialogData("\"Sorry I've run out of those upgrades!\"", "Li"));
            }
            DialogManager.Show(dialogTexts);
        }
    }

    public void UpgradeDamage()
    {
        if (doNot.getIntroduceMerchant())
        {
            DialogManager.gameObject.SetActive(true);
            BlankSlate();
            var dialogTexts = new List<DialogData>();
            if (PlayerModifiers.damageModifier < 2f)
            {
                //Add text here
                dialogTexts.Add(new DialogData("\"That will upgrade your damage by 50%! Just click if you wish to buy!\"", "Li"));
                //End of text
            }
            else
            {
                dialogTexts.Add(new DialogData("\"Sorry I've run out of those upgrades!\"", "Li"));
            }
            DialogManager.Show(dialogTexts);
        }
    }

    public void IceKey()
    {
        if (doNot.getIntroduceMerchant())
        {
            DialogManager.gameObject.SetActive(true);
            BlankSlate();
            var dialogTexts = new List<DialogData>();
            if (!doNot.getIceKey())
            {
                //Add text here
                dialogTexts.Add(new DialogData("\"That is the key to the elevator for the next mineshaft! Be sure you are ready!\"", "Li"));
                //End of text
            }
            else
            {
                dialogTexts.Add(new DialogData("\"Seems you already own this one, pal\"", "Li"));
            }
            DialogManager.Show(dialogTexts);
        }
    }

    public void LavaKey()
    {
        if (doNot.getIntroduceMerchant())
        {
            DialogManager.gameObject.SetActive(true);
            BlankSlate();
            var dialogTexts = new List<DialogData>();
            if (!doNot.getLavaKey() && doNot.getIceKey())
            {
                //Add text here
                dialogTexts.Add(new DialogData("\"That is the key to the elevator for the next mineshaft! Be sure you are ready!\"", "Li"));
                //End of text
            }
            else if (!doNot.getIceKey())
            {
                dialogTexts.Add(new DialogData("\"You have to buy the ice key first pal, sorry!\"", "Li"));
            }
            else
            {
                dialogTexts.Add(new DialogData("\"Seems you already own this one, pal\"", "Li"));
            }
            DialogManager.Show(dialogTexts);
        }
    }

    public void ThroneKey()
    {
        if (doNot.getIntroduceMerchant())
        {
            DialogManager.gameObject.SetActive(true);
            BlankSlate();
            var dialogTexts = new List<DialogData>();
            if (!doNot.getThroneKey() && doNot.getLavaKey() && doNot.getIceKey())
            {
                //Add text here
                dialogTexts.Add(new DialogData("\"That is the key to the final elevator for the next leg of your journey! Good luck!\"", "Li"));
                //End of text
            }
            else if (!doNot.getIceKey())
            {
                dialogTexts.Add(new DialogData("\"You have to buy the ice key first pal, sorry!\"", "Li"));
            }
            else if (!doNot.getLavaKey() && doNot.getIceKey())
            {
                dialogTexts.Add(new DialogData("\"You have to buy the lava key first pal, sorry!\"", "Li"));
            }
            else
            {
                dialogTexts.Add(new DialogData("\"Seems you already own this one, pal\"", "Li"));
            }
            DialogManager.Show(dialogTexts);
        }
    }

    public void NotEnough()
    {
        DialogManager.gameObject.SetActive(true);
        BlankSlate();
        var dialogTexts = new List<DialogData>();

        //Add text here
        dialogTexts.Add(new DialogData("\"/speed:0.03/It seems you don't have enough for that! Sorry pal!\"", "Li"));
        //End of text

        DialogManager.Show(dialogTexts);
        StartCoroutine(AwaitDLOG());
        doNot.setIntroduceMerchant(true);
        await = 1;
    }
}
