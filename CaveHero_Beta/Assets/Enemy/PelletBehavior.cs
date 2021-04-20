using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletBehavior : MonoBehaviour
{

    public float potency = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

        GameObject.Destroy(this, 12f);
    }

   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Wall"))
        {
            Destroy(this.gameObject, 0.0f);
        }

        if (collision.gameObject.name.Contains("Player"))
        {
            collision.gameObject.GetComponent<Health>().Poison(potency);

            Destroy(this.gameObject, 0.0f);
        }

    }


}
