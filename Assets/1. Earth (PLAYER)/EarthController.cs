using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthController : Singleton<EarthController>
{
    [SerializeField] Swipe swipeController;
    [SerializeField] float speed;
    bool isActive;
    Vector3 myPosition;

    private void Awake()
    {
        GameStateManager.Instance.OnStateHaveBennChanged += OnStateChanged;
    }

    private void Start()
    {
        isActive = false;
        myPosition = gameObject.transform.position;
    }

    public void OnStateChanged()
    {
        if(GameStateManager.Instance.GetState() == GameStateManager.GameState.Play || GameStateManager.Instance.GetState() == GameStateManager.GameState.Boot || GameStateManager.Instance.GetState() == GameStateManager.GameState.Pause)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
        gameObject.SetActive(isActive);

        if(GameStateManager.Instance.GetState() == GameStateManager.GameState.Boot)
        ResetEarth();
    }

    public void ResetEarth()
    {
        gameObject.transform.SetPositionAndRotation(myPosition, Quaternion.identity);
    }


    private void Update()
    {
        if (swipeController.SwipeLeft && this.transform.position.x > -2.7f && isActive)
        {
            this.transform.position += Vector3.left * Mathf.Clamp(speed , 0,10) * Time.fixedDeltaTime; 
        }
        else if (swipeController.SwipeRight && this.transform.position.x < 2.7f && isActive)
        {
            this.transform.position += Vector3.right * speed * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Moon")
        {
            Rigidbody2D ballRb = coll.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = coll.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPoint.x;

            if (hitPoint.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), MoonManager.Instance.initialBallSpeed));
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), MoonManager.Instance.initialBallSpeed));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            GameStateManager.Instance.ChangeState(GameStateManager.GameState.Gameover);
        }
    }
}
