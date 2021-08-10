using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    Rigidbody2D myRb;
    public float constantSpeed;
    int dmg;
    bool spinning;

    private void Start()
    {
        spinning = false;
        constantSpeed = 3.5f;
        myRb = gameObject.GetComponent<Rigidbody2D>();
        if (dmg == 0)
        {
            dmg = 1;
        }
    }

    public void ChangeMoonSprite(Sprite newMoonSkin)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newMoonSkin;
    }

    public void SetDmg(int amount)
    {
        dmg = amount;
    }
    public int GetDmg()
    {
        return dmg;
    }

    public void Spin(bool spin)
    {
        spinning = spin;
    }



    private void LateUpdate()
    {
        myRb.velocity = constantSpeed * (myRb.velocity.normalized);

        if (spinning)
        {
            transform.Rotate(Vector3.forward * 3f, Space.World);
            Debug.Log("spinno!!!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadzone"))
        {
            MoonManager.Instance.MoonOutOfScreen(this);
        }
    }
}
