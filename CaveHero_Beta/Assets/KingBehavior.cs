using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingBehavior : MonoBehaviour
{

    public GameObject target;
    public GameObject rocks;
    Rigidbody2D body;
    float reftime;
    int bossState = 0;
    public DoNotDestroy doNot;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        doNot = GameObject.FindGameObjectWithTag("DoNotDestroy").GetComponent<DoNotDestroy>();
        body = GetComponent<Rigidbody2D>();
        reftime = Time.time;
        this.gameObject.SetActive(false);

        if(doNot.getBeenToThrone())
        {
            this.gameObject.SetActive(true);
            rocks.SetActive(false);
            StartCoroutine(BOSSMIND());
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(doNot.getBeenToThrone())
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
            body.velocity = towards.normalized * 15f;
        }

        if (120 < enemytimer && enemytimer < 121)
        {
            body.velocity = towards.normalized * 40f;  
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

}



