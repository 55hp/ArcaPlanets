using System.Collections;
using UnityEngine;

public class ShootingModule : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    [SerializeField] float startingTime;
    [SerializeField] GameObject projectilePref;

    bool active;

    private void OnEnable()
    {
        StopAllCoroutines();
        active = false;
        StartCoroutine(Shoot(rateOfFire, startingTime));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        active = false;
    }

    public void TurnOn(bool active)
    {
        this.active = active;
    }
    
    IEnumerator Shoot(float fireRate, float startingTime)
    {
        yield return new WaitForSeconds(startingTime);

        while (active)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(projectilePref, this.transform.position, Quaternion.identity);
        }
    }
}
