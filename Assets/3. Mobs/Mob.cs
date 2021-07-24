using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] GameObject myFace;
    [SerializeField] GameObject myShootingModule;


    //This is my life...
    float startingHp;
    float actualHp;

    //This means it's a Planet!
    bool imTheBoss;

    public void MakeMeTheEvilestPlanetOfTheStage(int Hp, Sprite body , Sprite face, Color color , bool shooter)
    {
        startingHp = Hp;
        actualHp = startingHp;
        imTheBoss = true;

        gameObject.GetComponent<SpriteRenderer>().sprite = body;
        myFace.GetComponent<SpriteRenderer>().sprite = face;
        gameObject.GetComponent<SpriteRenderer>().color = color;

        myShootingModule.GetComponent<ShootingModule>().TurnOn(shooter);
    }

    public void MakeMeAnAttractiveSatellite(int Hp, Sprite body, Sprite face, Color color, bool shooter)
    {
        startingHp = Hp;
        actualHp = startingHp;
        imTheBoss = false;

        gameObject.GetComponent<SpriteRenderer>().sprite = body;
        myFace.GetComponent<SpriteRenderer>().sprite = face;
        gameObject.GetComponent<SpriteRenderer>().color = color;

        myShootingModule.GetComponent<ShootingModule>().TurnOn(shooter);
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
