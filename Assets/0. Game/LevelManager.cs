using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
    }
    

    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                break;
            case GameManager.GameState.Play:
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.Gameover:
                break;
            case GameManager.GameState.Win:
                break;
        }
    }



}
