using AmazingAssets.CurvedWorld.Example;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameState
{
    PrepareGame,
    GameProccess,
    EndGame,
    PauseGame    
}

public class GameController : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] EnemySpawnerController spawner;
    [SerializeField] AnimationController animationController;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerController playerController;
    [SerializeField] UInavigationController navigationController;
    [SerializeField] SoundController soundController;
    
    GameState currentState;
    int currentLevel;
    bool roundResult;
    void Start()
    {
        SetupGame();        
    }

    void SetupGame()
    {
        ChangeGameState(GameState.PrepareGame);
        navigationController.CanvasesSetup();
        navigationController.UpdateLevelNumberText(currentLevel);
    }
    public void StartLevel()
    {
        if(currentState == GameState.PrepareGame) 
        {
            ChangeGameState(GameState.GameProccess);
            navigationController.ChangeStartToIngame();          
            playerMovement.SwitchInput(true);            
            spawner.StartLevel();
        }
        
    } 
    public void EndLevel(bool success)
    {
        roundResult = success;
        if (currentState == GameState.GameProccess)
        {
            ChangeGameState(GameState.EndGame);            
            navigationController.UpdateEndLevelText(roundResult);
            StartCoroutine(EndingLevelCouroutine());           
        }
        if (roundResult)
        {
            currentLevel++;
        }
    }

    IEnumerator EndingLevelCouroutine()
    {
        playerMovement.SwitchInput(false);
        yield return new WaitForSeconds(1.3f);
        if (roundResult)
            soundController.Play("WinRound");
        else soundController.Play("LoseRound");
        navigationController.ChangeIngameToEnd();
        yield return new WaitForSeconds(0.6f);          //Задержка перед полным открытием панели          
        EventManager.InvokeGameReseted();
        playerController.ResetPlayer();       
    }
    //По кнопке
    public void GoToMenu()
    {
        if(currentState == GameState.EndGame)
        {
            ChangeGameState(GameState.PrepareGame);
            navigationController.ChangeEndToMenu();
            navigationController.UpdateLevelNumberText(currentLevel);
        }
      
    }
    //По кнопке
    public void SetPause()
    {
        if (currentState == GameState.GameProccess)
        {
            ChangeGameState(GameState.PauseGame);
            navigationController.ShowPauseCanvas(true);
        }
        Time.timeScale = 0;
    }
    public void StopPause()
    {
        if (currentState == GameState.PauseGame)
        {
            ChangeGameState(GameState.GameProccess);
            navigationController.ShowPauseCanvas(false);
            Time.timeScale = 1;
        }
    }
    public void QuitGame()
    {
        StopPause();
        navigationController.ShowIngameCanvas(false);
        EventManager.InvokePlayerDied();
        EndLevel(false);
    }
    void ChangeGameState(GameState state)
    {
        currentState = state;
    }

    public int GetCurrentLevelNumber()
    {
        return currentLevel;
    }

}
