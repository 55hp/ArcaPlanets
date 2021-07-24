using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingModule : MonoBehaviour
{

    [SerializeField] float startingTime;
    [SerializeField] float rateOfFire;
    [SerializeField] GameObject projectilePref;


    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
        CancelInvoke();
        InvokeRepeating("Shoot", startingTime, rateOfFire);
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
                CancelInvoke();
                break;
            case GameManager.GameState.Win:
                CancelInvoke();
                break;
        }
    }
    
    public void Shoot()
    {
        Instantiate(projectilePref , this.transform.position , Quaternion.identity);
    }
    
}
