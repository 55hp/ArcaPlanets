using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingModule : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet1;
    [SerializeField] GameObject bullet2;
    float startingTime;
    float fireRate;

    IEnumerator shootingType;

    public enum ShootingType
    {
        FIXED_RATE_ONE,
        FIXED_RATE_DOUBLE,
        FIXED_RATE_TRIPLE,
        FIXED_RATE_DIVERGENT
    }


    public void SetShootingModule(ShootingType shootingStyle , GameObject projectileType , float startingTime, float fireRate)
    {
        switch (shootingStyle)
        {
            case ShootingType.FIXED_RATE_ONE:
                shootingType = Single();
                break;
            case ShootingType.FIXED_RATE_DOUBLE:
                shootingType = Double();
                break;
            case ShootingType.FIXED_RATE_TRIPLE:
                shootingType = Triple();
                break;
            case ShootingType.FIXED_RATE_DIVERGENT:
                shootingType = Divergent();
                break;

        }

        this.bullet = projectileType;
        this.startingTime = startingTime;
        this.fireRate = fireRate;
    }

    public void SetShootingModule(ShootingType shootingStyle, GameObject rightBullet , GameObject leftBullet, float startingTime, float fireRate)
    {
        switch (shootingStyle)
        {
            case ShootingType.FIXED_RATE_ONE:
                shootingType = Single();
                break;
            case ShootingType.FIXED_RATE_DOUBLE:
                shootingType = Double();
                break;
            case ShootingType.FIXED_RATE_TRIPLE:
                shootingType = Triple();
                break;
            case ShootingType.FIXED_RATE_DIVERGENT:
                shootingType = Divergent();
                break;

        }

        this.bullet1 = rightBullet;
        this.bullet2 = leftBullet;
        this.startingTime = startingTime;
        this.fireRate = fireRate;
    }

    public void TurnOn()
    {
        if (shootingType != null) StartCoroutine(shootingType);
    }

    public void TurnOff()
    {
        if (shootingType != null) StopCoroutine(shootingType);
    }

    private void OnDisable()
    {
        if (shootingType != null) StopCoroutine(shootingType);
    }


    IEnumerator Single()
    {
        yield return new WaitForSeconds(startingTime);
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(bullet, this.transform.position, Quaternion.identity);
        }
    }

    IEnumerator Double()
    {
        yield return new WaitForSeconds(startingTime);
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(bullet, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Instantiate(bullet, this.transform.position, Quaternion.identity);
        }
    }

    IEnumerator Triple()
    {
        yield return new WaitForSeconds(startingTime);
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(bullet, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.4f);
            Instantiate(bullet, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.4f);
            Instantiate(bullet, this.transform.position, Quaternion.identity);
        }
    }

    IEnumerator Divergent()
    {
        yield return new WaitForSeconds(startingTime);
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(bullet1, this.transform.position, Quaternion.identity);
            Instantiate(bullet2, this.transform.position, Quaternion.identity);

        }
    }



}
