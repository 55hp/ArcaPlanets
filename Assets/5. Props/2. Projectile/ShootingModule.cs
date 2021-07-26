using System.Collections;
using UnityEngine;

public class ShootingModule : MonoBehaviour
{
    public float rateOfFire;
    public float startingTime;
    public GameObject projectileType;
    
    public bool active;
    
    public void InitGun( GameObject projectileType, float startingTime, float rateOfFire)
    {
        this.rateOfFire = rateOfFire;
        this.startingTime = startingTime;
        this.projectileType = projectileType;
    }

    public void TurnOn()
    {
        active = true;
        StartCoroutine(Shoot());
    }

    public void TurnOff()
    {
        active = false;
        StopAllCoroutines();
    }
    
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(startingTime);
        while (active)
        {
            yield return new WaitForSeconds(rateOfFire);
            Instantiate(projectileType, this.transform.position, Quaternion.identity);
        }
    }
}
