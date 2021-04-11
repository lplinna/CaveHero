﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceActorBehavior : MonoBehaviour
{

    public GameObject actee;
    private int longevity = 0;


    // Start is called before the first frame update
    void Start()
    {

        longevity = 500 + (Random.Range(0, 3000));
        transform.position = actee.transform.position;
        transform.parent = actee.transform;
        freezeEnemy();
    }



    void freezeEnemy()
    {
        bool done = false;
        bool animator = true;
        if (!done && actee.CompareTag("Bat")) { actee.GetComponent<BatBehavior>().enabled = false; done = true; }
        if (!done && actee.CompareTag("Beetle")) { actee.GetComponent<BeetleBehavior>().enabled = false; done = true; }
        if (!done && actee.CompareTag("Spider")) { actee.GetComponent<SpiderBehavior>().enabled = false; done = true; }
        if (!done && actee.CompareTag("King")) { actee.GetComponent<KingBehavior>().enabled = false; done = true; animator = false; }
        actee.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        actee.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (animator) { actee.GetComponent<Animator>().speed = 0f; }
        actee.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1f);
    }

    void unfreezeEnemy()
    {
        bool done = false;
        bool animator = false;
        if (!done && actee.CompareTag("Bat")) { actee.GetComponent<BatBehavior>().enabled = true; done = true; }
        if (!done && actee.CompareTag("Beetle")) { actee.GetComponent<BeetleBehavior>().enabled = true; done = true; }
        if (!done && actee.CompareTag("Spider")) { actee.GetComponent<SpiderBehavior>().enabled = true; done = true; }
        if (!done && actee.CompareTag("King")) { actee.GetComponent<KingBehavior>().enabled = true; done = true; animator = false; }
        actee.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        if (animator) {actee.GetComponent<Animator>().speed = 1f; }
        actee.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }


    // Update is called once per frame
    void Update()
    {
        longevity -= 1;
        

        if (longevity <= 0)
        {
            unfreezeEnemy();
            GameObject.Destroy(this);
        }

    }
}