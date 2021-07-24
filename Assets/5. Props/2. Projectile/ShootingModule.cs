using System.Collections;
using UnityEngine;

public class ShootingModule : MonoBehaviour
{
    float rateOfFire;
    float startingTime;
    GameObject projectileType;

    public void SetShootingModule(float time , float rate , GameObject projectile)
    {
        startingTime = time;
        rateOfFire = rate;
        projectileType = projectile;
    }

    bool active;
    
    public void TurnOn()
    {
        active = true;
        StartCoroutine(Shoot(rateOfFire, startingTime));
    }

    public void TurnOff()
    {
        active = false;
        StopAllCoroutines();
    }
    
    IEnumerator Shoot(float startingTime , float fireRate)
    {
        yield return new WaitForSeconds(startingTime);
        while (active)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(projectileType, this.transform.position, Quaternion.identity);
        }
    }
}
