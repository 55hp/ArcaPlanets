using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonManager : Singleton<MoonManager>
{
    [SerializeField]
    private Moon ballPrefab;

    private Moon initialBall;

    private Rigidbody2D initialBallRb;

    public float initialBallSpeed = 250;

    private void Awake()
    {
        GameStateManager.Instance.OnStateHaveBennChanged += OnStateChanged;
    }

    private void Update()
    {
        if (GameStateManager.Instance.myState == GameStateManager.GameState.Boot)
        {
            // Align ball position to the paddle position
            Vector3 paddlePosition = EarthController.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + .5f, 0);
            initialBall.transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0) || Input.touches.Length > 0)
            {
                initialBallRb.AddForce(new Vector2(0, initialBallSpeed));
                GameStateManager.Instance.ChangeState(GameStateManager.GameState.Play);
            }
        }
    }


    public void OnStateChanged()
    {
        if (GameStateManager.Instance.GetState() == GameStateManager.GameState.Boot)
        {
            InitBall();
        }
        else if (GameStateManager.Instance.GetState() == GameStateManager.GameState.Gameover || GameStateManager.Instance.GetState() == GameStateManager.GameState.Win)
        {
            Destroy(initialBall.gameObject);
        }
    }


    public void InitBall()
    {
        Vector3 paddlePosition = EarthController.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + .5f, 0);
        initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        initialBallRb = initialBall.GetComponent<Rigidbody2D>();
    }

}
