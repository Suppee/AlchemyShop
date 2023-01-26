using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // This sets up the game manager to be accessible by other scripts
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is Null!!!");

            return _instance;
        }
    }

    public GameState curState;
    //Variables & References
    public static event Action<GameState> OnGameStateChanged;

    // On Void Awake
    private void Awake()
    {
        // Initialize GameManager Instance
        if (_instance != null)
            Destroy(this);
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        UpdateGameState(curState);
    }
    public void UpdateGameState(GameState newState)
    {
        curState = newState;

        switch(newState)
        {
            case GameState.StartRound:
                
                break;

            case GameState.EndRound:

                break;

            case GameState.MainMenu:
                GoToMainMenu();
                break;

            case GameState.Playing:

                break;

            case GameState.Paused:

                break;

            case GameState.Testing:
                StartRound(GameMode.Coop, "level 0");
                break;
        }
        // Quit Application
        OnGameStateChanged?.Invoke(newState);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartRound(GameMode gamemode, string lvlName)
    {      
        RoundManager.Instance.StartRound(gamemode, lvlName);
    }

    public void GoToPlaying()
    {


    }

    // Pause Game
    public void PauseGame()
    {
        curState = GameState.Paused;
        UIManager.Instance.PauseMenu();
    }

    // Quit Application
    public void EndGame()
    {
        Application.Quit();
    }

}

    public enum GameState { MainMenu,StartRound,Playing,EndRound,Paused,Testing}

