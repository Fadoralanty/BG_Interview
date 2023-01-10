using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneytext;
    [SerializeField] private Wallet playerWallet;

    private void Start()
    {
        playerWallet.OnCurrentMoneyChanged += OnMoneyChangedListener;
        OnMoneyChangedListener(playerWallet.CurrentMoney);
    }

    private void OnMoneyChangedListener(int money)
    {
        moneytext.text = "Money: " + "<color=yellow>" + "<b>"+ money + "</b>"+ " $" ;
    }

    private void OnDestroy()
    {
        playerWallet.OnCurrentMoneyChanged -= OnMoneyChangedListener;
    }
}
