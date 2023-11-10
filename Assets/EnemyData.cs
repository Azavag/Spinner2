using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyHealthData
{
    public int level;
    public float enemyMaxHealthData;
    public float minibossMaxHealthData;
    public float bossMaxHealthData;
}
[Serializable]
public class EnemyWeaponRotationSpeedData
{
    public int level;
    public float minibossWeaponRotationData;
    public float bossWeaponRotationData;
}
[Serializable]
public class EnemyWeaponDurabilitydData
{
    public int level;
    public float minibossWeaponDurabilityData;
    public float bossWeaponDurabilityData;
}

public class EnemyData : MonoBehaviour
{
    [SerializeField] EnemyHealthData[] healthGrades;
    float enemyMaxHealth;
    float minibossMaxHealth;
    float bossMaxHealth;
    [SerializeField] EnemyWeaponRotationSpeedData[] rotationSpeedGrades;
    float bossWeaponRotation;
    float minibossWeaponRotation;
    [SerializeField] EnemyWeaponDurabilitydData[] weaponDurabilitydGrades;
    float bossWeaponDurability;
    float minibossWeaponDurability;

    void Start()
    {
        //При спавне получать значения отсюда

    }


    public float GetEnemyHealth()
    {
        return enemyMaxHealth;
    }
    public float GetMinibossHealth()
    {
        return minibossMaxHealth;
    }
    public float GetBossHealth()
    {
        return bossMaxHealth;
    }
    public float GetBossRotation()
    {
        return bossWeaponRotation;
    }
    public float GetMinibossRotation()
    {
        return minibossWeaponRotation;
    }
    public float GetBossWeaponDurability()
    {
        return bossWeaponDurability;
    }
    public float GetMinibossWeaponDurability()
    {
        return minibossWeaponDurability;
    }
    public void CheckLevelForHealth(int level)
    {
        foreach (var grade in healthGrades)
        {
            if (level == grade.level)
            {
                enemyMaxHealth = grade.enemyMaxHealthData;
                minibossMaxHealth = grade.minibossMaxHealthData;
                bossMaxHealth = grade.bossMaxHealthData;
                return;
            }
        }
    }
    public void CheckLevelForRotationSpeed(int level)
    {
        foreach (var grade in rotationSpeedGrades)
        {
            if (level == grade.level)
            {
                minibossWeaponRotation = grade.minibossWeaponRotationData;
                bossWeaponRotation = grade.bossWeaponRotationData;
                return;
            }
        }
    }
    public void CheckLevelForDurability(int level)
    {
        foreach (var grade in weaponDurabilitydGrades)
        {
            if (level == grade.level)
            {
                minibossWeaponDurability = grade.minibossWeaponDurabilityData;
                bossWeaponDurability = grade.bossWeaponDurabilityData;
                return;
            }
        }
    }
}
