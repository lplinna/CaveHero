using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleBehavior : MonoBehaviour
{
    public GameObject target;
    Rigidbody2D body;
    float reftime;
    float orbitway;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        body.velocity = Vector2.zero;
        reftime = Time.time + (Random.value * 4f);
        RandomizeOrbit();

    }


    void RandomizeOrbit()
    {
        orbitway = Random.value < 0.5 ? -1f : 1f;
    }
    
    void FixedUpdate()
    {
        var enemytimer = (Time.time - reftime) * 30f;
        var towards = target.transform.position - body.transform.position;
       

        if(enemytimer < 20)  //Orbit player
        {
            Vector3 ooftacular = Vector3.Cross(towards, Vector3.forward);
            body.velocity = Vector3.Lerp(body.velocity, ooftacular.normalized*4f * orbitway, 0.05f);
        }

        if(enemytimer > 20 && enemytimer < 40)  // Charge player
        {
            body.velocity = Vector3.Lerp(body.velocity, towards.normalized*2f, 0.08f);
        }


        if(towards.magnitude < 0.5f && Vector3.Dot(body.velocity,towards) > 0.5f) //Bonk, reverse direction after ramming
        {
            body.velocity = -body.velocity * 0.2f;
        }

        if(towards.magnitude < 0.5f && enemytimer > 70) //Move backwards
        {
            body.velocity = -towards.normalized * 2f;
        }


        if (enemytimer > 90)  //Repeat
        {
            reftime = Time.time;
            RandomizeOrbit();
        }
        
      
    }


    

}
