using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Boot,
        Ready,
        Play,
        Pause,
        Gameover,
        Win
    }

    public GameState activeState;

    public void SetState(GameState newState)
    {
        activeState = newState;
        EventManager.ChangeGameState(activeState);
    }

    public GameState GetState()
    {
        return activeState;
    }
    

    /// <summary>
    /// The next methods calls the ChangeGameState event to run in another Game State.
    /// </summary>
    public void RestartGame()
    {
        EventManager.ChangeGameState(GameState.Boot);
        activeState = GameState.Boot;
    }

    public void GameReady()
    {
        EventManager.ChangeGameState(GameState.Ready);
        activeState = GameState.Ready;
    }

    public void PlayGame()
    {
        EventManager.ChangeGameState(GameState.Play);
        activeState = GameState.Play;
    }

    public void PauseGame()
    {
        EventManager.ChangeGameState(GameState.Pause);
        activeState = GameState.Pause;
    }

    public void EndGame(bool ImTheWinner)
    {
        if (ImTheWinner)
        {
            EventManager.ChangeGameState(GameState.Win);
            activeState = GameState.Win;
        }
        else
        {
            EventManager.ChangeGameState(GameState.Gameover);
            activeState = GameState.Gameover;
        }
    }

}