using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMining : MonoBehaviour
{

    public int swiping = 0;
    float sangle;
    float eangle;
    float rotateDirection;
    public GameObject pickaxe;
    public GameObject SLAM;
    public PlayerCharacter player;
    bool placedAoE = false;
    public GameObject poisonIndicator, iceIndicator, fireIndicator;
    public static bool poisonAttack, iceAttack, fireAttack;

    public DoNotDestroy doNot;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerCharacter>();
        pickaxe = GameObject.FindGameObjectWithTag("Pickaxe");
        try
        {
            doNot = GameObject.FindGameObjectWithTag("DoNotDestroy").GetComponent<DoNotDestroy>();
        }
        catch
        {
            Invoke("Start", 0.2f);
        }
        poisonAttack = false;
        iceAttack = false;
        fireAttack = false;


        try
        {
            poisonIndicator = GameObject.Find("UI").transform.GetChild(3).gameObject;
            iceIndicator = GameObject.Find("UI").transform.GetChild(4).gameObject;
            fireIndicator = GameObject.Find("UI").transform.GetChild(5).gameObject;
        }
        catch { Invoke("Start", 0.5f); }
    }



    float GetPlayerRotation() 
    {
        return Quaternion.LookRotation(player.playerwayP, Vector3.forward).eulerAngles.y - 90f;
    }



    void BasicSwipe()
    {
        swiping = 1;
        var pangle = GetPlayerRotation();

        if (pangle == 0)
        {
            sangle = 30f;
            eangle = -30f;

        }
        if (pangle == 180)
        {
            sangle = 30f;
            eangle = 230f;

        }

        if (pangle == 90 || pangle == -90)
        {
            sangle = pangle + 2f;
            eangle = pangle - 2f;


        }



        player.isAttackingAnim = true;
        player.DoAttackAnimation();

        pickaxe.GetComponent<SpriteRenderer>().enabled = false;
        pickaxe.SetActive(true);
        pickaxe.GetComponent<PickaxeBehavior>().canHit = true;
    }




    // Update is called once per frame
    void Update()
    {
       if (player.isDialog == false)
        {
            if (Input.GetKeyDown(KeyCode.E) && swiping == 0 && doNot.getPoisonAttack())
            {
                pickaxe.GetComponent<PickaxeBehavior>().elemental = 1;
                poisonAttack = true;

                arrestAll();
                StartCoroutine(poisonIndicatorFlash());
            }

            if (Input.GetKeyDown(KeyCode.F) && swiping == 0 && doNot.getFireAttack())
            {
                pickaxe.GetComponent<PickaxeBehavior>().elemental = 2;
                fireAttack = true;

                arrestAll();
                StartCoroutine(fireIndicatorFlash());
            }

            if (Input.GetKeyDown(KeyCode.R) && swiping == 0 && doNot.getIceAttack())
            {
                pickaxe.GetComponent<PickaxeBehavior>().elemental = 3;
                iceAttack = true;

                arrestAll();
                StartCoroutine(iceIndicatorFlash());
            }




            if (Input.GetMouseButtonDown(0) && swiping == 0)
            {
                BasicSwipe();
                poisonAttack = false;
                iceAttack = false;
                fireAttack = false;
                arrestAll();
            }

            if (swiping == 1) //If it's a normal swing
            {
                var anim = player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);



                var tangle = Mathf.LerpAngle(sangle, eangle, anim.normalizedTime * 2f);
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



                var anim = player.GetComponent<Animator>();
                var mation = anim.GetCurrentAnimatorStateInfo(0);





                if (mation.normalizedTime < 0.2f)
                {

                    anim.speed = 0.2f;
                }
                if (mation.normalizedTime > 0.2f)
                {
                    anim.speed = 1f;
                }

                if (mation.normalizedTime > 0.8f)
                {
                    anim.speed = 0.2f;

                    if (placedAoE == false)
                    {
                        var q = Quaternion.AngleAxis(sangle, Vector3.forward);
                        var p = Instantiate(SLAM);
                        p.transform.position = player.transform.position + (q * Vector3.right);
                        pickaxe.SetActive(false);
                        placedAoE = true;
                    }

                }







                if (!player.isAttackingAnim)
                {
                    swiping = 0;
                    placedAoE = false;
                }

            }



            if (Input.GetMouseButtonDown(1) && swiping == 0)
            {


                swiping = 2;

                sangle = GetPlayerRotation();

                player.isAttackingAnim = true;
                player.DoAttackAnimation();


            }


            if (swiping == 2 && player.currEnergy < 0)
            {
                pickaxe.SetActive(false);
                pickaxe.GetComponent<PickaxeBehavior>().PowerAttack = false;
                swiping = 3;

                player.LeaveAttack();
            }

            if (swiping == 3 && player.currEnergy >= 30)
            {
                swiping = 0;
            }
        }
    }




    IEnumerator poisonIndicatorFlash()
    {
        poisonIndicator.SetActive(true);
        while (poisonAttack)
        {
            poisonIndicator.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(1f);
            poisonIndicator.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(1f);
        }
        yield break;
    }

    IEnumerator iceIndicatorFlash()
    {
        iceIndicator.SetActive(true);
        while (iceAttack)
        {
            iceIndicator.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(1f);
            iceIndicator.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(1f);
        }
        yield break;
    }

    IEnumerator fireIndicatorFlash()
    {
        fireIndicator.SetActive(true);
        while (fireAttack)
        {
            fireIndicator.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(1f);
            fireIndicator.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(1f);
        }
        yield break;
    }

    public void arrestAll()
    {
        fireIndicator.SetActive(false);
        poisonIndicator.SetActive(false);
        iceIndicator.SetActive(false);
    }


 
}
