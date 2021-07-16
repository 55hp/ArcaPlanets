using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : Singleton<AsteroidGenerator>
{

    [SerializeField] GameObject asteroidPrefab;
    [SerializeField] [Range(1, 10)] private float _timeSpawner;

    List<GameObject> asteroids = new List<GameObject>();

    private void Start()
    {
        GameManager.Instance.OnStateHaveBeenChanged += OnStateChanged;
    }


    public void OnStateChanged()
    {
        if (GameManager.Instance.GetState() == GameManager.GameState.Play)
        {
            InvokeRepeating("GenAst", 3f, _timeSpawner);
        }
        else if (GameManager.Instance.GetState() == GameManager.GameState.Gameover || GameManager.Instance.GetState() == GameManager.GameState.Win)
        {
            foreach(GameObject ast in asteroids)
            {
                if(ast != null) Destroy(ast);
            }
            asteroids.Clear();
            CancelInvoke("GenAst");
        }
    }

    public void GenAst()
    {
        asteroids.Add(Instantiate(asteroidPrefab, new Vector3(-5,Random.Range(-1,2),0) , Quaternion.identity));
    }

}
