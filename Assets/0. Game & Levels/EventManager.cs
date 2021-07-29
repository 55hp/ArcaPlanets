using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public static class EventManager 
{
    public delegate void OnStateChangeHandler(GameState newState);
    public static event OnStateChangeHandler OnStateHaveBeenChanged;

    public static void ChangeGameState(GameState newState)
    {
        EventManager.OnStateHaveBeenChanged?.Invoke(newState);
    }

    public delegate void OnShipDamageTakenHandler();
    public static event OnShipDamageTakenHandler OnLifeLost;

    public static void LoseLife()
    {
        EventManager.OnLifeLost?.Invoke();
    }

    public delegate void OnPlanetDamageTakenHandler(float amount);
    public static event OnPlanetDamageTakenHandler OnPlanetTookDamage;

    public static void DealDamageToThePlanet(float amount)
    {
        EventManager.OnPlanetTookDamage?.Invoke(amount);
    }
    

    /* Default methods to implement in observers for the OnStateHaveBeenChanged event
     * 
     * 
     * 
    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
    }
     * 
     * 
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
    *
    *
    */

}
