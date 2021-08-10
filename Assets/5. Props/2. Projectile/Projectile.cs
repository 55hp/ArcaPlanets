using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] int dmg;
    [SerializeField] float speed;
    [SerializeField] DIRECTION direction;
    Vector3 dir;

    public enum DIRECTION
    {
        UP,
        DOWN,
        LEFT_DOWN,
        RIGHT_DOWN
    }

    private void Start()
    {
        switch (direction)
        {
            case DIRECTION.UP:
                dir = Vector3.up;
                break;
            case DIRECTION.DOWN:
                dir = Vector3.down;
                break;
            case DIRECTION.LEFT_DOWN:
                dir = new Vector3(-0.3f, -1, 0);
                break;
            case DIRECTION.RIGHT_DOWN:
                dir = new Vector3(0.3f, -1, 0);
                break;
        }
    }

    public void SetProjectile(DIRECTION direction , int damage , float speed , Sprite bulletSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = bulletSprite;
        switch (direction)
        {
            case DIRECTION.UP:
                dir = Vector3.up;
                break;
            case DIRECTION.DOWN:
                dir = Vector3.down;
                break;
            case DIRECTION.LEFT_DOWN:
                dir = new Vector3(-0.3f, -1, 0);
                break;
            case DIRECTION.RIGHT_DOWN:
                dir = new Vector3(0.3f, -1, 0);
                break;
        }
        this.dmg = damage;
        this.speed = speed;
    }


    // Update is called once per frame
    void Update()
    {
        this.transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Moon") || collision.gameObject.CompareTag("EarthProjectile") || collision.gameObject.CompareTag("MobShield"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Earth"))
        {
            EventManager.LoseLife();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Mob"))
        {
            collision.gameObject.GetComponent<Mob>().DecreaseLife(dmg);
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EarthProjectile"))
        {
            Destroy(gameObject);
        }
    }

    public int GetDmg()
    {
        return dmg;
    }
}
