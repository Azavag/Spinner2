using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UInavigationController : MonoBehaviour
{
    [Header("Канвасы")]
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject ingameCanvas;
    [SerializeField] GameObject endLevelCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject shopCanvas;
    [SerializeField] GameObject resourcesCanvas;
    [SerializeField] GameObject settingsCanvas;
    [Header("Тексты")]
    [SerializeField] TextMeshProUGUI levelNumberText;
    [SerializeField] TextMeshProUGUI endLevelText;
    [Header("Ссылка")]
    [SerializeField] AnimationController animationController;

    public void CanvasesSetup()
    {
        ShowEndLevelCanvas(false);
        ShowIngameCanvas(false);
        ShowPauseCanvas(false);
        ShowSettingsCanvas(false);
        ShowStartCanvas(true);
        ShowShopCanvas(true);
        ShowResourcesCanvas(true);
    }
    public void ChangeStartToIngame()
    {
        ShowStartCanvas(false);
        ShowShopCanvas(false);
        ShowIngameCanvas(true);
    }
    public void ChangeIngameToEnd()
    {
        ShowIngameCanvas(false);
        ShowEndLevelCanvas(true);
        animationController.ShowEndLevelPanel();
    }
    public void ChangeEndToMenu()
    {
        animationController.HideEndLevelPanel(CloseEndLevelCanvas);       
    }  
    void CloseEndLevelCanvas()
    {
        ShowStartCanvas(true);
        ShowShopCanvas(true);
        ShowEndLevelCanvas(false);
    }
    //По кнопке
    public void ChangeMenuToSettings()
    {
        ShowSettingsCanvas(true);
        animationController.ShowSettingsPanelPanel();
    }
    public void ChangePauseToSettings()
    {
        ShowSettingsCanvas(true);
        animationController.ShowSettingsPanelPanel();
    }
    public void ChangeSettingsToMenu()
    {
        animationController.HideSettingsPanel(CloseSettingsCanvas);
    }
    void CloseSettingsCanvas()
    {
        ShowSettingsCanvas(false);
    }

    public void UpdateLevelNumberText(int levelNumber)
    {
        levelNumberText.text = (levelNumber + 1).ToString() + " уровень";
    }

    public void UpdateEndLevelText(bool success)
    {
        if (success)
            endLevelText.text = "Победа";
        else endLevelText.text = "Провал";
    }
   
    public void ShowPauseCanvas(bool state)
    {
        pauseCanvas.SetActive(state);
    }
    void ShowShopCanvas(bool state)
    {
        shopCanvas.SetActive(state);
    }
    public void ShowIngameCanvas(bool state)
    {
        ingameCanvas.SetActive(state);
    }
    public void ShowEndLevelCanvas(bool state)
    {
        endLevelCanvas.SetActive(state);
    }
    void ShowResourcesCanvas(bool state)
    {
        resourcesCanvas.SetActive(state);
    }
    void ShowStartCanvas(bool state)
    {
        startCanvas.SetActive(state);
    }
    public void ShowSettingsCanvas(bool state)
    {
        settingsCanvas.SetActive(state);
    }

}
