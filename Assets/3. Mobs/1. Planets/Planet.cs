﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] protected float life;
    float actualLife;
    float dmg;
    Vector3 myPosition;



    private void OnDisable()
    {
        GameManager.Instance.OnStateHaveBeenChanged -= OnStateChanged;
    }

    private void Start()
    {
        GameManager.Instance.OnStateHaveBeenChanged += OnStateChanged;
        myPosition = gameObject.transform.position;

        if (life == 0)
        {
            actualLife = 1f;
        }

        dmg = 0.1f / actualLife;


    }

    public void OnStateChanged()
    {
        if (GameManager.Instance.GetState() == GameManager.GameState.Boot)
        {
            ResetPlanet();
        }
        else if(GameManager.Instance.GetState() == GameManager.GameState.Win)
        {
            gameObject.GetComponent<Animator>().SetTrigger("die");
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
            DecreaseLife(collision.gameObject);
        }
    }

    public void DecreaseLife(GameObject moon)
    {
        actualLife -= 0.1f;

        UIManager.Instance.SetSliderValue(dmg);

        gameObject.GetComponent<Animator>().SetTrigger("take_dmg");
        if (actualLife <= 0)
        {
            UIManager.Instance.SetSliderValue(0);
            GameManager.Instance.ChangeState(GameManager.GameState.Win);
        }
    }

}