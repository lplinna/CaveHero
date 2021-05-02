using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeActorBehavior : MonoBehaviour
{
    public GameObject actee;
    private int longevity = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        longevity = 2 + Random.Range(3, 7);
        transform.position = actee.transform.position;
        transform.parent = actee.transform;
        StartCoroutine(PoisonYou(longevity));
    }



    public IEnumerator PoisonYou(int l)
    {
        while (true)
        {
            if (longevity >= 0)
            {
                longevity -= 1;
                DoHurt();
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                actee.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                GameObject.Destroy(this);
                break;
            }
        }
    }


    void DoHurt()
    {
        float damage = 1f * PlayerModifiers.damageModifier;
        if (actee.name.Contains("King")) actee.GetComponent<EnemyHealth>().Damage(damage * 5f);
        else actee.GetComponent<EnemyHealth>().Damage(damage);
    }


    // Update is called once per frame
    void Update()
    {

        var p = actee.GetComponent<SpriteRenderer>();
        p.color = new Color(0.5f, Mathf.Sin(Time.time * 15), 0.5f);
 
    }
}
