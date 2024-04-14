using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    StartGame,
    InGame,
    GameOver,
    Pause,
    Victory
}

public class GameManager : Singleton<GameManager>
{


    public static event Action<GameState> OnGameStateChanged;
    public GameState CurrentState { get; private set; } = GameState.GameOver;

    private bool _isPause;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
            ChangeGameState(GameState.MainMenu);
        else
            ChangeGameState(GameState.StartGame);

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (arg1.buildIndex == 1)
            ChangeGameState(GameState.StartGame);
    }

    public void ChangeGameState(GameState newState)
    {
        if (newState == CurrentState)
            return;

        CurrentState = newState;
        switch (CurrentState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.01f;
                break;
            case GameState.StartGame:
                InitGame();
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.01f;
                break;
            case GameState.InGame:
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.01f;
                break;
            case GameState.GameOver:
                break;
            case GameState.Pause:
                Time.timeScale = 0.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.01f;
                break;
            case GameState.Victory:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
        Debug.Log("Game State: " + CurrentState.ToString());
    }


    private void InitGame()
    {

    }


    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

}
