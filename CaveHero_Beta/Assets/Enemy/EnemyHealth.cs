using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public GameObject pickup;

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
                int randomGain = (int)Random.Range(4, 8);
                GameObject.FindGameObjectWithTag("Money").GetComponent<MoneyCounter>().AddMoney(randomGain);
            }

            if (name.Contains("Bat"))
            {
                SoundManager.PlaySound("BatDeath");
                int randomGain = (int)Random.Range(2, 6);
                GameObject.FindGameObjectWithTag("Money").GetComponent<MoneyCounter>().AddMoney(randomGain);
            }

            if (name.Contains("Beetle"))
            {
                SoundManager.PlaySound("BeetleDeath");
                int randomGain = (int)Random.Range(6, 10);
                GameObject.FindGameObjectWithTag("Money").GetComponent<MoneyCounter>().AddMoney(randomGain);
            }

            doDrops();
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
