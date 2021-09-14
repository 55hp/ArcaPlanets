﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject topWall;

    [SerializeField] GameObject topDeadzone;
    
    [SerializeField] Planet planet;
    [SerializeField] Satellite[] satellities;

    [SerializeField] Color[] planetBodyPalette;
    [SerializeField] Color[] satelliteBodyPalette;

    [SerializeField] Sprite[] planetFaces;
    [SerializeField] Sprite[] satelliteFaces;

    [SerializeField] Sprite[] planetBodies;
    [SerializeField] Sprite[] satelliteBodies;

    [SerializeField] GameObject[] bulletPrefabs;

    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
    }
    

    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                SetWalls();
                SetMobs(4);

                StartCoroutine(GetLevelReady());
                break;
            case GameManager.GameState.Play:
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.Gameover:
                ClearStage();
                break;
            case GameManager.GameState.Win:
                ClearStage();
                break;
        }
    }

    IEnumerator GetLevelReady()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.GameReady();
    }
    
    



    public void SetMobs(int satelliteNumber)
    {

        planet.SetPlanet(30, Ut.ROA(planetBodies),Ut.ROA(planetFaces), Ut.ROA(planetBodyPalette), Planet.PLANET_POWER.BLINK_SHIELD, 4, 6f, 6f , bulletPrefabs[2] , bulletPrefabs[3]);
        planet.gameObject.SetActive(true);
        planet.ActivatePlanet();

        if (satelliteNumber > 4 || satelliteNumber < 0)
        {
            satelliteNumber = 1;
        }

        for(int i = 0; i < satelliteNumber; i++)
        {
            satellities[i].SetSatellite(10, Ut.ROA(satelliteBodies), Ut.ROA(satelliteFaces), Ut.ROA(satelliteBodyPalette), Satellite.SATELLITE_POWER.CANNON, 4, 5, 5, bulletPrefabs[0], bulletPrefabs[0]);
            satellities[i].gameObject.SetActive(true);
            satellities[i].ActivateSatellite();
        }
    }


    public void ClearStage()
    {
        planet.gameObject.SetActive(false);
        foreach (Satellite x in satellities)
        {
            x.gameObject.SetActive(false);
        }
    }

    public void SetWalls()
    {
        topWall.transform.position = new Vector3(0, Camera.main.orthographicSize + 0.5f, 0);
        rightWall.transform.position = new Vector3(Camera.main.orthographicSize * Camera.main.aspect + 0.5f, 0, 0);
        leftWall.transform.position = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect - 0.5f, 0, 0);
    }
}
