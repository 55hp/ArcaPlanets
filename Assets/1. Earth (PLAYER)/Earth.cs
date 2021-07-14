﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : Singleton<Earth>
{
    /// <summary>
    /// The Earth Class define charateristics and behaviors of the Player of the game rapresented by a Earth Planet/ship
    /// </summary>
    [SerializeField] int hp;
    [SerializeField] GameObject lowerShield;
    bool alive;
    Vector3 mySize;


    public void OnDisable()
    {
        GameManager.Instance.OnStateHaveBeenChanged -= OnStateChanged;
    }

    public void OnStateChanged()
    {
        if (GameManager.Instance.GetState() == GameManager.GameState.Play)
        {
            gameObject.GetComponent<EarthController>().GoPlay(true);
        }
        else if (GameManager.Instance.GetState() == GameManager.GameState.Boot)
        {
            gameObject.GetComponent<EarthController>().ResetEarthPosition();
        }
        else
        {
            gameObject.GetComponent<EarthController>().GoPlay(false);
        }

    }

    
    private void Start()
    {
        GameManager.Instance.OnStateHaveBeenChanged += OnStateChanged;
        mySize = transform.localScale;

        if (hp == 0)
        {
            hp = 3;
            alive = true;
        }

        lowerShield.SetActive(false);
    }

    public bool IsAlive()
    {
        return alive;
    }

    public int GetHP()
    {
        return hp;
    }

    public void SetHP(int amount)
    {
        hp = amount;
    }

    /// <summary>
    /// This method will be called every time something occurs that deals dmg to our EarthShip.
    /// </summary>
    /// <param name="dmg"></param>
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        UIManager.Instance.DecreaseLives();
        if(hp <= 0)
        {
            alive = false;
            GameManager.Instance.ChangeState(GameManager.GameState.Gameover);
        }
    }

    /// <summary>
    ///  When the Earth collides with the moon it will be applied a force on the Moon based on the point of collision.
    ///  This method will be (probably) modified later.
    /// </summary>
    /// <param name="coll"></param>
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Moon")
        {
            Rigidbody2D ballRb = coll.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = coll.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPoint.x;

            if (hitPoint.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), MoonManager.Instance.initialMoonSpeed));
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), MoonManager.Instance.initialMoonSpeed));
            }
        }
    }

    /// <summary>
    /// When a projectile collides with the Earth, a decrease life will occurs. 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            TakeDamage(collision.gameObject.GetComponent<Projectile>().GetDamage());
        }
    }

    /// <summary>
    /// Power ups are coroutines that occurs for a determinated amount of time and applies different effects
    /// </summary>
    #region POWER UP EFFECTS REGION

    /// <summary>
    /// The Earth size increase.
    /// </summary>
    /// <param name="timer"></param>
    /// <returns></returns>
    public IEnumerator Bigger(float timer)
    {
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);

        yield return new WaitForSeconds(timer);

        gameObject.transform.localScale = mySize;
    }
    /// <summary>
    /// The Earth size reduce.
    /// </summary>
    /// <param name="timer"></param>
    /// <returns></returns>
    public IEnumerator Smaller(float timer)
    {
        gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1);

        yield return new WaitForSeconds(timer);

        gameObject.transform.localScale = mySize;
    }

    /// <summary>
    /// The lower shield ( a collider that covers the bottom of the screen) appears and then when the coroutine ends it disappear.
    /// </summary>
    /// <param name="timer"></param>
    /// <returns></returns>
    public IEnumerator LowShield(float timer)
    {
        lowerShield.SetActive(true);

        yield return new WaitForSeconds(timer);

        lowerShield.SetActive(false);
    }


    #endregion
}