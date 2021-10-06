using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] Slider lifeSlider;

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject congratScreen;
    [SerializeField] GameObject bootScreen;
    [SerializeField] GameObject pauseScreen;

    [SerializeField] Text HP;
    [SerializeField] GameObject fakeMoonPref;
    [SerializeField] GameObject fakeMoonSpawnPoint;
    GameObject fakeMoon;

    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
        EventManager.OnPlanetTookDamage += OnPlanetTookDamage;
        EventManager.OnLifeLost += UpdateHpText;

        fakeMoon = Instantiate(fakeMoonPref, fakeMoonSpawnPoint.transform);
        fakeMoon.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 2) * 100);
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
        EventManager.OnPlanetTookDamage -= OnPlanetTookDamage;
        EventManager.OnLifeLost -= UpdateHpText;
    }


    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                ResetSlider();
                UpdateHpText();
                break;
            case GameManager.GameState.Play:
                Destroy(fakeMoon);
                pauseScreen.SetActive(false);
                break;
            case GameManager.GameState.Pause:
                pauseScreen.SetActive(true);
                break;
            case GameManager.GameState.Gameover:
                gameOverScreen.SetActive(true);
                break;
            case GameManager.GameState.Win:
                StartCoroutine(ActivateWinScreen());
                break;
        }
    }

    public void OnPlanetTookDamage(float amountOfDamage)
    {
        lifeSlider.value -= amountOfDamage;
    }

    public void UpdateHpText()
    {
        HP.text = "HP: " + Earth.Instance.GetHP();
    }

    public void ResetSlider()
    {
        lifeSlider.value = 1;
    }

    IEnumerator ActivateWinScreen()
    {
        yield return new WaitForSecondsRealtime(4f);
        congratScreen.SetActive(true);

    }
}
