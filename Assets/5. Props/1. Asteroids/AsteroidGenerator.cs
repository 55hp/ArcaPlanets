using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : Singleton<AsteroidGenerator>
{

    [SerializeField] GameObject asteroidPrefab;

    [SerializeField] Sprite[] smallAstSprites;
    [SerializeField] Sprite[] midAstSprites;
    [SerializeField] Sprite[] bigAstSprites;
    [SerializeField] Color[] asteroidColors;

    [SerializeField] GameObject[] powerUpPrefabs;

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
    

    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                CleanAsteroids();
                StartCoroutine(GenAsteroids(_timeSpawner));
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

            RandomizeAsteroid();
            asteroids.Add(Instantiate(asteroidPrefab, new Vector3(-5, Random.Range(-0.5f, 1.5f), 0), Quaternion.identity));
        }
    }

    public static int[] pseudoRandomicArray = { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2 };

    public void RandomizeAsteroid()
    {
        int x = Ut.ROA(pseudoRandomicArray);
        switch (x)
        {
            //Small Asteroid
            case 0:
                asteroidPrefab.GetComponent<Asteroid>().SetAsteroid(Ut.ROA(smallAstSprites),3, Ut.ROA(asteroidColors));
                break;

            //Medium Asteroid
            case 1:
                asteroidPrefab.GetComponent<Asteroid>().SetAsteroid(Ut.ROA(midAstSprites), Ut.ROA(powerUpPrefabs), 1, Ut.ROA(asteroidColors));
                break;

            //Big Asteroid
            case 2:
                asteroidPrefab.GetComponent<Asteroid>().SetAsteroid(Ut.ROA(bigAstSprites), Ut.ROA(powerUpPrefabs), 0.5f, Ut.ROA(asteroidColors));
                break;
        }
    }
}
