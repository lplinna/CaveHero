using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireActorBehavior : MonoBehaviour
{
    public GameObject actee;
    private int longevity = 0;
    int panicType = 0;

    // Start is called before the first frame update
    void Start()
    {
        longevity = 20 + Random.Range(10, 30);
        transform.position = actee.transform.position;
        transform.position += new Vector3(0, 0, -9f);
        transform.parent = actee.transform;

        StartCoroutine(OnFire(longevity));
    }





    void doFieryAgony()
    {
        if (panicType==0)
        {
            if (actee.tag == "Spider")
            {
                actee.GetComponent<SpiderBehavior>().onFire = true;
         
            }
            if(actee.tag == "Beetle")
            {
                actee.GetComponent<BeetleBehavior>().onFire = true;
            }

            if (actee.tag == "Bat")
            {
                actee.GetComponent<BatBehavior>().onFire = true;
            }

            panicType = 1;
        }
    }



    void endFieryAgony()
    {
        panicType = 0;

        if (actee.tag == "Spider")
        {
            actee.GetComponent<SpiderBehavior>().onFire = false;

        }
        if (actee.tag == "Beetle")
        {
            actee.GetComponent<BeetleBehavior>().onFire = false;
            actee.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }
        
        if(actee.tag == "Bat")
        {
            actee.GetComponent<BatBehavior>().onFire = false;
            actee.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }


    }

    public IEnumerator OnFire(int l)
    {
        while(true)
        {

            if(longevity <= 0)
            {
                this.gameObject.GetComponent<ParticleSystem>().Stop();
                endFieryAgony();
                Object.Destroy(this);
                break;
            }
            else
            {
                longevity -= 1;
                DoHurt();
                yield return new WaitForSeconds(1f / 7.5f);
            }

        }
    }

    void DoHurt()
    {
        float damage = Random.Range(0, 3) * PlayerModifiers.damageModifier;
        if (actee.name.Contains("King")) actee.GetComponent<EnemyHealth>().Damage(damage * 5f);
        else actee.GetComponent<EnemyHealth>().Damage(damage);
    }

    // Update is called once per frame
    void Update()
    {
        
       
        doFieryAgony();
        
    }
}
