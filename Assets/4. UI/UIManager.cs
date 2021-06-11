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

    private void Awake()
    {
        GameStateManager.Instance.OnStateHaveBennChanged += OnStateChanged;
    }

    public void OnStateChanged()
    {
        if (GameStateManager.Instance.GetState() == GameStateManager.GameState.Gameover)
        {
            gameOverScreen.SetActive(true);
        }
        else if (GameStateManager.Instance.GetState() == GameStateManager.GameState.Win)
        {
            StartCoroutine(ActivateWinScreen());
        }
        else if (GameStateManager.Instance.GetState() == GameStateManager.GameState.Pause)
        {
            pauseScreen.SetActive(true);
        }
        else if (GameStateManager.Instance.GetState() == GameStateManager.GameState.Boot)
        {
            bootScreen.SetActive(true);
            ResetSlider();
        }
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
        lifeSliderPicker.GetComponent<Image>().color = Color.green;
    }

    IEnumerator ActivateWinScreen()
    {
        yield return new WaitForSecondsRealtime(3.8f);
        congratScreen.SetActive(true);

    }
}
