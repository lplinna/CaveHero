using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHealth : MonoBehaviour
{
    public float currHealth;
    public void Damage(float amount)
    {
        currHealth -= amount;
    }

    public Sprite stoneSprite, breakingSprite, brokenSprite;
    public SpriteRenderer stone;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = 20f;
        stone.sprite = stoneSprite;
        
        
    }


    void Perish()
    {
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
