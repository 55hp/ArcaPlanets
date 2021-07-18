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

    private void LateUpdate()
    {
        myRb.velocity = constantSpeed * (myRb.velocity.normalized);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Deadzone"))
        {
            EventManager.LoseLife();
        }
    }

    

}
