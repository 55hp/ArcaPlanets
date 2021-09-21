using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] int dmg;
    [SerializeField] float speed;
    [SerializeField] DIRECTION direction;
    [SerializeField] Sprite[] animSprites;
    Vector3 dir;

    [SerializeField] GameObject explosion;
    [SerializeField] float explosionVerticalOffset;


    public enum DIRECTION
    {
        UP,
        DOWN,
        LEFT_DOWN,
        RIGHT_DOWN
    }

    private void Start()
    {
        StartCoroutine(AnimationController.LoopingCicle(this.gameObject, animSprites, 0.15f));

        //Glowing color for the bullets
        //StartCoroutine(AnimationController.Glow(0.01f, this.gameObject, this.GetComponent<SpriteRenderer>().color, glowingColor));
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
        if (collision.gameObject.CompareTag("Earth"))
        {
            EventManager.LoseLife();
        }
        else if (collision.gameObject.CompareTag("Planet"))
        {
            collision.gameObject.GetComponent<Planet>().DecreaseLife(dmg);
        }
        else if (collision.gameObject.CompareTag("Satellite"))
        {
            collision.gameObject.GetComponent<Satellite>().DecreaseLife(dmg);
        }
        DestroyProjectile();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            DestroyProjectile();
    }

    public int GetDmg()
    {
        return dmg;
    }

    private void DestroyProjectile()
    {
        Instantiate(explosion, new Vector3( this.transform.position.x, this.transform.position.y + explosionVerticalOffset, this.transform.position.z), Quaternion.identity);
        gameObject.SetActive(false);
    }
    
}
