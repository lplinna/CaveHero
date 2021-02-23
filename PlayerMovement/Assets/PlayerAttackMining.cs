using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMining : MonoBehaviour
{

    bool swiping = false;
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
        
        if(Input.GetMouseButtonDown(0) && swiping==false)
        {
            // var b = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.DrawLine(b, transform.position, Color.blue);
            swiping = true;
            var pangle = GetPlayerRotation();
            sangle = -20f + pangle;
            eangle = 20f + pangle;
            rotateDirection = 0.2f;
            if(Random.value > 0.5f)
            {
                sangle = 20 + pangle;
                eangle = -20f + pangle;
                rotateDirection = -0.2f;
            }

            pickaxe.SetActive(true);
            pickaxe.GetComponent<PickaxeBehavior>().canHit = true;
            
        }

        if (swiping)
        {
            Quaternion q = Quaternion.AngleAxis(sangle, Vector3.forward);
            Vector3 p = q * Vector3.right;
            Debug.DrawLine(transform.position, transform.position + p, Color.blue);
            pickaxe.transform.position = transform.position + p;
            pickaxe.transform.rotation = q;

            sangle += rotateDirection;
            if (Mathf.Abs(sangle-eangle) < 0.2f)
            {
                swiping = false;
                pickaxe.SetActive(false);
            }


        }


    }
}
