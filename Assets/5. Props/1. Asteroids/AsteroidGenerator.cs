﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : Singleton<AsteroidGenerator>
{
    [SerializeField] GameObject[] asteroidsPrefab;
    [SerializeField] [Range(1, 10)] private float _timeSpawner;

    List<GameObject> asteroids = new List<GameObject>();

    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
    }

    public int powerUpCounter;
    private void Start()
    {
        powerUpCounter = 2;
    }

    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                CleanAsteroids();
                StartCoroutine(GenAsteroids(2f));
                break;
            case GameManager.GameState.Play:
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.Gameover:
                CleanAsteroids();
                break;
            case GameManager.GameState.Win:
                CleanAsteroids();
                break;
        }
    }


    public void CleanAsteroids()
    {
        StopAllCoroutines();
        foreach (GameObject ast in asteroids)
        {
            if (ast != null) Destroy(ast);
        }
        asteroids.Clear();
    }

    public IEnumerator GenAsteroids(float rate)
    {
        while (true)
        {
            yield return new WaitForSeconds(rate);
            asteroids.Add(Instantiate(GenRandomAsteroid(), new Vector3(-5, Random.Range(-0.5f, 1.5f), 0), Quaternion.identity));
        }
    }


    private static int[] pseudoRandomicIntArray = { 0,0,0,0,0,1,1,1,1,2,2 };
    [SerializeField] GameObject[] powerUpPrefabs;

    public GameObject GenRandomAsteroid()
    {
        int x = pseudoRandomicIntArray[Random.Range(0, pseudoRandomicIntArray.Length)];
        GameObject asteroid = asteroidsPrefab[x * 3 + Random.Range(0, 3)];
        asteroid.GetComponent<Asteroid>().GivePowerUp(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)]);
        
        return asteroid;
    }
}
