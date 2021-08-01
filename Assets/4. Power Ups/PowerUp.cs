using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] float timerEffect;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (speed <= 0)
        {
            //Default value 5
            speed = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        transform.Rotate(Vector3.forward * 0.2f, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Earth"))
        {
            PowerUpManager.Instance.TriggerPowerUpEffect(id, timerEffect);
            Destroy(gameObject);
        }
    }
}

