using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{

    [SerializeField] protected float life;
    float actualLife;

    private void Awake()
    {
        GameStateManager.Instance.OnStateHaveBennChanged += OnStateChanged;
    }

    public void OnStateChanged()
    {
        if (GameStateManager.Instance.GetState() == GameStateManager.GameState.Boot)
        {
            ResetSatellite();
        }
    }

    private void Start()
    {
        if (life == 0)
        {
            actualLife = 1f;
        }
        else
        {
            actualLife = life;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Moon")
        {
            TakeDamage();
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
}
