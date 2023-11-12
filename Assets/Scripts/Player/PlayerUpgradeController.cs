using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerWeaponGrades
{
    public int level;
    public TextMeshProUGUI weaponNameText;
    public GameObject weaponModel;
}


public class PlayerUpgradeController : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] PlayerController player;
    [SerializeField] PlayerWeaponController weaponController;
    [SerializeField] PlayerCanvasController canvasController;
    [SerializeField] SoundController soundController;
    [Header("Улучшения")]
    [SerializeField] float movementSpeedUpgrade;
    [SerializeField] float damageUpgrade;
    [SerializeField] float rotationSpeedUpgrade;
    [Header("Кнопки")]
    [SerializeField] Button speedUpgradeButton;
    [SerializeField] Button rotationUpgradeButton;
    [SerializeField] Button damageUpgradeButton;
    [Header("Текстовые счётчики")]
    [SerializeField] TextMeshProUGUI movementSpeedPriceText;
    [SerializeField] TextMeshProUGUI rotationSpeedPriceText;
    [SerializeField] TextMeshProUGUI damagePriceText;
    int movementSpeedUpgradeCounter;
    int rotationSpeedUpgradeCounter;
    int enemyDamageUpgradeCounter;
    int maxUpgradeCount = 50;
    [Header("Оружия")]
    [SerializeField] PlayerWeaponGrades[] weaponGrades;
    [SerializeField] TextMeshProUGUI[] upgradesTexts;
    int weaponModelCounter = 0;
    GameObject currentWeaponModel;
    void Start()
    {
        foreach (var text in upgradesTexts)
        {
            canvasController.ClearColor(text);
        }
        foreach (var grade in weaponGrades)
        {
            grade.weaponModel.SetActive(false);
        }
        WearWeapon(weaponModelCounter);     
    }

    void WearWeapon(int upgradeLevel)
    {          
        currentWeaponModel = weaponGrades[upgradeLevel].weaponModel;
        currentWeaponModel.SetActive(true);
    }
    void CheckOnSwapWeapons(int upgradeLevel)
    {    
        foreach(var grade in weaponGrades)
        {
            if (upgradeLevel == grade.level)
            {
                weaponModelCounter++;
                currentWeaponModel.SetActive(false);
                canvasController.ShowWeaponUpgradeText(grade.weaponNameText);
                if(grade.weaponNameText.text != "")
                    soundController.Play("WeaponUpgrade");
                WearWeapon(weaponModelCounter);
                return;
            }
        }
    }

    public void UpgradeMovementSpeed()
    {
        player.ChangeMovementSpeed(movementSpeedUpgrade);
        movementSpeedUpgradeCounter++;
        if(movementSpeedUpgradeCounter == maxUpgradeCount)
        {
            speedUpgradeButton.interactable = false;
            UpdateText(movementSpeedPriceText);
            return;
        }

    }
    public void UpgradeRotationSpeed()
    {
        player.ChangeRotationSpeed(rotationSpeedUpgrade);
        rotationSpeedUpgradeCounter++;
        if (rotationSpeedUpgradeCounter == maxUpgradeCount)
        {
            rotationUpgradeButton.interactable = false;
            UpdateText(rotationSpeedPriceText);
            return;
        }
    }
    public void UpgradeDealingDamage()
    {
        player.ChangeEnemyDamage(damageUpgrade);
        enemyDamageUpgradeCounter++;
        CheckOnSwapWeapons(enemyDamageUpgradeCounter);
        if (enemyDamageUpgradeCounter == maxUpgradeCount)
        {
            damageUpgradeButton.interactable = false;
            UpdateText(damagePriceText);
            return;
        }
    }

    void UpdateText(TextMeshProUGUI text)
    {
        text.text = "макс";
    }
}
