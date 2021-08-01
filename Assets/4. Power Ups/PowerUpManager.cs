using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : Singleton<PowerUpManager>
{
    IEnumerator activeEarthEffect;
    IEnumerator activeMoonEffect;

    Earth earth;
    MoonManager moon;

    private void Start()
    {
        earth = Earth.Instance;
        moon = MoonManager.Instance;
    }

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
                
                StopAllCoroutines();
                CleanPUfromStage();
                break;
            case GameManager.GameState.Play:

                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.Gameover:
                StopAllCoroutines();
                CleanPUfromStage();
                break;
            case GameManager.GameState.Win:
                StopAllCoroutines();
                CleanPUfromStage();
                break;
        }
    }
    
    List<GameObject> powerUpCollection = new List<GameObject>();

    public void CleanPUfromStage()
    {
        foreach(GameObject x in powerUpCollection)
        {
            Destroy(x);
        }
        powerUpCollection.Clear();
    }

    public void TriggerPowerUpEffect(int id , float howLong)
    {

        switch (id)
        {
            //EARTH Power Ups starting from 100
            case 101:
                earth.EffectsReset();
                activeEarthEffect = earth.Bigger(howLong);
                StartCoroutine(activeEarthEffect); Debug.Log("BIGGER ATTIVO");
                break;
            case 102:
                earth.EffectsReset();
                activeEarthEffect = earth.Smaller(howLong);
                StartCoroutine(activeEarthEffect); Debug.Log("SMALLER ATTIVO");
                break;
            case 103:
                //earth.EffectsReset();
                activeEarthEffect = earth.LowShield(howLong);
                StartCoroutine(activeEarthEffect); Debug.Log("LOWER SHIELD ATTIVO");
                break;
            case 151:
                earth.EffectsReset();
                activeEarthEffect = earth.DoubleBullets(howLong);
                StartCoroutine(activeEarthEffect); Debug.Log("DOUBLE BULLETS ATTIVO");
                break;
            case 152:

                break;
            case 153:

                break;

            //MOON Power Ups starting from 200
            case 201:
                moon.EffectsReset();
                activeMoonEffect = moon.RedMoon(howLong);
                StartCoroutine(activeMoonEffect); Debug.Log("RED MOON ATTIVO");
                break;
            case 202:
                //TODO MOON SCYTHES
                moon.EffectsReset();

                break;
            case 203:
                moon.EffectsReset();
                activeMoonEffect = moon.FullMoon(howLong);
                StartCoroutine(activeMoonEffect); Debug.Log("FULL MOON ATTIVO");
                break;

                //+1hp green power up
            case 301:
                Earth.Instance.AddHp(); Debug.Log("+1 HP ! ");
                UIManager.Instance.UpdateHpText();
                break;
        }
    }

    
}
