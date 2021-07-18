using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Boot = 0,
        Play = 1,
        Pause = 2,
        Gameover = 3,
        Win = 4
    }

    /// <summary>
    /// The next methods calls the ChangeGameState event to run in another Game State.
    /// </summary>
    public void RestartGame()
    {
        EventManager.ChangeGameState(GameState.Boot);
    }

    public void PlayGame()
    {
        EventManager.ChangeGameState(GameState.Play);
    }

    public void PauseGame()
    {
        EventManager.ChangeGameState(GameState.Pause);
    }

    public void EndGame(bool haveYouWon)
    {
        if(haveYouWon)
        EventManager.ChangeGameState(GameState.Win);
        else
        EventManager.ChangeGameState(GameState.Gameover);
    }

}