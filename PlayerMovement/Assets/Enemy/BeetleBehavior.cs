using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleBehavior : MonoBehaviour
{
    public GameObject target;
    Rigidbody2D body;
    float reftime;
    float orbitway;
    public SpriteRenderer beetleSprite;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        body.velocity = Vector2.zero;
        reftime = Time.time + (Random.value * 4f);
        RandomizeOrbit();
        beetleSprite = GetComponent<SpriteRenderer>();
    }


    void RandomizeOrbit()
    {
        orbitway = Random.value < 0.5 ? -1f : 1f;
    }



    void Update()
    {
        if (body.velocity.x > 0.2f)
        {

            beetleSprite.flipX = true;
        }
        else if (body.velocity.x < -0.2f)
        {
            
            beetleSprite.flipX = false;
        }
    }





    void FixedUpdate()
    {
        var enemytimer = (Time.time - reftime) * 30f;
        var towards = target.transform.position - body.transform.position;
       

        if(enemytimer < 40)  //Orbit player
        {
            Vector3 ooftacular = Vector3.Cross(towards, Vector3.forward);
            body.velocity = Vector3.Lerp(body.velocity, ooftacular.normalized*4f * orbitway, 0.18f);
            if(towards.magnitude > 2f)
            {
                body.velocity = Vector3.Lerp(body.velocity, towards.normalized * 4f * orbitway, 0.2f);
            }
        }

        if(enemytimer > 40 && enemytimer < 60)  // Charge player
        {
            body.velocity = Vector3.Lerp(body.velocity, towards.normalized*8f, 0.18f);
        }


        if(towards.magnitude < 0.25f && Vector3.Dot(body.velocity,towards) > 0.5f) //Bonk, reverse direction after ramming
        {
            body.velocity = -body.velocity * 0.2f;
        }

        if(towards.magnitude < 0.5f && enemytimer > 70) //Move backwards
        {
            body.velocity = -towards.normalized * 2f;
        }

        if(towards.magnitude > 4f)
        {
            body.velocity = Vector3.Lerp(body.velocity, towards.normalized * body.velocity.magnitude, 0.08f);
        }


        if (enemytimer > 120)  //Repeat
        {
            reftime = Time.time;
            RandomizeOrbit();
        }
        
      


        
    }


    

}
