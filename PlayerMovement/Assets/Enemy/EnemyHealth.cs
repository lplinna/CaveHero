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

    public void Damage(float amount)
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().AddMoney(3);
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
            else
            {
                audioSrc.Stop();
                audioSrc.Play();
            }
            if (name.Contains("SpiderEnemy"))
            {
                SoundManager.PlaySound("SpiderDeath");
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
