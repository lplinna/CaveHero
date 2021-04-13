using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMoney : MonoBehaviour
{
    public MoneyCounter moneyCounter;
    public TextMeshProUGUI money;
    // Update is called once per frame
    void Update()
    {
        money.text = "$" + moneyCounter.getMoney();
    }
}
