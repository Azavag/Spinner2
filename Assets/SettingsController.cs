using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public bool isDamageNumberShow;

    private void Start()
    {
        
    }
    //По кнопке тоггла
    public void ToggleDamageNumber()
    {
        isDamageNumberShow = !isDamageNumberShow;
        StaticSettings.isStaticDamageNumberShow = isDamageNumberShow;
    }
}

public static class StaticSettings
{
    static public bool isStaticDamageNumberShow;
    public static bool CanShowDamageNumber()
    {
        return isStaticDamageNumberShow;
    }
}

