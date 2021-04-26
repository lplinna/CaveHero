using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KingHealth : EnemyHealth
{
    
    public float maxHealth;

    public KingHealthbar healthBar;
    
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 1000.0f;
        currentHealth = maxHealth;

    }


    public override void Damage(float damage) {
        if (currentHealth > 0)
        {
            currentHealth -= damage;

            healthBar.SetHealth(currentHealth);
            Debug.Log(currentHealth);

        }
        else
        {
            currentHealth = 0;
        }
    }










}




   

  
