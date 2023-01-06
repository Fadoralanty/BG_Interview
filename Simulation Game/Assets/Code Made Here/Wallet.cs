using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField]private int currentMoney = 0;
    [SerializeField]private int maxMoney=9999;


    public void GetMoney(int money)
    {
        if (currentMoney + money >= maxMoney)
        {
            currentMoney = maxMoney;
        }
        else
        {
            currentMoney += money;
        }
    }

    public void LoseMoney(int money)
    {
        if (currentMoney - money < 0) currentMoney = 0;
        else
        {
            currentMoney -= money;
        }
    }
}