using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    public float currHealth;
    public GameObject healthPotion, stone, amethyst, emerald, ruby, diamond;

    public Sprite closed, broken, smidge, partial, open;
    public SpriteRenderer chest;

    public bool dead;
    public bool alreadyOpened;
    public bool elementalChest;

    public MoneyCounter money;
    public DoNotDestroy doNot;
    public Message0 message;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        currHealth = 1f;
        chest.sprite = closed;
        alreadyOpened = false;
    }

    public void Damage(float amount)
    {
        currHealth -= amount;
    }

    public void Opened()
    {
        if (!dead)
        {
            if (gameObject.name.Contains("Poison"))
            {
                doNot.setPoisonAttack(true);
                message.PoisonAttackGained();
            }
            else if (gameObject.name.Contains("Ice"))
            {
                doNot.setIceAttack(true);
                message.IceAttackGained();
            }
            else if (gameObject.name.Contains("Fire"))
            {
                doNot.setFireAttack(true);
                message.FireAttackGained();
            }
            else
            {
                int rewardChance = Random.Range(0,10);
                if(rewardChance < 3) // 20% chance of Lots of Coin
                {
                    int rewardAmount = Random.Range(0, 10);
                    if (rewardAmount < 2) // 10% of 100 gold
                    {
                        money.AddMoney(100);
                    }
                    else if (rewardAmount < 6) // 50% chance of 50 gold
                    {
                        money.AddMoney(50);
                    }
                    else // 25 gold
                    {
                        money.AddMoney(25);
                    }

                }
                else if (rewardChance < 6) { // 50% chance of Health Potions
                    int rewardAmount = Random.Range(0, 10);
                    if (rewardAmount < 6) // 50% of 2 health potions
                    {
                        Vector3 spawnObject;
                        for (int i = 0; i < 2; i++)
                        {
                            float randomY = Random.Range(0f, 0.5f);
                            float randomX = Random.Range(0f, 0.5f);
                            spawnObject.x = this.gameObject.transform.position.x - randomX;
                            spawnObject.y = this.gameObject.transform.position.y - randomY;
                            spawnObject.z = this.gameObject.transform.position.z;
                            Instantiate(healthPotion, spawnObject, Quaternion.identity);
                        }
                    }
                    else // 1 health potion
                    {
                        Vector3 spawnObject;
                        float randomY = Random.Range(0f, 0.5f);
                        float randomX = Random.Range(0f, 0.5f);
                        spawnObject.x = this.gameObject.transform.position.x - randomX;
                        spawnObject.y = this.gameObject.transform.position.y - randomY;
                        spawnObject.z = this.gameObject.transform.position.z;
                        Instantiate(healthPotion, spawnObject, Quaternion.identity);
                    }
                }
                else // Stones Dropped
                {
                    int rewardAmount = Random.Range(0, 10);
                    Vector3 spawnObject;
                    if (rewardAmount < 2) // 2 Diamond
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            float randomY = Random.Range(0f, 0.5f);
                            float randomX = Random.Range(0f, 0.5f);
                            spawnObject.x = this.gameObject.transform.position.x - randomX;
                            spawnObject.y = this.gameObject.transform.position.y - randomY;
                            spawnObject.z = this.gameObject.transform.position.z;
                            Instantiate(diamond, spawnObject, Quaternion.identity);
                        }
                    }
                    else if (rewardAmount < 4) // 4 Emerald
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            float randomY = Random.Range(0f, 0.5f);
                            float randomX = Random.Range(0f, 0.5f);
                            spawnObject.x = this.gameObject.transform.position.x - randomX;
                            spawnObject.y = this.gameObject.transform.position.y - randomY;
                            spawnObject.z = this.gameObject.transform.position.z;
                            Instantiate(emerald, spawnObject, Quaternion.identity);
                        }
                    }
                    else if (rewardAmount < 6) // 6 Ruby
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            float randomY = Random.Range(0f, 0.5f);
                            float randomX = Random.Range(0f, 0.5f);
                            spawnObject.x = this.gameObject.transform.position.x - randomX;
                            spawnObject.y = this.gameObject.transform.position.y - randomY;
                            spawnObject.z = this.gameObject.transform.position.z;
                            Instantiate(ruby, spawnObject, Quaternion.identity);
                        }
                    }
                    else if (rewardAmount < 8) // 8 Amethyst
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            float randomY = Random.Range(0f, 0.5f);
                            float randomX = Random.Range(0f, 0.5f);
                            spawnObject.x = this.gameObject.transform.position.x - randomX;
                            spawnObject.y = this.gameObject.transform.position.y - randomY;
                            spawnObject.z = this.gameObject.transform.position.z;
                            Instantiate(amethyst, spawnObject, Quaternion.identity);
                        }
                    }
                    else // 10 Stone
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            float randomY = Random.Range(0f, 0.5f);
                            float randomX = Random.Range(0f, 0.5f);
                            spawnObject.x = this.gameObject.transform.position.x - randomX;
                            spawnObject.y = this.gameObject.transform.position.y - randomY;
                            spawnObject.z = this.gameObject.transform.position.z;
                            Instantiate(stone, spawnObject, Quaternion.identity);
                        }
                    }
                }
            }
            dead = true;
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
