using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingModule : MonoBehaviour
{
    [SerializeField] GameObject projectileType;
    [SerializeField] float startingTime;
    [SerializeField] float fireRate;


    private void OnEnable()
    {
        StartCoroutine(Shoot());
    }

    private void OnDisable()
    {
        StopCoroutine(Shoot());
    }


    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(startingTime);
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(projectileType, this.transform.position, Quaternion.identity);
        }
    }
}
