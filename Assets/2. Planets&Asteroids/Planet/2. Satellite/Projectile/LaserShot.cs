using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.Instance.OnStateHaveBennChanged += OnStateChanged;

        if(speed == 0)
        {
            speed = 5;
        }
    }

    public void OnStateChanged()
    {
        if (GameStateManager.Instance.GetState() == GameStateManager.GameState.Pause || GameStateManager.Instance.GetState() == GameStateManager.GameState.Play)
        {
            return;
        }

        GameStateManager.Instance.OnStateHaveBennChanged -= OnStateChanged;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.down * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadzone") || collision.gameObject.CompareTag("Moon") || collision.gameObject.CompareTag("Earth"))
        {
            GameStateManager.Instance.OnStateHaveBennChanged -= OnStateChanged;
            Destroy(gameObject);
        }
    }

}
