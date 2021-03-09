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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }



    float GetPlayerRotation() 
    {
        return  Quaternion.LookRotation(player.playerwayP,Vector3.forward).eulerAngles.y - 90f;
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
            sangle = -20f + pangle;
            eangle = 20f + pangle;
            rotateDirection = 0.4f;
            if(Random.value > 0.5f)
            {
                sangle = 20 + pangle;
                eangle = -20f + pangle;
                rotateDirection = -0.4f;
            }

            pickaxe.SetActive(true);
            pickaxe.GetComponent<PickaxeBehavior>().canHit = true;
            
        }

        if (swiping == 1) //If it's a normal swing
        {
            Quaternion q = Quaternion.AngleAxis(sangle, Vector3.forward);
            Vector3 p = (q * Vector3.right) * 0.7f;
            Debug.DrawLine(transform.position, transform.position + p, Color.blue);
            pickaxe.transform.position = transform.position + p;
            pickaxe.transform.rotation = q;

            sangle += rotateDirection;
            if (Mathf.Abs(sangle - eangle) < 0.2f)
            {
                swiping = 0;
                pickaxe.SetActive(false);
            }


        }


        if (swiping == 2) //If it's a power attack
        {
            var b = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
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
