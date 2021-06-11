using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{

    [SerializeField] GameObject asteroidPrefab;
    [SerializeField][Range(1,10)] float _timeSpawner;

    List<GameObject> asteroids = new List<GameObject>();

    private void Awake()
    {
        GameStateManager.Instance.OnStateHaveBennChanged += OnStateChanged;
    }
    public void OnStateChanged()
    {
        if (GameStateManager.Instance.GetState() == GameStateManager.GameState.Play)
        {
            InvokeRepeating("GenAst", 3f, _timeSpawner);
        }
        else if (GameStateManager.Instance.GetState() == GameStateManager.GameState.Gameover || GameStateManager.Instance.GetState() == GameStateManager.GameState.Win)
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
