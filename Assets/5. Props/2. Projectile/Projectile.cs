using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int dmg;
    [SerializeField] float speed;
    [SerializeField] bool up;
    Vector3 direction;



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

        if (up)
        {
            direction = Vector3.up;
        }
        else
        {
            direction = Vector3.down;
        }
    }

    public int GetDmg()
    {
        return dmg;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadzone") || collision.gameObject.CompareTag("Moon") )
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Earth"))
        {
            EventManager.LoseLife();
            Destroy(gameObject);
        }else if (collision.gameObject.CompareTag("Mob"))
        {
            collision.gameObject.GetComponent<Mob>().DecreaseLife(dmg);
            Destroy(gameObject);
        }

    }
}
