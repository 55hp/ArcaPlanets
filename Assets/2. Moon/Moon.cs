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

    string name;

    [SerializeField] GameObject moonHitPrefab;

    public void SetName(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return name;
    }

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

    public void MoonSpinning(bool spin)
    {
        spinning = spin;
        if (spinning)
        {
            StartCoroutine(Spin());
        }
        else
        {
            StopCoroutine(Spin());
        }
    }

    private void LateUpdate()
    {
        myRb.velocity = constantSpeed * (myRb.velocity.normalized);
    }

    IEnumerator Spin()
    {
        while (spinning)
        {
            yield return new WaitForSeconds(0.1f);
            transform.Rotate(0f, 0f, 100f, Space.Self);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadzone"))
        {
            MoonManager.Instance.MoonOutOfScreen();
            Destroy(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Earth")
        {
            return;
        }
        else
        {
            Instantiate(moonHitPrefab, new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y, 0), Quaternion.identity);
        }

    }

}
