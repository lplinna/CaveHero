using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeEnemy : MonoBehaviour
{
    public GameObject challenge;
    // Start is called before the first frame update
    void Start()
    {
        challenge = GameObject.Find("ChallengeRoom");
    }

    // Update is called once per frame
    void Update()
    {
        Death();
    }

    public void Death()
    {
        if(!this.gameObject.activeSelf)
        {
            challenge.SendMessage("addDead", 1);
        }
    }
}
