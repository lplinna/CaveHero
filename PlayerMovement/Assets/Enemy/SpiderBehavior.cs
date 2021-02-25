using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehavior : MonoBehaviour
{
    public GameObject target;
    Rigidbody2D body;
    float reftime;
    

    // Start is called before the first frame update
    void Start()
    {
        // DELETE THIS AFTER DEMO
        Destroy(this.gameObject);
        target = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        body.velocity = Vector2.zero;
        reftime = Time.time + (Random.value * 4f);
        RandomizeZigZag();

    }


    void RandomizeZigZag()
    {
        Vector3 oof = Random.insideUnitSphere;
        oof.z = 0f;
        oof.y *= 0.2f;
        body.velocity = oof.normalized * 6f;
    }
    
    void FixedUpdate()
    {
        var enemytimer = (Time.time - reftime) * 30f;
        var towards = target.transform.position - body.transform.position;
        

        
        

        if(Mathf.Ceil(enemytimer%20) == 5)
        {
            RandomizeZigZag();
        }

        if (Mathf.Ceil(enemytimer % 19) == 5)
        {
            body.velocity = Vector3.zero;
        }


        if (towards.magnitude > 3f)
        {
            body.velocity = Vector3.Lerp(body.velocity, towards.normalized, 0.05f);
        }

        if(towards.magnitude < 0.2f)
        {
            body.velocity = Vector3.Lerp(body.velocity, -towards.normalized, 0.05f);
        }

        
        
        if (enemytimer > 90)  //Repeat
        {
            reftime = Time.time;
        }
        
      
    }


    

}
