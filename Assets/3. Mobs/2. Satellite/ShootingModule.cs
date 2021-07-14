using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingModule : MonoBehaviour
{

    [SerializeField] float startingTime;
    [SerializeField] float rateOfFire;
    [SerializeField] GameObject projectilePref;



    public void StartShooting()
    {
        if (rateOfFire != 0 )
        {
            CancelInvoke();
            InvokeRepeating("Shoot", startingTime, rateOfFire);
        }
    }
    
    public void Shoot()
    {
        Instantiate(projectilePref , this.transform.position , Quaternion.identity);
        Debug.Log("Sparo");
    }

    public void Stop()
    {
        CancelInvoke();
    }
}
