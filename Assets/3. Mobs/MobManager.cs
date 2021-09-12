using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    [SerializeField] Planet planet;
    [SerializeField] Satellite[] mobsGameobjects;
    
    [SerializeField] Sprite[] planetBodies;
    [SerializeField] Sprite[] planetFaces;
    [SerializeField] Sprite[] satBodies;
    [SerializeField] Sprite[] satFaces;
    [SerializeField] Color[] planetColors;
    [SerializeField] Color[] satColors;


    [SerializeField] GameObject[] enemyBullets;

    int thisStageMobs;

    public void SetNumberOfMobsForThisStage(int mobsNumber)
    {
        //mobsNumber must be between 1 - 4
        if (mobsNumber > 4 || mobsNumber < 0)
        {
            //Default value = 2.
            thisStageMobs = 2;
        }
        else
        {
            thisStageMobs = mobsNumber;
        }

    }

    public void InitMobs()
    {
        //Planet generation
        planet.RandomizePlanet(5, Ut.ROA(planetBodies), Ut.ROA(planetFaces), Ut.ROA(planetColors),Planet.POWER.BLINK_SHIELD, 10, 8 , 10);
        planet.ActivatePlanet();
        //mobsGameobjects[0].GetComponent<MobAnimationController>().MakeMeMoving(Ut.TossCoin(), 0.5f, MobAnimationController.MovSpeed.Slow);
        //GiveRandomPowerToThePlanet(mobsGameobjects[0]);
        /*
        //Satellite generation
        for (int i = 1; i <= thisStageMobs; i++)
        {
            mobsGameobjects[i].MakeMeAnAttractiveSatellite(10, Ut.ROA(satBodies), Ut.ROA(satFaces), Ut.ROA(satColors));
            mobsGameobjects[i].Live();
            //[i].GetComponent<MobAnimationController>().MakeMeMoving(Ut.TossCoin(), 0.3f, MobAnimationController.MovSpeed.Slow);
            GiveRandomWeapon(mobsGameobjects[i]);
            mobsGameobjects[i].GetComponent<ShootingModule>().TurnOn();
        }
        */
    }

    public void ClearMobsFromStage()
    {
        planet.gameObject.SetActive(false);

        
            foreach (Mob x in mobsGameobjects)
            {
            if(x != null)
            x.gameObject.SetActive(false);
            }
    }


    public void GiveRandomWeapon(Mob satellite)
    {

        //int x = Random.Range(0, 4);

        int x = 1;
        switch (x)
        {
            case 1:
                GiveCannonWeapon(satellite);
                break;
            case 2:
                GiveGatlingWeapon(satellite);
                break;
            case 3:
                GiveHalfMoonWeapon(satellite);
                break;
            default:
                break;
        }
    }

    public void GiveCannonWeapon(Mob mob)
    {
        mob.GetComponent<ShootingModule>().SetShootingModule(ShootingModule.ShootingType.FIXED_RATE_ONE, enemyBullets[0], 0, 8);
    }

    public void GiveGatlingWeapon(Mob mob)
    {
        mob.GetComponent<ShootingModule>().SetShootingModule(ShootingModule.ShootingType.FIXED_RATE_TRIPLE, enemyBullets[1], 0, 8);
    }

    public void GiveHalfMoonWeapon(Mob mob)
    {
        mob.GetComponent<ShootingModule>().SetShootingModule(ShootingModule.ShootingType.FIXED_RATE_ONE, enemyBullets[2], 0, 10);
    }

    /*
    public void GiveRandomPowerToThePlanet(Mob planet)
    {
        //int x = Random.Range(0, 2);
        int x = 0;

        switch (x)
        {
            case 0:
                GiveFullShield(planet);
                break;
            case 1:
                GiveDoublePlanetShoot(planet);
                break;
            case 2:
                GiveDirectionalShield(planet);
                break;
            default:
                break;
        }
    }
    
    public void GiveFullShield(Mob planet)
    {
        planet.GetComponent<ShieldModule>().SetBlinkShieldPower(4, 5, 8);
        planet.GetComponent<ShieldModule>().TurnOn();
    }

    public void GiveDoublePlanetShoot(Mob planet)
    {
        planet.GetComponent<ShootingModule>().SetShootingModule(ShootingModule.ShootingType.FIXED_RATE_DIVERGENT, enemyBullets[3], enemyBullets[4], 0, 10);
        planet.GetComponent<ShootingModule>().TurnOn();
    }


    public void GiveDirectionalShield(Mob planet)
    {
        planet.GetComponent<ShieldModule>().SetBlinkShieldPower(4, 5, 8);
        planet.GetComponent<ShieldModule>().TurnOn();
    }

    */
}
