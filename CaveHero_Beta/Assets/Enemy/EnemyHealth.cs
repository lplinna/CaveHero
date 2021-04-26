using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public GameObject pickup;
    public Vector3 spawnObject;
    public GameObject gold;
    public AudioSource audioSrc;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public virtual void Damage(float amount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= amount;
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
            else
            {
                audioSrc.Stop();
                audioSrc.Play();
            }
        }
        else
        {
            this.gameObject.SetActive(false);

            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
            else
            {
                audioSrc.Stop();
                audioSrc.Play();
            }

            if (name.Contains("Spider"))
            {
                SoundManager.PlaySound("SpiderDeath");
                int randomAmount = (int)Random.Range(4, 8);
                spawnGold(randomAmount);
            }

            if (name.Contains("Bat"))
            {
                SoundManager.PlaySound("BatDeath");
                int randomAmount = (int)Random.Range(2, 6);
                spawnGold(randomAmount);
            }

            if (name.Contains("Beetle"))
            {
                SoundManager.PlaySound("BeetleDeath");
                int randomAmount = (int)Random.Range(6, 10);
                spawnGold(randomAmount);
            }

            doDrops();
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

    public void spawnGold(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            randomizeXY();
            Instantiate(gold, spawnObject, Quaternion.identity);
        }
    }

    public void doDrops()
    {
        if (Random.value < 0.2f)
        {
            var p2 = Instantiate(pickup);
            p2.transform.position = this.transform.position;
        }
    }


}
