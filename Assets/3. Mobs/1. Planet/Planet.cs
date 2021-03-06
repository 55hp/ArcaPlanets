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
    [SerializeField] GameObject mouth;

    IEnumerator myPower;
    Animator myAnimator;
    ShootingScript myWeapon;

    private void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
        myPower = null;
    }

    public enum PLANET_POWER
    {
        BLINK_SHIELD,
        DIVERGENT_GUN,
        SINGLE_SHOT
    }

    public void SetPlanet(int Hp, Sprite body, Sprite face, Color color , PLANET_POWER myPower , float startingTime , float activeTime , float inactiveTime , GameObject bullet )
    {
        healthPoints = Hp;
        actualHp = healthPoints;
        gameObject.GetComponent<SpriteRenderer>().sprite = body;
        myBeautifulFace.GetComponent<SpriteRenderer>().sprite = face;
        gameObject.GetComponent<SpriteRenderer>().color = color;
        alive = false;

        switch (myPower)
        {
            case PLANET_POWER.BLINK_SHIELD:
                this.myPower = BlinkShield(startingTime, activeTime , inactiveTime);
                break;

            case PLANET_POWER.DIVERGENT_GUN:
                myWeapon = this.GetComponent<ShootingScript>();
                myWeapon.SetShootingStyle(bullet, mouth, ShootingScript.ShootingType.DIVERGENT_SHOOT);
                this.myPower = Shooting(startingTime, activeTime);
                break;

            case PLANET_POWER.SINGLE_SHOT:
                myWeapon = this.GetComponent<ShootingScript>();
                myWeapon.SetShootingStyle(bullet, mouth, ShootingScript.ShootingType.SINGLE_SHOOT);
                this.myPower = Shooting(startingTime, activeTime);
                break;
        }
    }

    public void ActivatePlanet()
    {
        alive = true;
        if (myPower != null) {
            StartCoroutine(myPower);
        }
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
            myAnimator.SetBool("Shielding", true);
            myAnimator.SetTrigger("ShieldIn");
            yield return new WaitForSeconds(activeTime);
            myAnimator.SetTrigger("ShieldOut");
            myAnimator.SetBool("Shielding", false);
        }
    }

    #endregion

    #region WEAPON

    public IEnumerator Shooting(float startingTime, float fireRate)
    {
        yield return new WaitForSeconds(startingTime+0.2f);
        while (alive)
        {
            yield return new WaitForSeconds(fireRate);
            myAnimator.SetTrigger("Shoot");
        }
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

    public void DecreaseLife(float damage)
    {
        actualHp -= damage;
        EventManager.DealDamageToThePlanet(damage / healthPoints);
        if (actualHp <= 0)
        {
            KillPlanet();
            return;
        }
        myAnimator.SetTrigger("Damage");
    }

    public void KillPlanet()
    {
        if (myPower != null)
        {
            StopCoroutine(myPower);
        }
        myAnimator.SetTrigger("Die");
        alive = false;
        myAnimator.SetBool("Alive", false);
        EventManager.ChangeGameState(GameManager.GameState.Win);
    }

}
