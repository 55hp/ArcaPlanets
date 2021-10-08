using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    
    GameObject bullet;

    GameObject spawner;
    ShootingType shootingStyle;

    public void SetShootingStyle(GameObject bulletType, GameObject shootingPoint, ShootingType shootingStyle)
    {
        this.bullet = bulletType;
        this.spawner = shootingPoint;
        this.shootingStyle = shootingStyle;
    }

    public enum ShootingType
    {
        SINGLE_SHOOT,
        FIXED_RATE_DOUBLE,
        FIXED_RATE_TRIPLE,
        DIVERGENT_SHOOT
    }
    
    public void Shoot()
    {
        switch (shootingStyle)
        {
            case ShootingType.SINGLE_SHOOT:
                Single();
                break;
            case ShootingType.FIXED_RATE_DOUBLE:
                StartCoroutine(Double());
                break;
            case ShootingType.FIXED_RATE_TRIPLE:
                StartCoroutine(Triple());
                break;
            case ShootingType.DIVERGENT_SHOOT:
                Divergent();
                break;
        }
    }

    void Single()
    {
            Instantiate(bullet, this.spawner.transform.position, Quaternion.identity);
    }

    IEnumerator Double()
    {
            Instantiate(bullet, this.spawner.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Instantiate(bullet, this.spawner.transform.position, Quaternion.identity);
    }

    IEnumerator Triple()
    {
        Instantiate(bullet, this.spawner.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(bullet, this.spawner.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(bullet, this.spawner.transform.position, Quaternion.identity);
    }

    void Divergent()
    {
            Instantiate(bullet, this.spawner.transform.position, Quaternion.identity);
            Instantiate(bullet, this.spawner.transform.position, Quaternion.identity);
    }
}
