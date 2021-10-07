using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] GameObject[] lives;
    [SerializeField] Color lifeActive;
    [SerializeField] Color lifeLost;


    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject gameHUD;

    [SerializeField] Slider planetHeartSlider;

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject congratScreen;
    [SerializeField] GameObject pauseScreen;
    
    [SerializeField] GameObject fakeMoonPref;
    [SerializeField] GameObject fakeMoonSpawnPoint;
    GameObject fakeMoon;
    

    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
        EventManager.OnPlanetTookDamage += OnPlanetTookDamage;
        //EventManager.OnLifeLost += UpdateHp;

        fakeMoon = Instantiate(fakeMoonPref, fakeMoonSpawnPoint.transform);
        fakeMoon.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 2) * 100);
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
        EventManager.OnPlanetTookDamage -= OnPlanetTookDamage;
        //EventManager.OnLifeLost -= UpdateHp;
    }


    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                ResetSlider();
                SetHp(Earth.Instance.GetHP());
                gameHUD.SetActive(true);
                Destroy(fakeMoon);
                break;
            case GameManager.GameState.Play:
                pauseScreen.SetActive(false);
                break;
            case GameManager.GameState.Pause:
                pauseScreen.SetActive(true);
                break;
            case GameManager.GameState.Gameover:
                gameOverScreen.SetActive(true);
                gameHUD.SetActive(false);
                break;
            case GameManager.GameState.Win:
                StartCoroutine(ActivateWinScreen());
                gameHUD.SetActive(false);
                break;
        }
    }

    public void OnPlanetTookDamage(float amountOfDamage)
    {
        planetHeartSlider.value -= amountOfDamage;
    }


    public void SetHp(int hp)
    {
        int i = 0;
        foreach (GameObject life in lives)
        {

            if (i < hp)
            {
                life.SetActive(true);
                life.GetComponent<Image>().color = lifeActive;
            }
            else
            {
                life.SetActive(false);
            }
            i++;
        }
    }

    public void GainLife()
    {
        if(lives[Earth.Instance.GetHP()].activeInHierarchy)
        lives[Earth.Instance.GetHP()].GetComponent<Image>().color = lifeActive;
    }

    public void LoseLife()
    {
        lives[Earth.Instance.GetHP()].GetComponent<Image>().color = lifeLost;
    }

    public void ResetSlider()
    {
        planetHeartSlider.value = 1;
    }

    IEnumerator ActivateWinScreen()
    {
        yield return new WaitForSecondsRealtime(4f);
        congratScreen.SetActive(true);

    }
}
