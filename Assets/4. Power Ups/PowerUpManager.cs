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
                StopAllCoroutines();
                CleanActivePowerUps();
                break;
            case GameManager.GameState.Play:
                isGenerating = true;
                StartCoroutine(GenRandomPowerUp(10));
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.Gameover:
                StopAllCoroutines();
                CleanActivePowerUps();
                break;
            case GameManager.GameState.Win:
                StopAllCoroutines();
                CleanActivePowerUps();
                break;
        }
    }

    [SerializeField] GameObject[] PowerUpPrefabs;
    [SerializeField] GameObject powerUpPathGameObject;
    PathCreator path;
    GameObject instantiatedPowerUp;
    List<GameObject> powerUpCollection = new List<GameObject>();
    public bool isGenerating;

    public void Start()
    {
        path = powerUpPathGameObject.GetComponent<PathCreator>();
    }


    public IEnumerator GenRandomPowerUp(float rate)
    {
        Debug.Log("Gen Random Power Up : ON");
        while (isGenerating)
        {
            yield return new WaitForSeconds(rate);
            instantiatedPowerUp = Instantiate(PowerUpPrefabs[Random.Range(0, PowerUpPrefabs.Length)]);
            powerUpCollection.Add(instantiatedPowerUp);
            instantiatedPowerUp.GetComponent<PowerUp>().SetPathCreator(path);
        }
    }

    public void CleanActivePowerUps()
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
            case 104:


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
                break;
            case 203:
                break;
            case 204:
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
        }
    }
}
