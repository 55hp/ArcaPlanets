using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float asteroidSpeed;
    int myLife;
    [SerializeField] GameObject mySpriteObj;
    [SerializeField] Sprite[] mySkin;

    GameObject powerUp;
    GameObject asteroidExplosion;


    public void SetAsteroid(Sprite[] skins , GameObject randomPowerUp , float speedAmount , Color color , int hp , GameObject explosion)
    {
        myLife = hp;
        mySkin = skins;
        mySpriteObj.GetComponent<SpriteRenderer>().sprite = mySkin[hp - 1];
        powerUp = randomPowerUp;
        asteroidSpeed = speedAmount;
        mySpriteObj.GetComponent<SpriteRenderer>().color = color;
        gameObject.GetComponent<CircleCollider2D>().radius = myLife * 0.22f;
        asteroidExplosion = explosion;
    }

    

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.right * asteroidSpeed * Time.deltaTime);
        mySpriteObj.transform.Rotate(Vector3.forward * 0.2f,Space.Self);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadzone"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("EarthProjectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<Projectile>().GetDmg());
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Moon"))
        {
            TakeDamage(collision.gameObject.GetComponent<Moon>().GetDmg());
        }
    }


    public void TakeDamage(int amount)
    {
        myLife -= amount;

        if (myLife > 0)
        {
            mySpriteObj.GetComponent<SpriteRenderer>().sprite = mySkin[myLife-1];
        }

        if (myLife <= 0)
        {
            if(powerUp != null)
            Instantiate(powerUp, gameObject.transform.position, Quaternion.identity);
            Instantiate(asteroidExplosion, gameObject.transform.position, Quaternion.identity);
            //TODO Aspetta prima un paio di frame 
            Destroy(gameObject);
        }
    }
    
}
