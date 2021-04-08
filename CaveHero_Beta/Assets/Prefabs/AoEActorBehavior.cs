using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEActorBehavior : MonoBehaviour
{

    public float longevity;
    public float startTime;
    public float hurtTime;
    // Start is called before the first frame update
    void Start()
    {
        longevity = 1f;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        var dulta = Time.time - startTime;
        var puga = Mathf.Lerp(0,2, (longevity-dulta)/longevity);
        transform.localScale = Vector3.one * puga;
        if (puga == 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }




    private void OnTriggerStay2D(Collider2D collision)
    {
        bool isEnemy = collision.gameObject.CompareTag("Bat") || collision.gameObject.CompareTag("Beetle") || collision.gameObject.CompareTag("Spider") || collision.gameObject.CompareTag("ChallengeEnemies");
        if (isEnemy)
        {
            if ((Time.time - hurtTime) > 0.02f)
            {
                collision.gameObject.GetComponent<EnemyHealth>().Damage(4f);
                hurtTime = Time.time;
            }
            

        }
    }

}
