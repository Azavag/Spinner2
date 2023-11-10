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
    [Header("Улучшения")]
    [SerializeField] float movementSpeedUpgrade;
    [SerializeField] float damageUpgrade;
    [SerializeField] float rotationSpeedUpgrade;
    [Header("Кнопки")]
    [SerializeField] Button speedUpgradeButton;
    [SerializeField] Button rotationUpgradeButton;
    [SerializeField] Button damageUpgradeButton;
    [Header("Текстовые счётчики")]
    [SerializeField] TextMeshProUGUI movementSpeedUpgradeText;
    [SerializeField] TextMeshProUGUI rotationSpeedUpgradeText;
    [SerializeField] TextMeshProUGUI enemyDamageUpgradeText;
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

        UpdateUpgradeText(movementSpeedUpgradeText, movementSpeedUpgradeCounter);
        UpdateUpgradeText(rotationSpeedUpgradeText, rotationSpeedUpgradeCounter);
        UpdateUpgradeText(enemyDamageUpgradeText, enemyDamageUpgradeCounter);       
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
            UpdateUpgradeText(movementSpeedUpgradeText);
            return;
        }
        UpdateUpgradeText(movementSpeedUpgradeText, movementSpeedUpgradeCounter);
    }
    public void UpgradeRotationSpeed()
    {
        player.ChangeRotationSpeed(rotationSpeedUpgrade);
        rotationSpeedUpgradeCounter++;
        if (rotationSpeedUpgradeCounter == maxUpgradeCount)
        {
            rotationUpgradeButton.interactable = false;
            UpdateUpgradeText(rotationSpeedUpgradeText);
            return;
        }
        UpdateUpgradeText(rotationSpeedUpgradeText, rotationSpeedUpgradeCounter);
    }
    public void UpgradeDealingDamage()
    {
        player.ChangeEnemyDamage(damageUpgrade);
        enemyDamageUpgradeCounter++;
        CheckOnSwapWeapons(enemyDamageUpgradeCounter);
        if (enemyDamageUpgradeCounter == maxUpgradeCount)
        {
            damageUpgradeButton.interactable = false;
            UpdateUpgradeText(enemyDamageUpgradeText);
            return;
        }
        UpdateUpgradeText(enemyDamageUpgradeText,enemyDamageUpgradeCounter);
    }
    void UpdateUpgradeText(TextMeshProUGUI text, int counter)
    {
        text.text = $"{counter}/{maxUpgradeCount}";
    }
    void UpdateUpgradeText(TextMeshProUGUI text)
    {
        text.text = "макс";
    }




}
