using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeEnemy : MonoBehaviour
{
    public ChallengeRoom challenge;
    // Start is called before the first frame update
    void Start()
    {
        
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
            challenge.addDead(1);
        }
    }
}
