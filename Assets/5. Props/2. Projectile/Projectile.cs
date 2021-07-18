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

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadzone") || collision.gameObject.CompareTag("Moon") || collision.gameObject.CompareTag("Earth"))
        {
            Destroy(gameObject);
        }
    }
}
