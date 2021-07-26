﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    //Well, this is my face... it's separated from my body.
    [SerializeField] GameObject myFace;

    //I have a gun, but sometimes it doesn't works
    ShootingModule myBeautifulGun;

    //This is my life...
    float startingHp;
    float actualHp;

    //This means it's a Planet!
    bool imTheBoss;

    public void MakeMeTheEvilestPlanetOfTheStage(int Hp, Sprite body , Sprite face, Color color)
    {
        startingHp = Hp;
        actualHp = startingHp;
        imTheBoss = true;

        gameObject.GetComponent<SpriteRenderer>().sprite = body;
        myFace.GetComponent<SpriteRenderer>().sprite = face;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    public void MakeMeAnAttractiveSatellite(int Hp, Sprite body, Sprite face, Color color)
    {
        startingHp = Hp;
        actualHp = startingHp;
        imTheBoss = false;

        gameObject.GetComponent<SpriteRenderer>().sprite = body;
        myFace.GetComponent<SpriteRenderer>().sprite = face;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    public void GiveMeAGun(GameObject bullet , float startingFire , float fireRate)
    {
        myBeautifulGun = GetComponent<ShootingModule>();
        myBeautifulGun.InitGun(bullet, startingFire, fireRate);
        myBeautifulGun.TurnOn();
    }

    private void OnDisable()
    {
        if(myBeautifulGun != null)
        myBeautifulGun.TurnOff();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Moon")
        {
            DecreaseLife(collision.gameObject.GetComponent<Moon>().GetDmg());
        }else if (collision.gameObject.tag == "Projectile")
        {
            DecreaseLife(collision.gameObject.GetComponent<Projectile>().GetDmg());
        }
    }

    public void DecreaseLife(float damage)
    {
        actualHp -= damage;

        if(imTheBoss)
        EventManager.DealDamageToThePlanet(damage / startingHp);
        
        //Am I DEAD?!
        if (actualHp <= 0)
        {
            if (imTheBoss) //I'm The only Planet of this Stage
            {
                EventManager.ChangeGameState(GameManager.GameState.Win);
            }
            else //I'm just a Satellite
            {
                gameObject.SetActive(false);
            }
        }
    }

    
}
