using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : Singleton<PowerUpManager>
{
    PowerUpDefinition activeEarthEffect;
    PowerUpDefinition activeMoonEffect;

    Earth earth;
    MoonManager moonManager;

    private void Start()
    {
        earth = Earth.Instance;
        moonManager = MoonManager.Instance;
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
                Debug.Log("BIGGER ATTIVO"); 
                ActivateEffectForTheEarth(new Earth.BiggerPowerUp(earth) { Duration = howLong });
                break;
            case 102:
                Debug.Log("SMALLER ATTIVO");
                ActivateEffectForTheEarth(new Earth.SmallerPowerUp(earth) { Duration = howLong }); 
                break;
            case 103:
                Debug.Log("LOWER SHIELD ATTIVO");
                ActivateEffectForTheEarth(new Earth.LowShieldPowerUp(earth) { Duration = howLong });
                break;
            case 151:
                Debug.Log("DOUBLE BULLETS ATTIVO");
                ActivateEffectForTheEarth(new Earth.DoubleBulletsPowerUp(earth) { Duration = howLong });
                break;
            case 152:

                break;
            case 153:

                break;

            //MOON Power Ups starting from 200
            case 201:
                Debug.Log("RED MOON ATTIVO");
                ActivateEffectForTheMoon(new MoonManager.RedMoonPowerUp(moonManager) { Duration = howLong }); 
                break;
            case 202:
                Debug.Log("MOON SCYTHES ATTIVO");
                ActivateEffectForTheMoon(new MoonManager.MoonScythes(moonManager) { Duration = 2 });
                break;
            case 203: 
                ActivateEffectForTheMoon(new MoonManager.FullMoon(moonManager) { Duration = howLong });
                Debug.Log("FULL MOON ATTIVO");
                break;

                //+1hp green power up
            case 301:
                Earth.Instance.AddHp(); Debug.Log("+1 HP ! ");
                UIManager.Instance.UpdateHpText();
                break;
        }
    }

    /// <summary>
    /// Attiva un effetto sulla luna
    /// </summary>
    /// <param name="PowerUpToActivate"></param>
    private void ActivateEffectForTheMoon(PowerUpDefinition PowerUpToActivate)
    {
        // Se c'è già un un effetto
        if (activeMoonEffect != null)
        {
            // Fermiamo l'effetto che è stato lanciato 
            StopCoroutine(activeMoonEffect.InternalCoroutine);

            // Deattivazione
            activeMoonEffect.Deactivate();
        }

        // Attiva l'effetto ottenuto
        activeMoonEffect = PowerUpToActivate;

        // Attiviamolo!
        activeMoonEffect.Activate();

        // Avvia la coroutine dell'effetto
        activeMoonEffect.InternalCoroutine = StartCoroutine(PowerUpToActivate.Wait());
    }

    /// <summary>
    /// Attiva un effetto sulla luna
    /// </summary>
    /// <param name="PowerUpToActivate"></param>
    private void ActivateEffectForTheEarth(PowerUpDefinition PowerUpToActivate)
    {
        // Se c'è già un un effetto
        if (activeEarthEffect != null)
        {
            // Fermiamo l'effetto che è stato lanciato 
            StopCoroutine(activeEarthEffect.InternalCoroutine);

            // Deattivazione
            activeEarthEffect.Deactivate();
        }

        // Attiva l'effetto ottenuto
        activeEarthEffect = PowerUpToActivate;

        // Attiviamolo!
        activeEarthEffect.Activate();

        // Avvia la coroutine dell'effetto
        activeEarthEffect.InternalCoroutine = StartCoroutine(PowerUpToActivate.Wait());
    }
}
