﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    [SerializeField] Mob[] mobsGameobjects;
    
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
        if (mobsNumber > 4 || mobsNumber < 1)
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
        mobsGameobjects[0].MakeMeTheEvilestPlanetOfTheStage(30, Ut.ROA(planetBodies), Ut.ROA(planetFaces), Ut.ROA(planetColors));
        mobsGameobjects[0].Live();
        mobsGameobjects[0].GetComponent<MobAnimationController>().MakeMeMoving(Ut.TossCoin(), 0.5f, MobAnimationController.MovSpeed.Slow);

        //Satellite generation
        for (int i = 1; i <= thisStageMobs; i++)
        {
            mobsGameobjects[i].MakeMeAnAttractiveSatellite(10, Ut.ROA(satBodies), Ut.ROA(satFaces), Ut.ROA(satColors));

            mobsGameobjects[i].Live();
            mobsGameobjects[i].GetComponent<MobAnimationController>().MakeMeMoving(Ut.TossCoin(), 0.3f, MobAnimationController.MovSpeed.Slow);
        }
    }

    public void ClearMobsFromStage()
    {
        foreach(Mob x in mobsGameobjects)
        {
            x.gameObject.SetActive(false);
        }
    }


}
