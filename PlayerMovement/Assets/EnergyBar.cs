using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energyBar;
    public PlayerCharacter energy;

    // Start is called before the first frame update
    void Start()
    {
        energy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        energyBar = GetComponent<Slider>();
        energyBar.maxValue = energy.maxEnergy;
        energyBar.value = energy.maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetEnergy(float energy)
    {
        energyBar.value = energy;
    }
}
