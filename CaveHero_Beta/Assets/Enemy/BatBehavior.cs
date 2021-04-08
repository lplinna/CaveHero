using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehavior : MonoBehaviour
{
    public GameObject target;
    Rigidbody2D body;
    float reftime;
    bool gotclose;
    public int batType = 0;
    public bool onFire = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        body.velocity = Vector3.zero;
        reftime = Time.time + (Random.value * 4f);
        var batAnim = GetComponent<Animator>();

        if (batType == 2)
        {
            batAnim.Play("BatAnimationLava");
        }
    }


    void FixedUpdate()
    {
        if (onFire)
        {
            bat_on_fire();
        }
        else
        {
            if (!target.GetComponent <PlayerCharacter>().isDialog)
            {
                bat_acting();
            }
            else
            {
                body.velocity = Vector3.zero;
            }
        }




    }


    void bat_acting()
    {
        var enemytimer = (Time.time - reftime) * 30f;
        var towards = target.transform.position - body.transform.position;
        bool close = towards.magnitude < 0.4f;



        if (enemytimer < 20)  //Bat dashes towards player
        {
            body.velocity = Vector3.Lerp(body.velocity, towards.normalized * 4f, 0.1f);

        }



        if (enemytimer > 40 && enemytimer < 90 && (close || gotclose))  //Bat floats upwards
        {
            var pmag = body.velocity.magnitude;
            var kelp = Vector2.Lerp(body.velocity.normalized, Vector2.up, 0.1f);
            body.velocity = kelp.normalized * pmag;
            gotclose = true;
        }


        if (Vector2.Angle(Vector2.up, towards) < 90f)  // A fix for moving the bats upwards if they're too far below the player. Might be unnecessary.
        {
            Vector2 bob = new Vector2(0f, 0.1f);
            body.velocity += bob;
        }

        if (enemytimer > 60)
        {
            Vector2 batway = body.velocity.x * Vector2.right;
            batway = batway.normalized * 2f;
            body.velocity = Vector3.Lerp(body.velocity, batway, 0.02f);
        }


        if (enemytimer > 90)  //Bat repeats
            reftime = Time.time;
        gotclose = false;

    }

    void bat_on_fire()
    {
        transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
        GetComponent<Animator>().speed = 3f;
        var p = Quaternion.AngleAxis(Time.time * 60f, Vector3.forward);
        body.velocity = p * new Vector2(0, 3);

    }

}






