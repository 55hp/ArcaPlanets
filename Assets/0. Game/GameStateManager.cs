using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : Singleton<GameStateManager>
{
    public delegate void OnStateChange();
    public event OnStateChange OnStateHaveBennChanged;


    public enum GameState
    {
        Boot,
        Play,
        Pause,
        Gameover,
        Win
    }

    public GameState myState;

    private void Awake()
    {
        myState = GameState.Boot;
    }


    public void ChangeState(GameState newState)
    {
        myState = newState;
        if (OnStateHaveBennChanged != null)
        {
            OnStateHaveBennChanged();
        }

        if(newState == GameState.Play)
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
        return myState;
    }

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

}
