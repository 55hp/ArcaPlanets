using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{

    [SerializeField] protected float life;
    float actualLife;
    Color startingColor;


    private void Start()
    {
        GameManager.Instance.OnStateHaveBeenChanged += OnStateChanged;
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

    public void OnStateChanged()
    {
        if (GameManager.Instance.GetState() == GameManager.GameState.Play)
        {
            ResetSatellite();
        }
    }

    public void TakeDamage()
    {
        actualLife -= 0.1f;
        
        if (actualLife <= 0)
        {
            this.gameObject.SetActive(false);
            this.gameObject.GetComponent<ShootingModule>().Stop();
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

            this.gameObject.GetComponent<ShootingModule>().StartShooting();
        
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Moon")
        {
            TakeDamage();

        }
    }
}
