using System.Collections;
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
    public Animator appearance;
    public GameObject spit;
    public KingHealth kingHealth;
    public Message0 message;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");


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



        body = GetComponent<Rigidbody2D>();
        reftime = Time.time;
        this.gameObject.SetActive(false);
        appearance = GetComponent<Animator>();

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


    void LookThisWay(Vector3 way)
    {
        if(way.y > 0)
        {
            appearance.Play("KingRunUp");
        }
        if(way.y < 0)
        {
            appearance.Play("KingRunDown");
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

        if (kingHealth.currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        doNot.setKingDead(true);
        message.Throne3();
        if(!message.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
        }
    }


    /*
    private void FixedUpdate()
    {
        FloatAndShoot();
    }*/





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









     void TailAndCharge()
    {
        var enemytimer = (Time.time - reftime) * 30f;
        var towards = target.transform.position - transform.position;
        
        LookThisWay(towards);
        if (enemytimer < 90)
        {
            body.velocity = towards.normalized * 6f;
            DamageModifier = 0.2f;
        }

        if (120 < enemytimer && enemytimer < 121)
        {
            body.velocity = towards.normalized * 20f;
            DamageModifier = 2f;
            LookThisWay(body.velocity);
        }

        if (enemytimer > 150)
        {
            reftime = Time.time;
        }

    }


   
    void FirePellets()
    {

        for(int i =0; i < 360f; i += 30)
        {
            var q = Instantiate(spit);
            q.GetComponent<PelletBehavior>().potency = 5f;
            q.transform.position = transform.position;
            q.transform.Translate(new Vector3(0f, 0f, 3f));
            Quaternion z = Quaternion.AngleAxis(i, Vector3.forward);
            q.transform.rotation = z;
            q.GetComponent<Rigidbody2D>().velocity = (z * Vector3.up) * 3f;
            GameObject.Destroy(q, 1.2f);
        }
        
        
    }


    


    private void undo()
    {
        shootlatch = 0f;
    }


    private float shootlatch = 0f;
    void FloatAndShoot()
    {
        var enemytimer = (Time.time - reftime) * 30f;
        var towards = target.transform.position - transform.position;

        appearance.Play("KingJumpFront");




        if (enemytimer < 90)
        {
            Vector3 ooftacular = Vector3.Cross(towards, Vector3.forward);
            body.velocity = ooftacular.normalized * 5f;
        }


        if (shootlatch == 0f)
        {
            FirePellets();
            Invoke("undo", appearance.GetCurrentAnimatorStateInfo(0).length);
            shootlatch = 6f;
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



