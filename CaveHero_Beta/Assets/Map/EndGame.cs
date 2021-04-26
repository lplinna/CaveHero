using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject endGame;
    public DoNotDestroy doNot;
    // Start is called before the first frame update
    void Start()
    {


        if (!PlayerModifiers.hasNot)
        {
            Debug.Log(this.gameObject.name + " awaiting doNot");
            Invoke("Start", 0.001f);
            return;
        }
        else
        {
            doNot = PlayerModifiers.doNot;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!doNot)
        {
            doNot = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().doNot;
        }

        else
        {
            if (doNot.getKingDead())
            {
                endGame.SetActive(true);
            }
        }
    }
}
