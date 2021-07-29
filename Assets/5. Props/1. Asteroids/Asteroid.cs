using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float asteroidSpeed;
    [SerializeField] int hp;
    [SerializeField] GameObject mySpriteObj;
    public GameObject powerUp;

    public void SetAsteroid(Sprite mySkin , GameObject randomPowerUp , float speedAmount , Color color)
    {
        mySpriteObj.GetComponent<SpriteRenderer>().sprite = mySkin;
        powerUp = randomPowerUp;
        asteroidSpeed = speedAmount;
        mySpriteObj.GetComponent<SpriteRenderer>().color = color;
    }

    public void SetAsteroid(Sprite mySkin, float speedAmount, Color color)
    {
        mySpriteObj.GetComponent<SpriteRenderer>().sprite = mySkin;
        powerUp = null;
        asteroidSpeed = speedAmount;
        mySpriteObj.GetComponent<SpriteRenderer>().color = color;
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
        hp -= amount;
        if (hp <= 0)
        {
            Instantiate(powerUp, gameObject.transform.position, Quaternion.identity);
            //TODO Aspetta prima un paio di frame 
            Destroy(gameObject);
        }
    }
    
}
