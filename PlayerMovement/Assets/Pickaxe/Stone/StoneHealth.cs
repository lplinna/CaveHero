using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHealth : MonoBehaviour
{
    public float currHealth;
    public GameObject pickup;
    public void Damage(float amount)
    {
        currHealth -= amount;
    }

    public Sprite stoneSprite, breakingSprite, brokenSprite;
    public SpriteRenderer stone;

    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        currHealth = 20f;
        stone.sprite = stoneSprite;
    }


    void Perish()
    {
        if (!dead)
        {
            Instantiate(pickup, this.gameObject.transform.position, Quaternion.identity);
            float randomExtra = Random.Range(0.000f, 1.000f);
            if (randomExtra <= 0.05)
            {
                Instantiate(pickup, this.gameObject.transform.position, Quaternion.identity);
            }
            Debug.Log("Pickup");
            dead = true;
        }
        
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currHealth <= 10)
        {
            stone.sprite = breakingSprite;
            if (currHealth <= 0)
            {
                stone.sprite = brokenSprite;
                Invoke("Perish", 0.2f);
            }
        }
    }
}
