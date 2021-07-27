using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PowerUpManager : Singleton<PowerUpManager>
{
    IEnumerator activeEarthEffect;
    IEnumerator activeMoonEffect;

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
                Earth.Instance.StopAnyEffect();
                MoonManager.Instance.StopAnyEffect();
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
                if (activeEarthEffect != null) { StopCoroutine(activeEarthEffect); }
                activeEarthEffect = Earth.Instance.Bigger(howLong);
                StartCoroutine(activeEarthEffect);
                break;
            case 102:
                if (activeEarthEffect != null) { StopCoroutine(activeEarthEffect); }
                activeEarthEffect = Earth.Instance.Smaller(howLong);
                StartCoroutine(activeEarthEffect);
                break;
            case 103:
                if (activeEarthEffect != null) { StopCoroutine(activeEarthEffect); }
                activeEarthEffect = Earth.Instance.LowShield(howLong);
                StartCoroutine(activeEarthEffect);
                break;
            case 151:
                if (activeEarthEffect != null) { StopCoroutine(activeEarthEffect); }
                activeEarthEffect = Earth.Instance.DoubleBullets(howLong);
                StartCoroutine(activeEarthEffect);
                break;
            case 105:


                break;
            case 106:
                break;
            case 107:
                break;
            case 108:
                break;
            case 109:
                break;
            case 110:
                break;
            case 111:
                break;
            case 112:
                break;
                //MOON Power Ups starting from 200
            case 201:
                if (activeMoonEffect != null) { StopCoroutine(activeMoonEffect); }
                activeMoonEffect = MoonManager.Instance.RedMoon(howLong);
                StartCoroutine(activeMoonEffect);
                break;
            case 202:
                //TODO MOON SCYTHES
                break;
            case 203:
                if (activeMoonEffect != null) { StopCoroutine(activeMoonEffect); }
                activeMoonEffect = MoonManager.Instance.FullMoon(howLong);
                StartCoroutine(activeMoonEffect);
                break;
            case 204:
                if (activeMoonEffect != null) { StopCoroutine(activeMoonEffect); }
                activeMoonEffect = MoonManager.Instance.NewMoon(howLong);
                StartCoroutine(activeMoonEffect);
                break;
            case 205:
                break;
            case 206:
                break;
            case 207:
                break;
            case 208:
                break;
            case 209:
                break;
            case 210:
                break;
            case 211:
                break;
            case 212:
                break;

                //+1hp green power up
            case 301:
                Earth.Instance.AddHp();
                UIManager.Instance.UpdateHpText();
                break;
        }
    }

    
}
