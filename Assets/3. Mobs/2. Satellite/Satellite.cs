using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    float healthPoints;
    float actualHp;

    [SerializeField] GameObject myBeautifulFace;
    bool alive;

    [SerializeField] GameObject myShield;
    [SerializeField] ShootingModule myWeapon;
    [SerializeField] float distance;

    IEnumerator myPower;
    Animator myAnimator;

    Vector3 startingPos;

    private float randomId;

    private void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
        myPower = null;
        startingPos = this.transform.position;
        randomId = Random.Range(0, 45*6) ;
    }
    

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition() {
        var a = ((randomId  * 360) + Time.time) * Mathf.Deg2Rad ;
        var c = Mathf.Cos(a) * distance;
        this.transform.position = startingPos + new Vector3(c, 0, 0) ;
    }

    
    public enum SATELLITE_POWER
    {
        CANNON,
        GATLING,
        HALF_MOON,
        ASSAULT
    }

    public void SetSatellite(int Hp, Sprite body, Sprite face, Color color, SATELLITE_POWER myPower, float startingTime, float activeTime, float inactiveTime, GameObject bullet1, GameObject bullet2)
    {
        healthPoints = Hp;
        actualHp = healthPoints;
        gameObject.GetComponent<SpriteRenderer>().sprite = body;
        myBeautifulFace.GetComponent<SpriteRenderer>().sprite = face;
        gameObject.GetComponent<SpriteRenderer>().color = color;
        alive = false;

        switch (myPower)
        {
            //In weapon's powers the active time represent the fire rate.
            //Even if to set a shooting module you need to specify 2 bullet types, based on the shooting style the satellite could shot only 1 type of bullet.
            case SATELLITE_POWER.CANNON:
                myWeapon.SetShootingModule(ShootingModule.ShootingType.FIXED_RATE_ONE, bullet1, bullet2, startingTime, activeTime);
                this.myPower = Shoot(startingTime, activeTime);
                break;
            case SATELLITE_POWER.GATLING:
                myWeapon.SetShootingModule(ShootingModule.ShootingType.FIXED_RATE_TRIPLE, bullet1, bullet2, startingTime, activeTime);
                this.myPower = Shoot(startingTime, activeTime);
                break;
            case SATELLITE_POWER.HALF_MOON:
                myWeapon.SetShootingModule(ShootingModule.ShootingType.FIXED_RATE_ONE, bullet1, bullet2, startingTime, activeTime);
                this.myPower = Shoot(startingTime, activeTime);
                break;
            case SATELLITE_POWER.ASSAULT:
                break;
        }
    }


    public void ActivateSatellite()
    {
        alive = true;
        if (myPower != null)
        {
            StartCoroutine(myPower);
        }
    }


    #region SHIELD_REGION

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

    #region WEAPON_REGION

    IEnumerator Shoot(float startingTime, float fireRate)
    {
        myWeapon.TurnOn();
        yield return new WaitForSeconds(startingTime + 0.2f);
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
        if (actualHp <= 0)
        {
            StartCoroutine(KillSatellite());
            return;
        }
        myAnimator.SetTrigger("Damage");
    }



    IEnumerator KillSatellite()
    {
        if (myPower != null)
        {
            StopCoroutine(myPower);
        }
        //TODO VERIFICARE SINCRONIZZAZIONE ANIMAZIONI
        myAnimator.SetTrigger("Die");
        alive = false;
        myAnimator.SetBool("Alive", false);
        yield return new WaitForSeconds(3);
    }
}
