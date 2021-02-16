using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = health.maxHealth;
        healthBar.value = health.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(float hp)
    {
        healthBar.value = hp;
    }
}
