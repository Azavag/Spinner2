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
    int maxUpgradeCount = 50;
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
        moneyManager.UpdateMoneyCount(-movementSpeedUpgradePrice);
        SuccessBuy();      
        playerUpgrade.UpgradeMovementSpeed();
        movementUpgradePriceLevel++;
        if (movementUpgradePriceLevel == maxUpgradeCount)
            return;
        ChangePriceLevel(out movementSpeedUpgradePrice, movementUpgradePriceLevel);
        UpdatePriceText(speedPriceText, movementSpeedUpgradePrice);
    }
    public void TryBuyRotationSpeedUpgrade() 
    {
        if(moneyManager.GetMoneyCount() < rotationSpeedUpgradePrice)
        {
            FailedBuy();
            return;
        }     
        moneyManager.UpdateMoneyCount(-rotationSpeedUpgradePrice);
        SuccessBuy();
        playerUpgrade.UpgradeRotationSpeed();
        rotationUpgradePriceLevel++;
        if (rotationUpgradePriceLevel == maxUpgradeCount)
            return;
        ChangePriceLevel(out rotationSpeedUpgradePrice, rotationUpgradePriceLevel);
        UpdatePriceText(rotationPriceText, rotationSpeedUpgradePrice);
        
    }
    public void TryBuyEnemyDamageUpgrade() 
    {       
        if (moneyManager.GetMoneyCount() < damageUpgradePrice)
        {
            FailedBuy();
            return;
        }
        
        moneyManager.UpdateMoneyCount(-damageUpgradePrice);
        SuccessBuy();
        playerUpgrade.UpgradeDealingDamage();
        damageUpgradePriceLevel++;
        if (damageUpgradePriceLevel == maxUpgradeCount)
            return;
        ChangePriceLevel(out damageUpgradePrice, damageUpgradePriceLevel);
        UpdatePriceText(damagePriceText, damageUpgradePrice);
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
        soundController.Play("FailBuy");
    }
}
