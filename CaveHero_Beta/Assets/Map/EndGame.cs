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


        try
        {
            doNot = GameObject.FindGameObjectWithTag("DoNotDestroy").GetComponent<DoNotDestroy>();
        }
        catch
        {
            Invoke("Start", 0.2f);
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
