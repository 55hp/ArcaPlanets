using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] protected float life;
    float actualLife;
    float dmg;
    Vector3 myPosition;

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
        myPosition = gameObject.transform.position;
        if (life == 0)
        {
            actualLife = 1f;
        }
        dmg = 0.1f / actualLife;
    }


    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                //TODO Change the method above into a GenerateNewPlanet [different hp, assets, ext ]
                ResetPlanet();
                break;
            case GameManager.GameState.Play:
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.Gameover:
                break;
            case GameManager.GameState.Win:
                gameObject.GetComponent<Animator>().SetTrigger("die");
                break;
        }
    }

    public void ResetPlanet()
    {
        actualLife = life;
        gameObject.GetComponent<Animator>().Play("Idle");
        gameObject.transform.SetPositionAndRotation(myPosition, Quaternion.identity);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Moon")
        {
            DecreaseLife(collision.gameObject.GetComponent<Moon>().GetDmg());
        }
    }

    public void DecreaseLife(float damage)
    {
        actualLife -= damage;
        EventManager.DealDamageToThePlanet(damage / life);
        gameObject.GetComponent<Animator>().SetTrigger("take_dmg");

        if (actualLife <= 0)
        {
            EventManager.ChangeGameState(GameManager.GameState.Win);
        }
    }
}
