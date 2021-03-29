using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GameObject.Destroy(this, 12f);
    }

    // Update is called once per frame
    void Update()
    {

    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Wall"))
        {
            Destroy(this.gameObject, 0.0f);
        }

        if (collision.gameObject.name.Contains("Player"))
        {
            collision.gameObject.GetComponent<Health>().Poison(20.0f);

            Destroy(this.gameObject, 0.0f);
        }

    }


}
