using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;




    public void Damage(float amount)
    {
        if(currentHealth > 0)
        {
            currentHealth -= amount;
        }
        else
        {
            this.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().AddMoney(3);
        }
    }

   
}
