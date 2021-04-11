﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingBehavior : MonoBehaviour
{

    public GameObject target;
    Rigidbody2D body;
    float reftime;
    int bossState = 0;
    float DamageModifier = 0.2f;
    public DoNotDestroy doNot;
    public bool DebugFight;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        doNot = GameObject.FindGameObjectWithTag("DoNotDestroy").GetComponent<DoNotDestroy>();
        body = GetComponent<Rigidbody2D>();
        reftime = Time.time;
        this.gameObject.SetActive(false);

        if(doNot.getBeenToThrone() == true || DebugFight)
        {
            this.gameObject.SetActive(true);
            StartCoroutine(BOSSMIND());
        }


        if (DebugFight)
        {
            var q = target.GetComponent<Message0>();


            q.Teardown();
        }

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(doNot.getBeenToThrone() == true || DebugFight)
        {
            if (bossState == 0)
            {
                TailAndCharge();
            }
            if (bossState == 1)
            {
                FloatAndShoot();
            }
        }
    }


    private IEnumerator BOSSMIND()
    {
        while (true)
        {
            bossState = 0;
            reftime = Time.time;
            yield return new WaitForSeconds(12f);
            reftime = Time.time;
            bossState = 1;
            yield return new WaitForSeconds(6f);
        }


    }





    private void TailAndCharge()
    {
        var enemytimer = (Time.time - reftime) * 30f;
        var towards = target.transform.position - transform.position;
        if (enemytimer < 90)
        {
            body.velocity = towards.normalized * 6f;
            DamageModifier = 0.2f;
        }

        if (120 < enemytimer && enemytimer < 121)
        {
            body.velocity = towards.normalized * 20f;
            DamageModifier = 2f;
        }

        if (enemytimer > 150)
        {
            reftime = Time.time;
        }

    }


    private void FloatAndShoot()
    {
        var enemytimer = (Time.time - reftime) * 30f;
        var towards  = target.transform.position - transform.position;
        if (enemytimer < 90 )
        {
            Vector3 ooftacular = Vector3.Cross(towards, Vector3.forward);
            body.velocity = ooftacular.normalized * 5f;
            
        }

        if (towards.magnitude < 8f)
        {
            body.velocity = -5f * towards.normalized;
        }



        if (enemytimer > 150)
        {
            reftime = Time.time;
        }

    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        var reactor = collision.collider.gameObject;
        if (reactor.CompareTag("Player"))
        {
            reactor.GetComponent<Health>().Damage(DamageModifier);
        }

        if(reactor.name == "Stone")
        {
            if (DamageModifier == 2f) GameObject.Destroy(reactor);
        }
    }

}


