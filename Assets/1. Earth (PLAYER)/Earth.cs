using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Earth : Singleton<Earth>
{
    /// <summary>
    /// The Earth Class define charateristics and behaviors of the Player of the game rapresented by a Earth Planet/ship
    /// </summary>
    int myHp;


    [SerializeField] GameObject cockpit;
    [SerializeField] GameObject body;
    [SerializeField] GameObject wings;

    [SerializeField] GameObject lowerShield;

    [SerializeField] GameObject[] projectiles;
    [SerializeField] ShootingModule leftCannon;
    [SerializeField] ShootingModule rightCannon;


    [SerializeField] GameObject myWeapon;


    bool alive;
    Vector3 mySize;


    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
        EventManager.OnLifeLost += LoseLife;
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
        EventManager.OnLifeLost -= LoseLife;
    }

    private void Start()
    {
        mySize = transform.localScale;
    }

    public void InitEarth()
    {
        myHp = 3;
        alive = true;
        lowerShield.SetActive(false);

        leftCannon.InitGun(projectiles[0], 0.4f, 1f);
        rightCannon.InitGun(projectiles[0], 0, 1f);
    }

    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                InitEarth();
                gameObject.GetComponent<EarthController>().ResetEarthPosition();
                break;
            case GameManager.GameState.Play:
                gameObject.GetComponent<EarthController>().GoPlay(true);
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.Gameover:
                gameObject.GetComponent<EarthController>().GoPlay(false);
                StopAnyEffect();
                break;
            case GameManager.GameState.Win:
                gameObject.GetComponent<EarthController>().GoPlay(false);
                StopAnyEffect();
                break;
        }
    }

    /// <summary>
    /// This method will be called every time something occurs that deals dmg to our EarthShip by the event it is registered to.
    /// </summary>
    /// <param name="dmg"></param>
    public void LoseLife()
    {
        myHp--;
        if (myHp <= 0)
        {
            alive = false;
            EventManager.ChangeGameState(GameState.Gameover);
        }
    }


    public bool IsAlive()
    {
        return alive;
    }

    public int GetHP()
    {
        return myHp;
    }

    public void SetHP(int amount)
    {
        myHp = amount;
    }

    public void AddHp()
    {
        myHp++;
    }

    

    /// <summary>
    ///  When the Earth collides with the moon it will be applied a force on the Moon based on the point of collision.
    ///  This method will be (probably) modified later.
    /// </summary>
    /// <param name="coll"></param>
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Moon")
        {
            Rigidbody2D ballRb = coll.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = coll.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPoint.x;

            if (hitPoint.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), MoonManager.Instance.initialMoonSpeed));
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), MoonManager.Instance.initialMoonSpeed));
            }
        }
    }

    /// <summary>
    /// When a projectile collides with the Earth, a decrease life will occurs. 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            EventManager.LoseLife();
        }
    }

    /// <summary>
    /// Power ups are coroutines that occurs for a determinated amount of time and applies different effects
    /// </summary>
    #region POWER UP EFFECTS REGION

    /// <summary>
    /// The Earth size increase.
    /// </summary>
    /// <param name="timer"></param>
    /// <returns></returns>
    public IEnumerator Bigger(float timer)
    {
        StopAnyEffect();
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        yield return new WaitForSeconds(timer);
        gameObject.transform.localScale = mySize;
    }

    /// <summary>
    /// The Earth size reduce.
    /// </summary>
    /// <param name="timer"></param>
    /// <returns></returns>
    public IEnumerator Smaller(float timer)
    {
        StopAnyEffect();
        gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1);

        yield return new WaitForSeconds(timer);

        gameObject.transform.localScale = mySize;
    }

    /// <summary>
    /// The lower shield ( a collider that covers the bottom of the screen) appears and then when the coroutine ends it disappear.
    /// </summary>
    /// <param name="timer"></param>
    /// <returns></returns>
    public IEnumerator LowShield(float timer)
    {
        StopAnyEffect();
        lowerShield.SetActive(true);

        yield return new WaitForSeconds(timer);

        lowerShield.SetActive(false);
    }

    public IEnumerator DoubleBullets(float timer)
    {
        StopAnyEffect();
        myWeapon.GetComponent<SpriteRenderer>().enabled = true;

        leftCannon.TurnOn();
        rightCannon.TurnOn();

        yield return new WaitForSeconds(timer);
        myWeapon.GetComponent<SpriteRenderer>().enabled = false;
        leftCannon.TurnOff();
        rightCannon.TurnOff();
    }

    public void StopAnyEffect()
    {
        //Revert for Bigger and Smaller
        gameObject.transform.localScale = mySize;

        //Revert for lowerShield
        lowerShield.SetActive(false);

        //Revert double bullets
        myWeapon.GetComponent<SpriteRenderer>().enabled = false;
        leftCannon.TurnOff();
        rightCannon.TurnOff();
    }


    #endregion
}
