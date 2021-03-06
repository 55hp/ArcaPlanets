using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingModule : MonoBehaviour 
{
    
    GameObject bullet1;
    GameObject bullet2;

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


    public void SetShootingModule(ShootingType shootingStyle, GameObject bulletType1 , GameObject bulletType2, float startingTime, float fireRate)
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

        this.bullet1 = bulletType1;
        this.bullet2 = bulletType2;
        this.startingTime = startingTime + Random.Range(0.2f, 3);   // Aggiunto un minimo di randomicità 
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
            Instantiate(bullet1, this.transform.position, Quaternion.identity);
        }
    }

    IEnumerator Double()
    {
        yield return new WaitForSeconds(startingTime);
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(bullet1, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Instantiate(bullet1, this.transform.position, Quaternion.identity);
        }
    }

    IEnumerator Triple()
    {
        yield return new WaitForSeconds(startingTime);
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(bullet1, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.4f);
            Instantiate(bullet1, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.4f);
            Instantiate(bullet1, this.transform.position, Quaternion.identity);
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
