using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    Rigidbody2D myRb;
    float constantSpeed;
    int dmg;


    private void Start()
    {
        constantSpeed = 6.2f;
        myRb = gameObject.GetComponent<Rigidbody2D>();

        if(dmg == 0)
        {
            dmg = 1;
        }
    }

    public void SetDmg(int amount)
    {
        dmg = amount;
    }
    public int GetDmg()
    {
        return dmg;
    }

    private void LateUpdate()
    {
        myRb.velocity = constantSpeed * (myRb.velocity.normalized);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadzone"))
        {
            EventManager.LoseLife();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadzone"))
        {
            MoonManager.Instance.ResetBall();
        }
    }
}
