using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int dmg;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {

        GameManager.Instance.OnStateHaveBeenChanged += OnStateChanged;

        if (dmg == 0)
        {
            dmg = 1;
        }

        if (speed == 0)
        {
            speed = 5;
        }
    }

    public int GetDamage()
    {
        return dmg;
    }

    public void OnStateChanged()
    {
        if (GameManager.Instance.GetState() == GameManager.GameState.Pause || GameManager.Instance.GetState() == GameManager.GameState.Play)
        {
            return;
        }

        GameManager.Instance.OnStateHaveBeenChanged -= OnStateChanged;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadzone") || collision.gameObject.CompareTag("Moon") || collision.gameObject.CompareTag("Earth"))
        {
            GameManager.Instance.OnStateHaveBeenChanged -= OnStateChanged;
            Destroy(gameObject);
        }
    }
}
