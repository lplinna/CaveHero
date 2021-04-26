using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    public float currHealth;
    public GameObject healthPotion, stone, amethyst, emerald, ruby, diamond, sapphire, gold;

    public Sprite closed, broken, smidge, partial, open;
    public SpriteRenderer chest;

    public bool dead;
    public bool alreadyOpened;
    public bool elementalChest;

    public DoNotDestroy doNot;
    public Message0 message;
    public Vector3 spawnObject;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        currHealth = 1f;
        chest.sprite = closed;
        alreadyOpened = false;


        if (!PlayerModifiers.hasNot)
        {
            Debug.Log(this.gameObject.name + " awaiting doNot");
            Invoke("Start", 0.001f);
            return;
        }
        else
        {
            doNot = PlayerModifiers.doNot;
        }



        if (doNot.getPoisonAttack() && this.gameObject.name.Contains("Poison"))
        {
            this.gameObject.SetActive(false);
        }

        if (doNot.getFireAttack() && this.gameObject.name.Contains("Fire"))
        {
            this.gameObject.SetActive(false);
        }

        if (doNot.getIceAttack() && this.gameObject.name.Contains("Ice"))
        {
            this.gameObject.SetActive(false);
        }

        if (doNot.getSapphire() && this.gameObject.name.Contains("Ending"))
        {
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {

    }

    public void Damage(float amount)
    {
        currHealth -= amount;
        StartCoroutine("breakOpen");
    }

    public void Opened()
    {
        //doNot = GameObject.Find("Player").GetComponent<PlayerCharacter>().doNot;
        if (!dead)
        {
            randomizeXY();
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
            else if (gameObject.name.Contains("Ending"))
            {
                randomizeXY();
                Instantiate(sapphire, spawnObject, Quaternion.identity);
                doNot.setSapphire(true);
                message.Sapphire();
            }
            else
            {
                float rewardChance = Random.value;
                if (rewardChance <= 0.2) // 20% chance of Lots of Coin
                {
                    float rewardAmount = Random.value;
                    if (rewardAmount <= 0.1) // 10% of 100 gold
                    {
                        for(int i = 0; i < 100; i++)
                        {
                            randomizeXY();
                            Instantiate(gold, spawnObject, Quaternion.identity);
                        }
                    }
                    else if (rewardAmount <= 0.5) // 50% chance of 50 gold
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            randomizeXY();
                            Instantiate(gold, spawnObject, Quaternion.identity);
                        }
                    }
                    else // 25 gold
                    {
                        for (int i = 0; i < 25; i++)
                        {
                            randomizeXY();
                            Instantiate(gold, spawnObject, Quaternion.identity);
                        }
                    }

                }
                else if (rewardChance <= 0.5)
                { // 50% chance of Health Potions
                    float rewardAmount = Random.value;
                    if (rewardAmount <= 5) // 50% of 2 health potions
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            randomizeXY();
                            Instantiate(healthPotion, spawnObject, Quaternion.identity);
                        }
                    }
                    else // 1 health potion
                    {
                        Instantiate(healthPotion, spawnObject, Quaternion.identity);
                    }
                }
                else // Stones Dropped
                {
                    float rewardAmount = Random.value;
                    if (rewardAmount <= 0.1) // 10% of 2 Diamond
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            randomizeXY();
                            Instantiate(diamond, spawnObject, Quaternion.identity);
                        }
                    }
                    else if (rewardAmount <= 0.25) // 25% of 4 Emerald
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            randomizeXY();
                            Instantiate(emerald, spawnObject, Quaternion.identity);
                        }
                    }
                    else if (rewardAmount <= 0.35) // 35% of 6 Ruby
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            randomizeXY();
                            Instantiate(ruby, spawnObject, Quaternion.identity);
                        }
                    }
                    else if (rewardAmount <= 0.6) // 60% of 8 Amethyst
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            randomizeXY();
                            Instantiate(amethyst, spawnObject, Quaternion.identity);
                        }
                    }
                    else // 10 Stone
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            randomizeXY();
                            Instantiate(stone, spawnObject, Quaternion.identity);
                        }
                    }
                }
            }
            dead = true;
        }
    }


    IEnumerator breakOpen()
    {
        if (!alreadyOpened)
        {
            chest.sprite = broken;
            SoundManager.PlaySound("Chest");
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

    public void randomizeXY()
    {
        float randomY = Random.Range(0f, 0.75f);
        float randomX = Random.Range(-0.50f, 0.50f);
        spawnObject.x = this.gameObject.transform.position.x - randomX;
        spawnObject.y = this.gameObject.transform.position.y - randomY;
        spawnObject.z = this.gameObject.transform.position.z;
    }
}
