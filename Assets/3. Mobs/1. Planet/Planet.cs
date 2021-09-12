using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    float healthPoints;
    float actualHp;

    [SerializeField] GameObject myBeautifulFace;
    bool alive;

    [SerializeField] GameObject myShield;
    [SerializeField] GameObject myWeapon;

    IEnumerator power;
    Animator myAnimator;

    public enum POWER
    {
        BLINK_SHIELD,
        DIVERGENT_GUN
    }

    public void RandomizePlanet(int Hp, Sprite body, Sprite face, Color color , POWER myPower , float startingTimer , float powerOnTimer , float powerOffTimer)
    {
        healthPoints = Hp;
        actualHp = healthPoints;
        gameObject.GetComponent<SpriteRenderer>().sprite = body;
        myBeautifulFace.GetComponent<SpriteRenderer>().sprite = face;
        gameObject.GetComponent<SpriteRenderer>().color = color;
        alive = false;

        switch (myPower)
        {
            case POWER.BLINK_SHIELD:
                this.power = BlinkShield(startingTimer, powerOnTimer , powerOffTimer);
                break;
        }
    }

    public void ActivatePlanet()
    {
        alive = true;
        gameObject.SetActive(true);
        myAnimator = gameObject.GetComponent<Animator>();
        StartCoroutine(power);
    }


    IEnumerator KillPlanet()
    {
        myAnimator.SetTrigger("Die");
        alive = false;
        myAnimator.SetBool("Alive", false);
        yield return new WaitForSeconds(3);
        EventManager.ChangeGameState(GameManager.GameState.Win);
    }

    public void DecreaseLife(float damage)
    {
        actualHp -= damage;
        EventManager.DealDamageToThePlanet(damage / healthPoints);
        if (actualHp <= 0)
        {
            StartCoroutine(KillPlanet());
            return;
        }
        myAnimator.SetTrigger("Damage");
    }


    #region SHIELD

    public void ShieldActive()
    {
        myShield.GetComponent<CircleCollider2D>().enabled = true;
    }

    public void ShieldInactive()
    {
        myShield.GetComponent<CircleCollider2D>().enabled = false;
    }


    IEnumerator BlinkShield(float startingTime, float activeTime, float inactiveTime)
    {
        yield return new WaitForSeconds(startingTime);
        while (alive)
        {
            yield return new WaitForSeconds(inactiveTime);
            myAnimator.SetTrigger("ShieldIn");
            yield return new WaitForSeconds(activeTime);
            myAnimator.SetTrigger("ShieldOut");
        }
    }

    #endregion

    #region WEAPON

    public void Shoot()
    {
        myAnimator.SetTrigger("Shoot");
    }
    
    #endregion

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (alive)
        {
            if (collision.gameObject.tag == "Moon")
            {
                DecreaseLife(collision.gameObject.GetComponent<Moon>().GetDmg());
            }
            else if (collision.gameObject.tag == "Projectile")
            {
                DecreaseLife(collision.gameObject.GetComponent<Projectile>().GetDmg());
            }
        }
    }

    
}
