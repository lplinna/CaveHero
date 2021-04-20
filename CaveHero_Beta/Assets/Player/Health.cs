using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float currHealth = 0.0f;
    public float maxHealth;

    public static HealthBar healthBar;

    public CheckpointSystem checkpoint;

    int poisonCounter = 0;
    bool hurting = false;

    void Awake()
    {
        //maxHealth = 100.0f * PlayerModifiers.healthModifier;
    }
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        StartCoroutine(Regenerate());
        try
        {
            healthBar = GameObject.FindObjectOfType<HealthBar>();
        }
        catch
        {
            Invoke("Start", 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerModifiers.healthModifier);
        //Debug.Log(currHealth + " / " + maxHealth);
        if (maxHealth < maxHealth * PlayerModifiers.healthModifier)
        {
            maxHealth = 100.0f * PlayerModifiers.healthModifier;
            currHealth = maxHealth;
            Debug.Log("oof");
        }
    }



    public void Damage (float damage) {
        if (currHealth > 0)
        {
            currHealth -= damage;

            healthBar.SetHealth(currHealth);

            DoHurting();

        }
        if (currHealth <= 0)
        {
            this.gameObject.SetActive(false);
            SoundManager.PlaySound("PlayerDeath");
            Invoke("Death", 3f);
        }
    }


    public void Poison(float hits)
    {
        if (poisonCounter<3)
        {
            StartCoroutine(PoisonRoutine(hits));
            poisonCounter++;
        }
    }




    public void DoHurting()
    {
        if (hurting == false)
        {
            StartCoroutine(OuchieFlashRoutine(300f));
            hurting = true;
        }
    }


    public IEnumerator OuchieFlashRoutine(float limit)
    {
        var p = GetComponent<SpriteRenderer>();
        while (true)
        {
            if (limit >= 0)
            { 
                p.color = new Color(Mathf.Sin(Time.time*35f), 0f, 0f);
                yield return null;
                limit--;
            }
            else
            {
                p.color = new Color(1f, 1f, 1f);
                hurting = false;
                yield break;
            }
        }
    }



    public void Death()
    {
        MoneyCounter.Death();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator Regenerate()
    {
        while (true)
        {
            if (SceneManager.GetActiveScene().name == "Merchant")
            {
                if (currHealth < 100 * PlayerModifiers.healthModifier)
                {
                    currHealth += 10;
                    healthBar.SetHealth(currHealth);
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                if (currHealth < 100 * PlayerModifiers.healthModifier)
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
    }


    public Color sickened(Color c)
    {
        c = Color.Lerp(c, Color.green, 0.4f);
        return c;
    }

    public IEnumerator PoisonRoutine(float hits,float damage=1.0f,float speed=0.2f)
    {
        healthBar.SetColor(sickened(healthBar.GetColor()));
        int hcount = 0;
        while (true)
        {
            if (currHealth > 0 && hcount <= hits)
            {
                Damage(damage);
                hcount++;
                yield return new WaitForSeconds(speed);
            }
            else
            {
                poisonCounter -= 1;
                healthBar.SetColor(Color.red);
                yield break;
            }
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        bool isEnemy = collision.gameObject.CompareTag("Bat") || collision.gameObject.CompareTag("Beetle") || collision.gameObject.CompareTag("Spider") || collision.gameObject.CompareTag("ChallengeEnemies");
        if (isEnemy)
        {
            Damage(0.5f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealthPickUp"))
        {
            if (currHealth < 100 * PlayerModifiers.healthModifier)
            {
                SoundManager.PlaySound("HealthPotion");
                currHealth += 20;
                collision.gameObject.SetActive(false);
            }
        }
    }
}
