using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// Game Manager:
    /// Init the game using the Game State Manager class changing for the first time the State from null to Boot
    /// </summary>
    private void Start()
    {
        activeState = GameState.Boot;
        ChangeState(GameState.Boot);
    }


    public delegate void OnStateChange();
    public event OnStateChange OnStateHaveBeenChanged;
    public GameState activeState;

    public enum GameState
    {
        Boot,
        Play,
        Pause,
        Gameover,
        Win
    }

    /// <summary>
    /// This is the main method that gives to all other classes the possibility to change the actual state.
    /// Then it triggers the event.
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(GameState newState)
    {
        activeState = newState;

        if (OnStateHaveBeenChanged != null)
        {
            OnStateHaveBeenChanged();
        }

        if (newState == GameState.Play)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public GameState GetState()
    {
        return activeState;
    }


    /// <summary>
    /// The next methods gives the possibility to change the Game State from the editor ( Buttons ).
    /// </summary>
    public void RestartGame()
    {
        ChangeState(GameState.Boot);
    }

    public void PlayGame()
    {
        ChangeState(GameState.Play);
    }

    public void PauseGame()
    {
        ChangeState(GameState.Pause);
    }

    public void ResumeGame()
    {
        ChangeState(GameState.Play);
    }
}