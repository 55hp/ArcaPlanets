using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingModule : MonoBehaviour
{

    [SerializeField] float startingTime;
    [SerializeField] float rateOfFire;
    [SerializeField] GameObject projectilePref;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", startingTime, rateOfFire);
    }

    
    public void Shoot()
    {
        Instantiate(projectilePref , this.transform.position , Quaternion.identity);
    }


}
