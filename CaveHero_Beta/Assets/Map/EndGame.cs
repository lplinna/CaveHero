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
        doNot = GameObject.FindGameObjectWithTag("DoNotDestroy").GetComponent<DoNotDestroy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(doNot.getKingDead())
        {
            endGame.SetActive(true);
        }
    }
}
