using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingHealthbar : MonoBehaviour
{
    public Slider healthBar;
    public Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.Find("FightKing").GetComponent<Health>();
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

    public void SetColor(Color p)
    {
        Image barbox = healthBar.fillRect.gameObject.GetComponent<Image>();
        barbox.color = p;
    }

    public Color GetColor()
    {
        Image barbox = healthBar.fillRect.gameObject.GetComponent<Image>();
        return barbox.color;
    }
}
