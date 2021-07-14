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

    [SerializeField] GameObject earthIconPrefab;
    [SerializeField] GameObject earthIconPanel;


    private void Start()
    {
        GameManager.Instance.OnStateHaveBeenChanged += OnStateChanged;
    }

    public void OnDisable()
    {
        GameManager.Instance.OnStateHaveBeenChanged -= OnStateChanged;
    }

    List<GameObject> lives = new List<GameObject>();
    int counter;
    public void InitLives()
    {
        lives.Clear();
        GameObject newLife;
        for(int i = 0; i < Earth.Instance.GetHP(); i++)
        {
            newLife = Instantiate(earthIconPrefab, earthIconPanel.transform);
            lives.Add(newLife);
            counter++;
        }
    }


    public void DecreaseLives()
    {
        if(counter >= 0)
        {
            lives[counter].SetActive(false);
            Destroy(lives[counter]);
            lives.RemoveAt(counter);
            counter--;
        }
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
            bootScreen.SetActive(true);
            ResetSlider();
            counter = 0;
            InitLives();
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
