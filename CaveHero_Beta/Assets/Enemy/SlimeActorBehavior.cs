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
        longevity = 500 + (Random.Range(0,3000));
        transform.position = actee.transform.position;
        transform.parent = actee.transform;

    }








    // Update is called once per frame
    void Update()
    {
        Debug.Log(longevity);
        longevity -= 1;



        var p = actee.GetComponent<SpriteRenderer>();
        

        if (longevity%80 == 0)
        {
            
            if (actee.name.Contains("Player")) actee.GetComponent<Health>().Damage(1f);
            else actee.GetComponent<EnemyHealth>().Damage(1f);
        }
        p.color = new Color(0.5f, Mathf.Sin(Time.time * 15), 0.5f);
        
        
        if (longevity <= 0)
        {
            p.color = new Color(1f, 1f, 1f);
            Object.Destroy(this);
        }
        
    }
}
