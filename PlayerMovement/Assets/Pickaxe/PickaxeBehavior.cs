using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeBehavior : MonoBehaviour
{


    // Start is called before the first frame update



    public bool canHit;
    private void Start()
    {
        canHit = true;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canHit)
        {
            Debug.Log("Enemy hit!");
            var dm = collision.gameObject.GetComponent<EnemyHealth>();
            dm.Damage(30f);
            canHit = false;

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
