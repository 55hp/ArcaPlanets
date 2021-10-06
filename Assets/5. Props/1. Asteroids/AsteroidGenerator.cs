using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : Singleton<AsteroidGenerator>
{

    [SerializeField] GameObject asteroidPrefab;
    [SerializeField] GameObject asteroidAxplosionPrefab;

    [SerializeField] Sprite[] smallAstSprites1;
    [SerializeField] Sprite[] smallAstSprites2;
    [SerializeField] Sprite[] smallAstSprites3;

    [SerializeField] Sprite[] midAstSprites1;
    [SerializeField] Sprite[] midAstSprites2;
    [SerializeField] Sprite[] midAstSprites3;

    [SerializeField] Sprite[] bigAstSprites1;
    [SerializeField] Sprite[] bigAstSprites2;
    [SerializeField] Sprite[] bigAstSprites3;

    Sprite[][] smalls = new Sprite[3][];
    Sprite[][] mids= new Sprite[3][];
    Sprite[][] bigs = new Sprite[3][];

    [SerializeField] Color[] asteroidColors;

    [SerializeField] GameObject[] powerUpPrefabs;

    [SerializeField] [Range(0.1f, 4)] private float _timeSpawner;

    List<GameObject> asteroids = new List<GameObject>();

    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
    }

    private void Start()
    {
        
        smalls[0] = smallAstSprites1;
        smalls[1] = smallAstSprites2;
        smalls[2] = smallAstSprites3;

        mids[0] = midAstSprites1;
        mids[1] = midAstSprites2;
        mids[2] = midAstSprites2;

        bigs[0] = bigAstSprites1;
        bigs[1] = bigAstSprites2;
        bigs[2] = bigAstSprites3;

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

            var newAsteroid = Instantiate(asteroidPrefab, new Vector3(-5, Random.Range(-0.5f, 1.5f), 0), Quaternion.identity);
            RandomizeAsteroid(newAsteroid.GetComponent<Asteroid>());
            asteroids.Add(newAsteroid);
        }
    }

    public static int[] pseudoRandomicArray = {  0, 0, 0, 0, 0, 1, 1, 1, 2, 2 };

    public void RandomizeAsteroid(Asteroid Asteroid)
    {
        int x = Ut.ROA(pseudoRandomicArray);
        switch (x)
        {
            //Small Asteroid - Speed : 1 --> 3
            case 0:
                Asteroid.SetAsteroid(Ut.ROA(smalls), Ut.ROA(powerUpPrefabs), Random.Range(0.5f,2.8f) , Ut.ROA(asteroidColors) , 1 , asteroidAxplosionPrefab);
                break;

            //Medium Asteroid
            case 1:
                Asteroid.SetAsteroid(Ut.ROA(mids), Ut.ROA(powerUpPrefabs), Random.Range(0.5f, 1.5f), Ut.ROA(asteroidColors) , 2 , asteroidAxplosionPrefab);
                break;

            //Big Asteroid
            case 2:
                Asteroid.SetAsteroid(Ut.ROA(bigs), Ut.ROA(powerUpPrefabs), Random.Range(0.4f, 1f), Ut.ROA(asteroidColors) , 3 , asteroidAxplosionPrefab);
                break;
        }
    }



}
