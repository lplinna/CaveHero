using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMining : MonoBehaviour
{

    int swiping = 0;
    float sangle;
    float eangle;
    float rotateDirection;
    public GameObject pickaxe;
    public PlayerCharacter player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerCharacter>();
        pickaxe = GameObject.FindGameObjectWithTag("Pickaxe");
    }



    float GetPlayerRotation() 
    {
        return Quaternion.LookRotation(player.playerwayP, Vector3.forward).eulerAngles.y - 90f;
    }




    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0) && swiping==0)
        {
            // var b = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.DrawLine(b, transform.position, Color.blue);
            swiping = 1;
            var pangle = GetPlayerRotation();
            
            if (pangle == 0)
            {
             sangle = 90f;
             eangle = -30f;
             
            }
            if(pangle==180)
            {
             sangle = 90f;
             eangle = 230f;
             
            }

            if(pangle == 90 || pangle == -90)
            {
             sangle = pangle + 20f;
             eangle = pangle - 20f;
             

            }



            player.isAttackingAnim = true;
           player.DoAttackAnimation();

            pickaxe.GetComponent<SpriteRenderer>().enabled = false;
            pickaxe.SetActive(true);
            pickaxe.GetComponent<PickaxeBehavior>().canHit = true;
            
        }

        if (swiping == 1) //If it's a normal swing
        {
            var anim = player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            

            
            var tangle = Mathf.LerpAngle(sangle, eangle, anim.normalizedTime *2f);
            Quaternion q = Quaternion.AngleAxis(tangle, Vector3.forward);
            Vector3 p = (q * Vector3.right) * 0.7f;
            Debug.DrawLine(transform.position, transform.position + p, Color.blue);
            pickaxe.transform.position = transform.position + p;
            pickaxe.transform.rotation = q;


            //sangle += rotateDirection;
            if (!player.isAttackingAnim)
            {
                swiping = 0;
                pickaxe.SetActive(false);
            }


        }


        if (swiping == 2) //If it's a power attack
        {
            Vector3 b = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
            b.Normalize();
            Quaternion q = Quaternion.LookRotation(b, Vector3.forward);

            if(eangle > 0)
            {
                eangle -= rotateDirection;
                q*= Quaternion.AngleAxis(Mathf.Sin(eangle)*60, Vector3.forward);
                player.Drain(0.07f);
            }
            else
            {
                swiping = 0;
                pickaxe.SetActive(false);
                pickaxe.GetComponent<PickaxeBehavior>().PowerAttack = false;
            }

            pickaxe.transform.position = player.transform.position + (q * Vector3.up);
            pickaxe.transform.rotation = q  * Quaternion.AngleAxis(90, Vector3.forward);

        }
       


        if(Input.GetMouseButtonDown(1) && swiping == 0)
        {
            swiping = 2;
            var pangle = GetPlayerRotation();
            eangle = 8f;
            rotateDirection = 0.02f;
            pickaxe.GetComponent<SpriteRenderer>().enabled = true;
            pickaxe.SetActive(true);
            pickaxe.GetComponent<PickaxeBehavior>().PowerAttack = true;
        }


        if(swiping==2 && player.currEnergy < 0)
        {
            pickaxe.SetActive(false);
            pickaxe.GetComponent<PickaxeBehavior>().PowerAttack = false;
            swiping = 0;
        }
        




    }
}
