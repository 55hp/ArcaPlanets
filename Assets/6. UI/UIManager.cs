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
        GameManager.Instance.OnStateHaveBeenChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStateHaveBeenChanged -= OnStateChanged;
    }

    public void UpdateHp()
    {
        HP.text ="HP: " + Earth.Instance.GetHP();
    }

    public void OnStateChanged()
    {
        if (GameManager.Instance.GetState() == GameManager.GameState.Gameover)
        {
            gameOverScreen.SetActive(true);
        }
        else if (GameManager.Instance.GetState() == GameManager.GameState.Win)
        {
            StartCoroutine(ActivateWinScreen());
        }
        else if (GameManager.Instance.GetState() == GameManager.GameState.Pause)
        {
            pauseScreen.SetActive(true);
        }
        else if (GameManager.Instance.GetState() == GameManager.GameState.Boot)
        {
            ResetSlider();
            UpdateHp();
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
        lifeSliderPicker.GetComponent<Image>().color = Color.red;
    }

    IEnumerator ActivateWinScreen()
    {
        yield return new WaitForSecondsRealtime(3.8f);
        congratScreen.SetActive(true);

    }
}
