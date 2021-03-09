using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public GameObject pickup;



    public void Damage(float amount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= amount;
        }
        else
        {
            this.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().AddMoney(3);
            doDrops();
        }
    }


    public void doDrops()
    {
        if (Random.value < 0.2f)
        {
            var p2 = Instantiate(pickup);
            p2.transform.position = this.transform.position;
        }
    }


}
