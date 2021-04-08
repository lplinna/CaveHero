using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Text;

public class MoneyCounter : MonoBehaviour
{
    public static float currMoney = 0;
    private MoneyCounter instance;
    public static GameObject moneyCounter;
    public static TextMeshProUGUI moneyText;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        moneyCounter = this.gameObject;
        moneyText = this.gameObject.GetComponent<TextMeshProUGUI>();
        
    }

    void FixedUpdate()
    {
        UpdateMoney();
    }

    public void UpdateMoney()
    {
        moneyText.text = "$" + currMoney;
    }

    public void SetMoney(float muns)
    {
        moneyText.text = "$" + muns;
    }

    public void AddMoney(int money)
    {
       currMoney += money;
       SetMoney(currMoney);
    }

    public void TakeMoney(float money)
    {
        if (currMoney >= money)
        {
            currMoney -= money;
        }
        SetMoney(currMoney);
    }

    public float getMoney()
    {
        return currMoney;
    }

    public static void Death()
    {
        int randomLoss = (int)Random.Range(0, 10);
        
        if (currMoney >= randomLoss)
        {
            moneyCounter.GetComponent<MoneyCounter>().TakeMoney(randomLoss);
        }
        else
        {
            moneyCounter.GetComponent<MoneyCounter>().TakeMoney(currMoney);
        }

    }

   
}
