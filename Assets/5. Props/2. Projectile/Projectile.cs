using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] int dmg;
    [SerializeField] float speed;
    [SerializeField] DIRECTION direction;
    [SerializeField] Sprite[] animSprites;
    [SerializeField] Sprite[] explosion;
    Vector3 dir;

    [SerializeField] float explosionSize;
    public enum DIRECTION
    {
        UP,
        DOWN,
        LEFT_DOWN,
        RIGHT_DOWN
    }

    private void Start()
    {
        if(explosionSize == 0) explosionSize = 1;

        StartCoroutine(AnimationController.LoopingCicle(this.gameObject, animSprites, 0.15f));
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
        if (collision.gameObject.CompareTag("Moon") || collision.gameObject.CompareTag("EarthProjectile") )
        {
            StopAllCoroutines();
            DestroyProjectile();
        }
        else if (collision.gameObject.CompareTag("Earth"))
        {
            EventManager.LoseLife();
            DestroyProjectile();
        }
        else if (collision.gameObject.CompareTag("Mob"))
        {
            collision.gameObject.GetComponent<Planet>().DecreaseLife(dmg);
            DestroyProjectile();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EarthProjectile") || collision.gameObject.CompareTag("MobShield"))
        {
            DestroyProjectile();
        }
    }

    public int GetDmg()
    {
        return dmg;
    }

    private void DestroyProjectile()
    {
        //Neutralizzare il proiettile qui
        StopAllCoroutines();
        StartCoroutine(AnimationController.FixedCicle(this.gameObject, explosion, 0.1f));
        StartCoroutine(ExplosionTimer(explosion.Length * 0.1f));
        StartCoroutine(Larger(0.2f,explosionSize));
    }


    private IEnumerator ExplosionTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private IEnumerator Larger(float startingTime , float amount)
    {
        yield return new WaitForSeconds(startingTime);
        transform.localScale *= amount;
    }
}
