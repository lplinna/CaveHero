using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Wall"))
        {
            Destroy(this.gameObject, 0.2f);
        }

        if (collision.gameObject.name.Contains("Player"))
        {
            collision.gameObject.GetComponent<Health>().Damage(3f);

            Destroy(this.gameObject, 0.02f);
        }


    }


}
