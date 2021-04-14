using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    public float currHealth;
    public GameObject gold, fireElemental, iceElemental, poisonElemental, healthPotion;
    public GameObject stone, amethyst, emerald, ruby, diamond;

    public Sprite closed, broken, smidge, partial, open;
    public SpriteRenderer chest;

    public bool dead;
    public bool alreadyOpened;
    public bool elementalChest;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        currHealth = 1f;
        chest.sprite = closed;
        alreadyOpened = false;
    }

    public void Opened()
    {
        if (!dead)
        {
            if (gameObject.name.Contains("Poison"))
            {
                Vector3 spawnObject;
                float randomY = Random.Range(-0.2500f, 0.2500f);
                float randomX = Random.Range(-0.2500f, 0.2500f);
                spawnObject.x = this.gameObject.transform.position.x - randomX;
                spawnObject.y = this.gameObject.transform.position.y - randomY;
                spawnObject.z = this.gameObject.transform.position.z;
                Instantiate(poisonElemental, spawnObject, Quaternion.identity);
            }
            else if (gameObject.name.Contains("Ice"))
            {
                Vector3 spawnObject;
                float randomY = Random.Range(-0.2500f, 0.2500f);
                float randomX = Random.Range(-0.2500f, 0.2500f);
                spawnObject.x = this.gameObject.transform.position.x - randomX;
                spawnObject.y = this.gameObject.transform.position.y - randomY;
                spawnObject.z = this.gameObject.transform.position.z;
                Instantiate(iceElemental, spawnObject, Quaternion.identity);
            }
            else if (gameObject.name.Contains("Fire"))
            {
                Vector3 spawnObject;
                float randomY = Random.Range(-0.2500f, 0.2500f);
                float randomX = Random.Range(-0.2500f, 0.2500f);
                spawnObject.x = this.gameObject.transform.position.x - randomX;
                spawnObject.y = this.gameObject.transform.position.y - randomY;
                spawnObject.z = this.gameObject.transform.position.z;
                Instantiate(fireElemental, spawnObject, Quaternion.identity);
            }
            else
            {
                System.Random rnd = new System.Random();
                int rewardChance = rnd.Next(1,10);
                if(rewardChance < 3) // 20% chance of Lots of Coin
                {
                    int rewardAmount = rnd.Next(1, 10);
                    if(rewardAmount < 2)
                    {

                    }
                    else if (rewardAmount < 6)
                    {

                    }

                }
                else if (rewardChance < 6) { // 50% chance of Health Potions

                }
                else // Stones Dropped
                {

                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currHealth <= 0)
        {
            StartCoroutine("breakOpen");
        }
    }

    IEnumerator breakOpen()
    {
        if (!alreadyOpened)
        {
            chest.sprite = broken;
            yield return new WaitForSeconds(0.2f);
            chest.sprite = smidge;
            yield return new WaitForSeconds(0.2f);
            chest.sprite = partial;
            yield return new WaitForSeconds(0.2f);
            chest.sprite = open;
            Opened();
            alreadyOpened = true;
        }
        
    }
}
