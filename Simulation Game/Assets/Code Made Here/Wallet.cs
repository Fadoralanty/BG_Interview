using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{

    public Action<int> OnCurrentMoneyChanged;
    public int CurrentMoney => currentMoney;
    [SerializeField]private int currentMoney = 0;
    [SerializeField]private int maxMoney=9999;


    private void Start()
    {
        OnCurrentMoneyChanged?.Invoke(currentMoney);
    }

    public void GetMoney(int money)
    {
        if (currentMoney + money >= maxMoney)
        {
            currentMoney = maxMoney;
            OnCurrentMoneyChanged?.Invoke(currentMoney);
        }
        else
        {
            currentMoney += money;
            OnCurrentMoneyChanged?.Invoke(currentMoney);

        }
    }

    public void LoseMoney(int money)
    {
        if (currentMoney - money < 0)
        {
            currentMoney = 0;
            OnCurrentMoneyChanged?.Invoke(currentMoney);

        }
        else
        {
            currentMoney -= money;
            OnCurrentMoneyChanged?.Invoke(currentMoney);
        }
    }
}