using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehavior : MonoBehaviour
{
    public GameObject target;
    public GameObject pellet;
    Rigidbody2D body;
    float reftime;
    Vector2 zagway;
    public SpriteRenderer spiderSprite;
    public Sprite forward, backward;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        body.velocity = Vector2.zero;
        reftime = Time.time + (Random.value * 4f);
        RandomizeZigZag();

    }


    void Update()
    {
        if (body.velocity.x > 0.2f)
        {
            spiderSprite.sprite = forward;
            spiderSprite.flipX = true;
        }
        else if (body.velocity.x < -0.2f)
        {
            spiderSprite.sprite = forward;
            spiderSprite.flipX = false;
        }
    }


    void RandomizeZigZag()
    {
        Vector3 oof = Random.insideUnitSphere;
        oof.z = 0f;
        oof.y *= 0.2f;
        body.velocity = oof.normalized * 6f;
        zagway = body.velocity;
    }


    void FirePellet(Vector3 direction)
    {
        var t = Instantiate(pellet, transform.position, Quaternion.identity);
        if (t == null) { return; }
        t.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        
        t.GetComponent<Rigidbody2D>().velocity = direction * 5f;
    }


    void FixedUpdate()
    {
        var enemytimer = (Time.time - reftime) * 30f;
        var towards = target.transform.position - body.transform.position;

        
        if ((25%enemytimer)<2f)
        {
            FirePellet(towards.normalized);
        }

        
        



        if(Mathf.Ceil(enemytimer%20) == 5)
        {
            RandomizeZigZag();
            
        }

        if (Mathf.Ceil(enemytimer % 19) == 5)
        {
            body.velocity = Vector3.zero;
        }


        if (towards.magnitude > 5f)
        {
            body.velocity = Vector3.Lerp(towards.normalized * 3f, zagway, 0.2f);
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
