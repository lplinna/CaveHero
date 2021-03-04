using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeBehavior : MonoBehaviour
{


    // Start is called before the first frame update



    public bool canHit;
    public bool PowerAttack;
    private void Start()
    {
        canHit = true;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {

        Debug.Log(collision.collider.gameObject.name);
        bool isEnemy = collision.gameObject.CompareTag("Bat") || collision.gameObject.CompareTag("Beetle") || collision.gameObject.CompareTag("Spider") || collision.gameObject.CompareTag("ChallengeEnemies");
        if (isEnemy)
        {
            Debug.Log("Enemy hit!");
            var dm = collision.gameObject.GetComponent<EnemyHealth>();
            if (canHit)
            {
                dm.Damage(30f);
                canHit = false;
            }

            if (PowerAttack)
            {
                dm.Damage(5f);
            }

        }

        if (collision.gameObject.CompareTag("Stone") && canHit)
        {
            Debug.Log("Stone hit!");
            var stone = collision.gameObject.GetComponent<StoneHealth>();
            stone.Damage(10f);
            canHit = false;
        }
    }
}
