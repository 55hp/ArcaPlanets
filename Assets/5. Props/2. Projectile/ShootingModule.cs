using System.Collections;
using UnityEngine;

public class ShootingModule : MonoBehaviour
{
    GameObject projectileType;
    IEnumerator myShootingStyle;
    
    public void InitGun( GameObject projectileType, float startingTime, float rateOfFire)
    {
        this.projectileType = projectileType;
        myShootingStyle = Shoot(startingTime, rateOfFire);
    }


    public void TurnOn()
    {
        StartCoroutine(myShootingStyle);
    }

    public void TurnOff()
    {
        StopCoroutine(myShootingStyle);
    }
    
    
    IEnumerator Shoot(float startingTime , float fireRate)
    {
        yield return new WaitForSeconds(startingTime);
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(projectileType, this.transform.position, Quaternion.identity);
        }
    }
}
