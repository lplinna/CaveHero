using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBreaking : MonoBehaviour
{
    public float currHealth;
    public Sprite stoneSprite, breakingSprite, brokenSprite;
    public SpriteRenderer stone;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = 20f;
        stone.sprite = stoneSprite;
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
                this.gameObject.SetActive(false);
            }
        }
        
    }

    public void Damage(float amount)
    {
        if (currHealth > 0)
        {
            currHealth -= amount;
        }
    }
}
