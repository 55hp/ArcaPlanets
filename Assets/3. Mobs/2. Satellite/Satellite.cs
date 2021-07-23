using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{

    [SerializeField] protected float life;
    float actualLife;
    Color startingColor;

    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
    }

    private void Start()
    {
        startingColor = gameObject.GetComponent<SpriteRenderer>().color;

        if (life == 0)
        {
            actualLife = 1f;
        }
        else
        {
            actualLife = life;
        }
    }

    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                ResetSatellite();
                break;
            case GameManager.GameState.Play:
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.Gameover:
                break;
            case GameManager.GameState.Win:
                break;
        }
    }

    public void TakeDamage()
    {
        actualLife -= 0.1f;
        if (actualLife <= 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetTrigger("take_dmg");
        }
    }

    public void ResetSatellite()
    {
        gameObject.GetComponent<SpriteRenderer>().color = startingColor;
        this.gameObject.SetActive(true);

        if (life == 0)
        {
            actualLife = 1f;
        }
        else
        {
            actualLife = life;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Moon")
        {
            TakeDamage();
        }
    }
}
