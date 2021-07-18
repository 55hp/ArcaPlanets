using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] Slider lifeSlider;
    [SerializeField] GameObject lifeSliderPicker;

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject congratScreen;
    [SerializeField] GameObject bootScreen;
    [SerializeField] GameObject pauseScreen;

    [SerializeField] Text HP;

    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
        EventManager.OnLifeLost += UpdateHP;
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
        EventManager.OnLifeLost -= UpdateHP;
    }


    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                ResetSlider();
                UpdateHP();
                break;
            case GameManager.GameState.Play:
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


    public void UpdateHP()
    {
        HP.text = "HP: " + Earth.Instance.GetHP();
    }

    public void SetSliderValue(float amount)
    {
        lifeSlider.value -= amount;
        if ( amount <= 0)
        {
            lifeSliderPicker.GetComponent<Image>().color = Color.black;
        }
    }

    public void ResetSlider()
    {
        lifeSlider.value = 1;
        lifeSliderPicker.GetComponent<Image>().color = Color.red;
    }

    IEnumerator ActivateWinScreen()
    {
        yield return new WaitForSecondsRealtime(3.8f);
        congratScreen.SetActive(true);

    }
}
