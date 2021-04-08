using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireActorBehavior : MonoBehaviour
{
    public GameObject actee;
    private int longevity = 0;

    // Start is called before the first frame update
    void Start()
    {
        longevity = 5000 + (Random.Range(0,3000));
        transform.position = actee.transform.position;
        transform.position += new Vector3(0, 0, -9f);
        transform.parent = actee.transform;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(longevity);
        longevity -= 1;

        if (longevity%80 == 0)
        {
            if(actee.name.Contains("Player")) actee.GetComponent<Health>().Damage(Random.Range(0, 3));
            else actee.GetComponent<EnemyHealth>().Damage(Random.Range(0, 3));
        }

        if (longevity <= 0)
        {
            this.gameObject.GetComponent<ParticleSystem>().Stop();
            Object.Destroy(this);
        }
        
    }
}
