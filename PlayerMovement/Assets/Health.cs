using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currHealth = 0.0f;
    public float maxHealth = 100.0f;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        StartCoroutine(Regenerate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage (float damage) {
        if (currHealth > 0)
        {
            currHealth -= damage;

            healthBar.SetHealth(currHealth);
        }

    }

    public IEnumerator Regenerate()
    {
        while (true)
        {
            if (currHealth < 100)
            {
                currHealth += 1;
                healthBar.SetHealth(currHealth);
                yield return new WaitForSeconds(1);
            } 
            else
            {
                yield return null;
            }
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage(0.5f);
        }
    }
}
