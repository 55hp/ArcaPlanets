using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : Singleton<AsteroidGenerator>
{

    [SerializeField] GameObject asteroidPrefab;
    [SerializeField] Sprite[] androidSprites;
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
                StartCoroutine(GenAsteroids(3f));
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
        CancelInvoke("GenAst");
    }

    public IEnumerator GenAsteroids(float rate)
    {
        while (true)
        {
            yield return new WaitForSeconds(rate);
            asteroids.Add(Instantiate(asteroidPrefab, new Vector3(-5, Random.Range(-1, 2), 0), Quaternion.identity));
        }
    }

    public GameObject GenRandomAsteroid()
    {
        Asteroid x = 

        return Asteroid;
    }
}
