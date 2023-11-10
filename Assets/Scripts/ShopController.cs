using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class ShopController : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] MoneyManager moneyManager;
    [SerializeField] PlayerUpgradeController playerUpgrade;
    [SerializeField] SoundController soundController;
    [Header("Цены")]
    [SerializeField] int[] priceGrades;
    int movementSpeedUpgradePrice, damageUpgradePrice, rotationSpeedUpgradePrice;
    int movementUpgradePriceLevel, damageUpgradePriceLevel, rotationUpgradePriceLevel;
    [Header("Тексты")]
    [SerializeField] TextMeshProUGUI speedPriceText;
    [SerializeField] TextMeshProUGUI rotationPriceText;
    [SerializeField] TextMeshProUGUI damagePriceText;
    void Start()
    {
        ChangePriceLevel(out movementSpeedUpgradePrice, movementUpgradePriceLevel);
        ChangePriceLevel(out rotationSpeedUpgradePrice, rotationUpgradePriceLevel);
        ChangePriceLevel(out damageUpgradePrice, damageUpgradePriceLevel);
        UpdatePriceText(speedPriceText, movementSpeedUpgradePrice);
        UpdatePriceText(rotationPriceText, rotationSpeedUpgradePrice);
        UpdatePriceText(damagePriceText, damageUpgradePrice);
    }

    //По кнопкам
    public void TryBuyMovementSpeedUpgrade() 
    { 
        if(moneyManager.GetMoneyCount() < movementSpeedUpgradePrice)
        {
            FailedBuy();
            return;
        }
        playerUpgrade.UpgradeMovementSpeed();
        moneyManager.UpdateMoneyCount(-movementSpeedUpgradePrice);
        movementUpgradePriceLevel++;
        ChangePriceLevel(out movementSpeedUpgradePrice, movementUpgradePriceLevel);
        UpdatePriceText(speedPriceText, movementSpeedUpgradePrice);
        SuccessBuy();      
    }
    public void TryBuyRotationSpeedUpgrade() 
    {
        if(moneyManager.GetMoneyCount() < rotationSpeedUpgradePrice)
        {
            FailedBuy();
            return;
        }    
        playerUpgrade.UpgradeRotationSpeed();
        moneyManager.UpdateMoneyCount(-rotationSpeedUpgradePrice);
        rotationUpgradePriceLevel++;
        ChangePriceLevel(out rotationSpeedUpgradePrice, rotationUpgradePriceLevel);
        UpdatePriceText(rotationPriceText, rotationSpeedUpgradePrice);
        SuccessBuy();        
    }
    public void TryBuyEnemyDamageUpgrade() 
    {       
        if (moneyManager.GetMoneyCount() < damageUpgradePrice)
        {
            FailedBuy();
            return;
        }
        playerUpgrade.UpgradeDealingDamage();
        moneyManager.UpdateMoneyCount(-damageUpgradePrice);
        damageUpgradePriceLevel++;
        ChangePriceLevel(out damageUpgradePrice, damageUpgradePriceLevel);
        UpdatePriceText(damagePriceText, damageUpgradePrice);
        SuccessBuy();     
    }

    void ChangePriceLevel(out int price,int level)
    {
        price = priceGrades[level];
    }
  
    void UpdatePriceText(TextMeshProUGUI priceText, int price)
    {
        priceText.text = price.ToString();
    }
    void SuccessBuy()
    {
        soundController.Play("SuccesBuy");
    }
    void FailedBuy()
    {
        soundController.Play("FailureBuy");
    }
}
