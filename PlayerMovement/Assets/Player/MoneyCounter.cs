using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Text;

public class MoneyCounter : MonoBehaviour
{
   

   
    public void SetMoney(int muns)
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = "MONEY " + muns;
    }



}
