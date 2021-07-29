using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] MobManager mobManager;

    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject topWall;



    public void SetWalls()
    {

        Debug.Log("" + Camera.main.orthographicSize);
        Debug.Log("" + Camera.main.aspect);

        topWall.transform.position = new Vector3(0 ,Camera.main.orthographicSize + 0.5f, 0);

        rightWall.transform.position = new Vector3(Camera.main.orthographicSize * Camera.main.aspect + 0.5f , 0 , 0);
        leftWall.transform.position = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect - 0.5f , 0 , 0);
    }


    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
        SetWalls();
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
                mobManager.SetNumberOfMobsForThisStage(3);
                mobManager.InitMobs();
                break;
            case GameManager.GameState.Play:
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.Gameover:
                mobManager.ClearMobsFromStage();
                break;
            case GameManager.GameState.Win:
                mobManager.ClearMobsFromStage();
                break;
        }
    }



}
