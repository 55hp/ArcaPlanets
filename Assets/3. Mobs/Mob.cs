using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    //This is my life...
    float startingHp;
    float actualHp;

    //This means it's a Planet!
    bool imTheBoss;

    //My London look!
    Sprite myBody;
    Sprite myPrettyFace;
    Color mySkin;

    private void Start()
    {
        myBody = gameObject.GetComponent<SpriteRenderer>().sprite;
        myPrettyFace = gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
        mySkin = gameObject.GetComponents<SpriteRenderer>()[0].color;
    }

    public void MakeMeTheEvilestPlanetOfTheStage(int Hp, Sprite body , Sprite face, Color color)
    {
        startingHp = Hp;
        imTheBoss = true;

        myBody = body;
        myPrettyFace = face;
        mySkin = color;
    }

    public void MakeMeAnAttractiveSatellite(int Hp, Sprite body, Sprite face, Color color)
    {
        startingHp = Hp;
        imTheBoss = false;

        myBody = body;
        myPrettyFace = face;
        mySkin = color;
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
