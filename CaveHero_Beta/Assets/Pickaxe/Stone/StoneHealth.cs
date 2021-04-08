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
            
            Vector3 spawnObject;
            for (int i = 0; i < 3; i++)
            {
                float randomY = Random.Range(-0.2500f, 0.2500f);
                float randomX = Random.Range(-0.2500f, 0.2500f);
                spawnObject.x = this.gameObject.transform.position.x - randomX;
                spawnObject.y = this.gameObject.transform.position.y - randomY;
                spawnObject.z = this.gameObject.transform.position.z;
                Instantiate(pickup, spawnObject, Quaternion.identity);
            }
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
        StartCoroutine(stoneMine());
    }

    IEnumerator stoneMine()
    {
        if (currHealth <= 10)
        {
            stone.sprite = breakingSprite;
            yield return new WaitForSeconds(0.2f);
            if (currHealth <= 0)
            {
                stone.sprite = brokenSprite;
                Invoke("Perish", 0.2f);
            }
        }
    }
}
