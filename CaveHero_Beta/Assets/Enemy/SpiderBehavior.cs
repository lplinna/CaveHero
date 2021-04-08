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
    bool readyFire;
    public SpriteRenderer spiderSprite;
    private Animator spiderAnim;
    
    public int spiderType = 0;
    private string precedent = "SpiderAnimation";

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        body.velocity = Vector2.zero;
        reftime = Time.time + (Random.value * 4f);
        RandomizeZigZag();
        readyFire = true;

        spiderAnim = GetComponent<Animator>();

        if (spiderType == 1)
        {
            precedent = "SpiderAnimationIce";
        }

        if (spiderType == 2)
        {
            precedent = "SpiderAnimationLava";
        }
    }


    void Update()
    {

        if (body.velocity.y > 0)
        {
            spiderAnim.Play(precedent + "Up");
            
            if (body.velocity.x > 0.2f)
            {
                spiderSprite.flipX = false;
            }
            else if (body.velocity.x < -0.2f)
            {
               
                spiderSprite.flipX = true;
            }
        }
        else
        {
            spiderAnim.Play(precedent + "Down");
            
            if (body.velocity.x > 0.2f)
            {
                //spiderSprite.sprite = forward;
                spiderSprite.flipX = true;
            }
            else if (body.velocity.x < -0.2f)
            {
                // spiderSprite.sprite = forward;
                spiderSprite.flipX = false;
            }
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
        var smalloffset = direction.normalized * 0.2f;
        var t = Instantiate(pellet, transform.position+smalloffset, Quaternion.identity);
        if (t == null) { return; }
        t.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        
        t.GetComponent<Rigidbody2D>().velocity = direction * 5f;
    }

    void ReadyToShoot()
    {
        readyFire = true;
    }


    void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().isDialog == true)
        {
            var enemytimer = (Time.time - reftime) * 30f;
            var towards = target.transform.position - body.transform.position;


            if (readyFire)
            {
                FirePellet(towards.normalized);
                Invoke("ReadyToShoot", 1.2f);
                readyFire = false;
            }


            if (Mathf.Ceil(enemytimer % 20) == 5)
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

            if (towards.magnitude < 0.2f)
            {
                body.velocity = Vector3.Lerp(body.velocity, -towards.normalized, 0.05f);
            }



            if (enemytimer > 90)  //Repeat
            {
                reftime = Time.time;
            }


        }
    }
        


    

}
